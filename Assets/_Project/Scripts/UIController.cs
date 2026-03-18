using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        [SerializeField] private GameObject gridSelection, gameplayScreen; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private LevelComplete levelComplete;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void StartGameFromGridSelection(int row, int col)
        {
            gridSelection.SetActive(false);
            gameplayScreen.SetActive(true);
            gameManager.StartNewGame(row, col);
        }

        public void ShowLevelCompleteScreen(int score, int turns)
        {
            levelComplete.SetUI(score, turns);
            levelComplete.gameObject.SetActive(true);
        }

        public void StartGameFromLevelComplete()
        {
            gameplayScreen.SetActive(false);
            gridSelection.SetActive(true);
        }
    }
}
