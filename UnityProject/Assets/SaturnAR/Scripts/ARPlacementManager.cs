using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if AR_FOUNDATION_PRESENT
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
#endif

namespace SaturnAR
{
    /// <summary>
    /// Manages AR surface detection, raycast hit-testing, placement reticle,
    /// and planetary model anchoring onto real-world planes.
    /// Includes intelligent Editor/Simulation fallback to ensure zero runtime breaks.
    /// </summary>
    public class ARPlacementManager : MonoBehaviour
    {
        [Header("Placement Configuration")]
        [SerializeField] private GameObject planetRootObject;
        [SerializeField] private GameObject placementReticle;
        [SerializeField] private Camera arCamera;

        [Header("Fallback Settings")]
        [SerializeField] private float defaultDistanceInFront = 1.2f;
        [SerializeField] private float defaultHeightOffset = -0.1f;
        [SerializeField] private bool autoPlaceOnStartup = true;

        private bool isPlacedOnSurface = false;

#if AR_FOUNDATION_PRESENT
        [SerializeField] private ARRaycastManager raycastManager;
        [SerializeField] private ARPlaneManager planeManager;
        private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
#endif

        private void Start()
        {
            if (arCamera == null)
            {
                arCamera = Camera.main;
            }

            if (autoPlaceOnStartup)
            {
                PlaceInFrontOfCamera();
            }
        }

        private void Update()
        {
            UpdateReticle();
            HandlePlacementInput();
        }

        private void UpdateReticle()
        {
#if AR_FOUNDATION_PRESENT
            if (raycastManager != null && placementReticle != null && !isPlacedOnSurface)
            {
                Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    placementReticle.SetActive(true);
                    placementReticle.transform.position = hitPose.position;
                    placementReticle.transform.rotation = hitPose.rotation;
                    return;
                }
            }
#endif
            if (placementReticle != null && isPlacedOnSurface)
            {
                placementReticle.SetActive(false);
            }
        }

        private void HandlePlacementInput()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Touch touch = Input.GetTouch(0);

                // Ignore UI touches
                if (UnityEngine.EventSystems.EventSystem.current != null &&
                    UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }

#if AR_FOUNDATION_PRESENT
                if (raycastManager != null && raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    AnchorPlanetToPosition(hitPose.position);
                    return;
                }
#endif
            }
        }

        /// <summary>
        /// Anchors the planetary model onto a specific 3D coordinate.
        /// </summary>
        public void AnchorPlanetToPosition(Vector3 worldPosition)
        {
            if (planetRootObject != null)
            {
                planetRootObject.transform.position = worldPosition;
                planetRootObject.SetActive(true);
                isPlacedOnSurface = true;
                if (placementReticle != null) placementReticle.SetActive(false);
                Debug.Log($"[SaturnAR.ARPlacementManager] Anchored planet to {worldPosition}");
            }
        }

        /// <summary>
        /// Places the planet directly in front of the active camera coordinate space.
        /// Guaranteed fallback for editor testing and room environments without detected planes.
        /// </summary>
        public void PlaceInFrontOfCamera()
        {
            if (planetRootObject == null) return;
            if (arCamera == null) arCamera = Camera.main;

            if (arCamera != null)
            {
                Vector3 targetPos = arCamera.transform.position +
                                   arCamera.transform.forward * defaultDistanceInFront +
                                   Vector3.up * defaultHeightOffset;

                planetRootObject.transform.position = targetPos;
                planetRootObject.transform.LookAt(arCamera.transform);
                planetRootObject.transform.Rotate(0f, 180f, 0f); // face front to camera
                planetRootObject.SetActive(true);
                isPlacedOnSurface = true;
            }
        }

        /// <summary>
        /// Repositions the planet in front of the camera when the user moves to a new location.
        /// </summary>
        public void ResetPlacement()
        {
            isPlacedOnSurface = false;
            PlaceInFrontOfCamera();
        }
    }
}
