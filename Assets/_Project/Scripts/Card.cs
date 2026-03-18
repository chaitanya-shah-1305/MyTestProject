using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardID;
    public Image cardImage;
    public Sprite backSprite;
    public Sprite frontSprite;
    [HideInInspector] public bool isFaceUp = false;
    [HideInInspector] public bool isMatched = false;

    public void Init(int id, Sprite front)
    {
        cardID = id;
        frontSprite = front;
        cardImage.sprite = backSprite;
        isMatched = false;
        isFaceUp = false;
        gameObject.SetActive(true);
    }
}