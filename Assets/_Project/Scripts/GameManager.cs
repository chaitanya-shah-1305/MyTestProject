using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("UI & Grid References")]
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform gridParent;
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private Sprite[] cardSprites; // List of face up cards (various shapes)
        [SerializeField] private GameUI gameUI;

        private List<Card> _allCards;
        private int _currentRows, _currentCols;
        private Card _activeCard;
        private int _score, _turns, _combo = 1;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        private void Start()
        {
            _currentRows = 2;
            _currentCols = 2;
            StartNewGame();
        }

        public void StartNewGame()
        {
            _allCards = new List<Card>();
            SetupGrid(_currentRows, _currentCols);
            var deck = GenerateDeck(_currentRows, _currentCols);
            foreach (var id in deck)
            {
                CreateCard(id);
            }
            UpdateUI();
        }

        // --- Grid & Deck Management ---
        private void SetupGrid(int rows, int cols)
        {
            var rect = gridParent.GetComponent<RectTransform>();
            var availableW = rect.rect.width - (gridLayout.padding.left + gridLayout.padding.right);
            var availableH = rect.rect.height - (gridLayout.padding.top + gridLayout.padding.bottom);

            var cellW = (availableW - (gridLayout.spacing.x * (cols - 1))) / cols;
            var cellH = (availableH - (gridLayout.spacing.y * (rows - 1))) / rows;

            var size = Mathf.Min(cellW, cellH);
            gridLayout.cellSize = new Vector2(size, size);
            gridLayout.constraintCount = cols;
        }

        // Shuffle and generate deck
        private List<int> GenerateDeck(int r, int c)
        {
            var deck = new List<int>();
            for (var i = 0; i < (r * c) / 2; i++) { deck.Add(i); deck.Add(i); }

            // Fisher-Yates Shuffle
            for (var i = deck.Count - 1; i > 0; i--)
            {
                var rnd = Random.Range(0, i + 1);
                (deck[i], deck[rnd]) = (deck[rnd], deck[i]);
            }
            return deck;
        }

        // Create cards
        private void CreateCard(int id)
        {
            var go = Instantiate(cardPrefab, gridParent);
            var c = go.GetComponent<Card>();
            c.Init(id, cardSprites[id]);
            _allCards.Add(c);
        }

        public void OnCardFlipped(Card card)
        {
            StartCoroutine(card.Flip(true));

            // If no card is opened, make this as an active card
            if (_activeCard == null)
            {
                _activeCard = card;
                return; 
            }

            // Check for the match
            var c1 = _activeCard;
            var c2 = card;
            _activeCard = null;
            StartCoroutine(ProcessMatch(c1, c2));
        }

        private IEnumerator ProcessMatch(Card c1, Card c2)
        {
            _turns++;
            yield return new WaitForSeconds(0.4f);
            if (c1.cardID == c2.cardID)
            {
                // Match Found
                c1.SetMatched();
                c2.SetMatched();
                _score += 100 * _combo;
                _combo++;
                CheckWinCondition();
            }
            else
            {
                // Match Failed
                StartCoroutine(c1.Flip(false));
                StartCoroutine(c2.Flip(false));
                _combo = 1;
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            gameUI.UpdateVisuals(_score, _turns, _combo);
        }

        private void CheckWinCondition()
        {
            var isComplete = true;
            foreach (var c in _allCards)
            {
                if (c.isMatched) continue;
                isComplete = false;
                break;
            }

            if (!isComplete) return;
            UIController.Instance.ShowLevelCompleteScreen(_score, _turns);
            ClearBoard();
        }

        private void ClearBoard()
        {
            foreach (Transform t in gridParent) Destroy(t.gameObject);
            _allCards.Clear();
            _score = 0;
            _turns = 0;
            _combo = 1;
            UpdateUI();
        }
    }
}