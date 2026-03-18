using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject btnResume;

        private void OnEnable()
        {
            btnResume.SetActive(PlayerPrefs.HasKey("SavedGame"));
        }

        public void OnResumeButtonClicked()
        {
            UIController.Instance.ResumeSavedGame();
        }

        public void OnPlayButtonClicked()
        {
            // Clear the saved data since we are starting new game
            PlayerPrefs.DeleteKey("SavedGame");
            UIController.Instance.OpenGridSelectionFromMenu();
        }
    }
}
