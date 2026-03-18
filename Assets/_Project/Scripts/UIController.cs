using UnityEngine;

namespace _Project.Scripts
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        [SerializeField] private GameObject mainMenu, gridSelection, gameplayScreen; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private LevelComplete levelComplete;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void ResumeSavedGame()
        {
            mainMenu.SetActive(false);
            gameplayScreen.SetActive(true);
            gameManager.LoadGameProgress();
        }

        public void OpenGridSelectionFromMenu()
        {
            mainMenu.SetActive(false);
            gridSelection.SetActive(true);
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
