using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaturnAR
{
    /// <summary>
    /// Manages scene navigation and transitions across SATURN AR scenes.
    /// Preserves full backward compatibility with original workshop method signature: SwitchScenes(string).
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        [Header("Audio Feedback (Optional)")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip transitionSfx;

        [Header("Debugging")]
        [SerializeField] private bool enableDebugLogs = true;

        private static SceneController instance;
        public static SceneController Instance => instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        /// <summary>
        /// Loads the designated scene by name.
        /// Preserves the exact name and signature taught in the original workshop.
        /// </summary>
        /// <param name="sceneName">Target scene name (e.g., "HomeScreen", "Saturn", "Earth", "Mars")</param>
        public void SwitchScenes(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError("[SaturnAR.SceneController] SwitchScenes called with null or empty scene name.");
                return;
            }

            if (enableDebugLogs)
            {
                Debug.Log($"[SaturnAR.SceneController] Transitioning to scene: '{sceneName}'");
            }

            if (audioSource != null && transitionSfx != null)
            {
                audioSource.PlayOneShot(transitionSfx);
            }

            // Verify scene exists in build settings or load directly
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// Convenience shortcut for UI Back buttons to return directly to the main menu.
        /// </summary>
        public void ReturnToHome()
        {
            SwitchScenes("HomeScreen");
        }

        /// <summary>
        /// Direct access method to load the flagship Saturn exploration scene.
        /// </summary>
        public void LoadSaturnScene()
        {
            SwitchScenes("Saturn");
        }

        /// <summary>
        /// Safely exits the application on Android devices.
        /// </summary>
        public void ExitApplication()
        {
            if (enableDebugLogs)
            {
                Debug.Log("[SaturnAR.SceneController] Exiting SATURN AR application.");
            }
            Application.Quit();
        }
    }
}
