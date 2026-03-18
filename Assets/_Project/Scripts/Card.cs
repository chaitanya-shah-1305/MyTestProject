using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class Card : MonoBehaviour
    {
        public int cardID;
        public Image cardImage;
        public Sprite backSprite;
        public Sprite frontSprite;
        [HideInInspector] public bool isFaceUp = false;
        [HideInInspector] public bool isMatched = false;
        private const float FlipDuration = 0.15f, ScaleDownDuration = 0.15f;

        public void Init(int id, Sprite front)
        {
            cardID = id;
            frontSprite = front;
            cardImage.sprite = backSprite;
            isMatched = false;
            isFaceUp = false;
            gameObject.SetActive(true);
        }

        public void OnCardClick()
        {
            // Ignore if already face up, matched, or if the manager is full
            if (isFaceUp || isMatched) return;
            GameManager.Instance.OnCardFlipped(this);
        }

        public IEnumerator Flip(bool faceUp)
        {
            isFaceUp = faceUp;
            var time = 0f;

            var startRotation = transform.rotation;
            var endRotation = transform.rotation * Quaternion.Euler(0, 180, 0);

            var spriteSwapped = false;

            while (time < FlipDuration)
            {
                time += Time.deltaTime;
                var progress = time / FlipDuration;

                // Rotate the card
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, progress);

                // Swap sprite at the midpoint (90 degrees)
                if (!spriteSwapped && progress >= 0.5f)
                {
                    cardImage.sprite = faceUp ? frontSprite : backSprite;
                    spriteSwapped = true;
                }

                yield return null;
            }
        }

        public void SetMatched()
        {
            isMatched = true;
            // Remove Card
            StartCoroutine(RemoveCard());
        }

        private IEnumerator RemoveCard()
        {
            yield return new WaitForSeconds(0.3f);
            var startScale = transform.localScale;
            var endScale = Vector3.zero;
            var time = 0f;
            while (time < ScaleDownDuration)
            {
                time += Time.deltaTime;
                // Scale down the card
                transform.localScale = Vector3.Lerp(startScale, endScale, time / ScaleDownDuration);
                yield return null;
            }
        }
    }
}