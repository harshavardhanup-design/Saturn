using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaturnAR
{
    /// <summary>
    /// Enables responsive mobile touch interaction: single-finger drag to rotate,
    /// two-finger pinch to scale, and smooth reset. Supports mouse drag in Editor.
    /// </summary>
    public class PlanetTouchInteraction : MonoBehaviour
    {
        [Header("Target & Reference")]
        [SerializeField] private Transform targetModel;
        [SerializeField] private RotationControllerPlanet autoRotation;

        [Header("Rotation Settings")]
        [SerializeField] private float rotationSpeed = 0.25f;
        [SerializeField] private float rotationDamping = 8f;
        [SerializeField] private bool invertY = false;

        [Header("Pinch-to-Scale Settings")]
        [SerializeField] private float minScale = 0.25f;
        [SerializeField] private float maxScale = 2.8f;
        [SerializeField] private float pinchSensitivity = 0.003f;
        [SerializeField] private float scaleDamping = 10f;

        // Internal State
        private Vector3 initialPosition;
        private Quaternion initialRotation;
        private Vector3 initialScale;

        private Vector3 currentVelocity;
        private float targetScaleFactor = 1f;
        private float currentScaleFactor = 1f;

        private Vector2 lastTouchPosition;
        private bool isTouching = false;
        private float angularVelocityX;
        private float angularVelocityY;

        private void Awake()
        {
            if (targetModel == null)
            {
                targetModel = transform;
            }

            if (autoRotation == null)
            {
                autoRotation = GetComponent<RotationControllerPlanet>();
            }

            // Cache pristine initial transforms for the Reset button
            initialPosition = targetModel.localPosition;
            initialRotation = targetModel.localRotation;
            initialScale = targetModel.localScale;
            currentScaleFactor = 1f;
            targetScaleFactor = 1f;
        }

        private void Update()
        {
            HandleTouchInput();
            HandleMouseInput();
            ApplyInertiaAndDamping();
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    isTouching = true;
                    lastTouchPosition = touch.position;
                    if (autoRotation != null) autoRotation.SetAutoRotation(false);
                }
                else if (touch.phase == TouchPhase.Moved && isTouching)
                {
                    Vector2 delta = touch.deltaPosition;
                    angularVelocityY = -delta.x * rotationSpeed;
                    angularVelocityX = (invertY ? -delta.y : delta.y) * rotationSpeed;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isTouching = false;
                }
            }
            else if (Input.touchCount >= 2)
            {
                // Pinch to scale
                Touch t0 = Input.GetTouch(0);
                Touch t1 = Input.GetTouch(1);

                Vector2 prevPos0 = t0.position - t0.deltaPosition;
                Vector2 prevPos1 = t1.position - t1.deltaPosition;

                float prevDistance = (prevPos0 - prevPos1).magnitude;
                float currentDistance = (t0.position - t1.position).magnitude;
                float deltaDistance = currentDistance - prevDistance;

                targetScaleFactor += deltaDistance * pinchSensitivity;
                targetScaleFactor = Mathf.Clamp(targetScaleFactor, minScale, maxScale);

                if (autoRotation != null) autoRotation.SetAutoRotation(false);
            }
        }

        private void HandleMouseInput()
        {
            // Unity Editor development fallback
            if (!Application.isMobilePlatform)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isTouching = true;
                    lastTouchPosition = Input.mousePosition;
                    if (autoRotation != null) autoRotation.SetAutoRotation(false);
                }
                else if (Input.GetMouseButton(0) && isTouching)
                {
                    Vector2 currentPos = Input.mousePosition;
                    Vector2 delta = currentPos - lastTouchPosition;
                    lastTouchPosition = currentPos;

                    angularVelocityY = -delta.x * rotationSpeed * 1.5f;
                    angularVelocityX = (invertY ? -delta.y : delta.y) * rotationSpeed * 1.5f;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    isTouching = false;
                }

                // Mouse scroll wheel zoom
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (Mathf.Abs(scroll) > 0.001f)
                {
                    targetScaleFactor += scroll * 1.5f;
                    targetScaleFactor = Mathf.Clamp(targetScaleFactor, minScale, maxScale);
                }
            }
        }

        private void ApplyInertiaAndDamping()
        {
            if (targetModel == null) return;

            // Apply angular velocity to model
            if (Mathf.Abs(angularVelocityX) > 0.001f || Mathf.Abs(angularVelocityY) > 0.001f)
            {
                targetModel.Rotate(Vector3.up, angularVelocityY, Space.World);
                targetModel.Rotate(Vector3.right, angularVelocityX, Space.World);

                // Smooth deceleration damping
                angularVelocityX = Mathf.Lerp(angularVelocityX, 0f, Time.deltaTime * rotationDamping);
                angularVelocityY = Mathf.Lerp(angularVelocityY, 0f, Time.deltaTime * rotationDamping);
            }

            // Smooth scale lerp
            currentScaleFactor = Mathf.Lerp(currentScaleFactor, targetScaleFactor, Time.deltaTime * scaleDamping);
            targetModel.localScale = initialScale * currentScaleFactor;
        }

        /// <summary>
        /// Resets the planet model smoothly back to its default orientation, scale, and position.
        /// Wired to the UI Reset Button.
        /// </summary>
        public void ResetTransform()
        {
            StopAllCoroutines();
            StartCoroutine(SmoothResetRoutine());
        }

        private IEnumerator SmoothResetRoutine()
        {
            angularVelocityX = 0f;
            angularVelocityY = 0f;
            targetScaleFactor = 1f;

            Vector3 startPos = targetModel.localPosition;
            Quaternion startRot = targetModel.localRotation;
            Vector3 startScale = targetModel.localScale;

            float elapsed = 0f;
            float duration = 0.45f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
                targetModel.localPosition = Vector3.Lerp(startPos, initialPosition, t);
                targetModel.localRotation = Quaternion.Slerp(startRot, initialRotation, t);
                targetModel.localScale = Vector3.Lerp(startScale, initialScale, t);
                yield return null;
            }

            targetModel.localPosition = initialPosition;
            targetModel.localRotation = initialRotation;
            targetModel.localScale = initialScale;
            currentScaleFactor = 1f;

            if (autoRotation != null)
            {
                autoRotation.SetAutoRotation(true);
            }
        }
    }
}
