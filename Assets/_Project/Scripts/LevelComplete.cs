using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI finalScoreText, finalTurnsText;

        public void SetUI(int score, int turns)
        {
            finalScoreText.text = $"Final Score: {score}";
            finalTurnsText.text = $"Total Turns: {turns}";
        }

        public void OnCloseButtonPressed()
        {
            gameObject.SetActive(false);
            UIController.Instance.StartGameFromLevelComplete();
        }
    }
}
