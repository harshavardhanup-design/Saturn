using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SaturnAR
{
    [System.Serializable]
    public struct PlanetData
    {
        public string planetName;
        public string designation;
        public string planetaryType;
        public string equatorialDiameter;
        public string mass;
        public string distanceFromSun;
        public string orbitalPeriod;
        public string dayLength;
        public string confirmedMoons;
        [TextArea(3, 8)]
        public string scientificDescription;
    }

    /// <summary>
    /// Controls the planetary scientific information panel HUD overlay.
    /// Provides collapsible display of authentic astronomical data.
    /// </summary>
    public class PlanetInfoManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject infoPanelContainer;
        [SerializeField] private Text planetTitleText;
        [SerializeField] private Text designationText;
        [SerializeField] private Text diameterValueText;
        [SerializeField] private Text massValueText;
        [SerializeField] private Text distanceValueText;
        [SerializeField] private Text orbitValueText;
        [SerializeField] private Text moonsValueText;
        [SerializeField] private Text descriptionBodyText;

        [Header("Planet Data Presets")]
        [SerializeField] private PlanetData saturnData = new PlanetData
        {
            planetName = "SATURN",
            designation = "Sol VI — The Ringed Gas Giant",
            planetaryType = "Gas Giant (Hydrogen & Helium)",
            equatorialDiameter = "116,464 km (9.14x Earth)",
            mass = "5.683 × 10^26 kg (95.16 Earths)",
            distanceFromSun = "1.434 Billion km (9.58 AU)",
            orbitalPeriod = "29.45 Earth Years",
            dayLength = "10 Hours 33 Minutes",
            confirmedMoons = "146 Recognized (Titan, Enceladus, Mimas)",
            scientificDescription = "Saturn is the sixth planet from the Sun and the second-largest in our solar system. Adorned with a dazzling, complex system of icy rings composed of billions of water-ice particles, Saturn hosts an atmosphere of alternating high-speed jet streams and ammonia clouds."
        };

        [SerializeField] private PlanetData earthData = new PlanetData
        {
            planetName = "EARTH",
            designation = "Sol III — The Blue Planet",
            planetaryType = "Terrestrial (Silicate Rock & Iron)",
            equatorialDiameter = "12,742 km",
            mass = "5.972 × 10^24 kg",
            distanceFromSun = "149.6 Million km (1.00 AU)",
            orbitalPeriod = "365.25 Days",
            dayLength = "24 Hours 00 Minutes",
            confirmedMoons = "1 (The Moon)",
            scientificDescription = "Earth is our home planet, the third from the Sun, and the only known astronomical body harboring liquid surface water and life."
        };

        [SerializeField] private PlanetData marsData = new PlanetData
        {
            planetName = "MARS",
            designation = "Sol IV — The Red Planet",
            planetaryType = "Terrestrial (Basaltic Crust)",
            equatorialDiameter = "6,779 km (0.53x Earth)",
            mass = "6.417 × 10^23 kg (0.107 Earths)",
            distanceFromSun = "227.9 Million km (1.52 AU)",
            orbitalPeriod = "687 Earth Days",
            dayLength = "24 Hours 37 Minutes",
            confirmedMoons = "2 (Phobos & Deimos)",
            scientificDescription = "Mars is a dusty, cold desert world with a thin carbon dioxide atmosphere. Its distinctive reddish hue originates from oxidized iron minerals across its vast volcanic plains."
        };

        [Header("Initial Active Planet")]
        [SerializeField] private string defaultPlanetKey = "Saturn";

        private bool isPanelOpen = false;

        private void Start()
        {
            // Auto-load based on current scene name
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (sceneName.Contains("Earth"))
            {
                DisplayPlanet(earthData);
            }
            else if (sceneName.Contains("Mars"))
            {
                DisplayPlanet(marsData);
            }
            else
            {
                DisplayPlanet(saturnData);
            }

            // Initially collapsed for unobscured AR view
            if (infoPanelContainer != null)
            {
                infoPanelContainer.SetActive(false);
            }
        }

        public void ToggleInfoPanel()
        {
            isPanelOpen = !isPanelOpen;
            if (infoPanelContainer != null)
            {
                infoPanelContainer.SetActive(isPanelOpen);
            }
        }

        public void CloseInfoPanel()
        {
            isPanelOpen = false;
            if (infoPanelContainer != null)
            {
                infoPanelContainer.SetActive(false);
            }
        }

        private void DisplayPlanet(PlanetData data)
        {
            if (planetTitleText != null) planetTitleText.text = data.planetName;
            if (designationText != null) designationText.text = data.designation;
            if (diameterValueText != null) diameterValueText.text = data.equatorialDiameter;
            if (massValueText != null) massValueText.text = data.mass;
            if (distanceValueText != null) distanceValueText.text = data.distanceFromSun;
            if (orbitValueText != null) orbitValueText.text = data.orbitalPeriod;
            if (moonsValueText != null) moonsValueText.text = data.confirmedMoons;
            if (descriptionBodyText != null) descriptionBodyText.text = data.scientificDescription;
        }
    }
}
