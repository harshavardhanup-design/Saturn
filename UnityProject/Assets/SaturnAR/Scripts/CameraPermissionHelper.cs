using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace SaturnAR
{
    /// <summary>
    /// Verifies and requests runtime Camera permissions required for ARCore tracking on Android.
    /// Provides graceful error handling if permission is denied.
    /// </summary>
    public class CameraPermissionHelper : MonoBehaviour
    {
        [Header("Permission Feedback UI (Optional)")]
        [SerializeField] private GameObject permissionWarningPanel;

        private void Start()
        {
            CheckCameraPermission();
        }

        public void CheckCameraPermission()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Debug.Log("[SaturnAR] Requesting Android Camera permission...");
                Permission.RequestUserPermission(Permission.Camera);
                StartCoroutine(WaitForPermission());
            }
            else
            {
                OnPermissionGranted();
            }
#else
            OnPermissionGranted();
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        private IEnumerator WaitForPermission()
        {
            yield return new WaitForSeconds(0.5f);
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                OnPermissionGranted();
            }
            else
            {
                OnPermissionDenied();
            }
        }
#endif

        private void OnPermissionGranted()
        {
            Debug.Log("[SaturnAR] Camera permission verified and authorized.");
            if (permissionWarningPanel != null)
            {
                permissionWarningPanel.SetActive(false);
            }
        }

        private void OnPermissionDenied()
        {
            Debug.LogWarning("[SaturnAR] Camera permission was denied. AR tracking cannot proceed without camera access.");
            if (permissionWarningPanel != null)
            {
                permissionWarningPanel.SetActive(true);
            }
        }

        public void ReRequestPermission()
        {
            CheckCameraPermission();
        }
    }
}
