using UnityEngine;
using TMPro;
using System.Collections;

namespace _Project.Scripts
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText, comboText, turnsText;
        private const float ComboAnimationDuration = 0.3f;
        public void UpdateVisuals(int score, int turns, int combo)
        {
            scoreText.SetText($"Score: {score}");
            turnsText.SetText($"Turns: {turns}");
            comboText.SetText(combo > 1 ? $"Combo x{combo}" : "");
            if (combo > 1) StartCoroutine(Pop(comboText.transform));
        }

        private IEnumerator Pop(Transform text)
        {
            text.localScale = Vector3.one * 1.5f;
            var startScale = text.localScale;
            var endScale = Vector3.one;
            var time = 0f;
            while (time < ComboAnimationDuration)
            {
                time += Time.deltaTime;
                text.localScale = Vector3.Lerp(startScale, endScale, time / ComboAnimationDuration);
                yield return null;
            }
        }
    }
}