using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaturnAR
{
    /// <summary>
    /// Controls automated axial rotation for planetary bodies.
    /// Preserves exact public field signatures from original workshop (PlanetObject, RotationVector).
    /// Hardened with defensive null-checks to eliminate DEF-02 NullReferenceExceptions.
    /// </summary>
    public class RotationControllerPlanet : MonoBehaviour
    {
        [Tooltip("The planet GameObject to rotate. If unassigned, automatically defaults to this GameObject.")]
        public GameObject PlanetObject;

        [Tooltip("Euler angular velocity vector in degrees per second (e.g. (0, 15, 0)).")]
        public Vector3 RotationVector = new Vector3(0f, 12f, 0f);

        [Tooltip("Enables or pauses automatic idle axial rotation.")]
        public bool isRotating = true;

        private Transform targetTransform;

        private void Awake()
        {
            ValidateTarget();
        }

        private void OnValidate()
        {
            ValidateTarget();
        }

        private void ValidateTarget()
        {
            // Defensive fix: Auto-assign to self if unassigned to prevent NullReferenceException
            if (PlanetObject == null)
            {
                PlanetObject = gameObject;
            }
            targetTransform = PlanetObject.transform;
        }

        private void Update()
        {
            if (!isRotating) return;

            if (targetTransform == null)
            {
                ValidateTarget();
                if (targetTransform == null) return;
            }

            // Smooth frame-rate independent rotation in world or local space
            targetTransform.Rotate(RotationVector * Time.deltaTime, Space.Self);
        }

        /// <summary>
        /// Temporarily pause rotation (e.g., when the user is actively dragging or inspecting a feature).
        /// </summary>
        public void SetAutoRotation(bool active)
        {
            isRotating = active;
        }

        /// <summary>
        /// Dynamically adjust the rotational speed.
        /// </summary>
        public void SetRotationSpeed(float speedMultiplier)
        {
            RotationVector = new Vector3(0f, 12f * speedMultiplier, 0f);
        }
    }
}
