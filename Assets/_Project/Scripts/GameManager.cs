using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI & Grid References")]
    public GameObject cardPrefab;
    public Transform gridParent;
    public GridLayoutGroup gridLayout;
    public Sprite[] cardSprites; // Assign your card face textures here

    private List<Card> _allCards = new List<Card>();
    private int _currentRows, _currentCols;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        _currentRows = 2;
        _currentCols = 2;
        SetupGrid(_currentRows, _currentCols);
        var deck = GenerateDeck(_currentRows, _currentCols);
        foreach (var id in deck)
        {
            CreateCard(id);
        }
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
}