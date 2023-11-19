using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private CardState cardState;
    [SerializeField]
    private CardType cardType;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Image image;
    

    public CardState CardState { get { return cardState; } set { cardState = value; } }
    public CardType CardType { get { return cardType; } set { cardType = value; } }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void SetImage(Sprite[] sprites)
    {
        image.sprite = sprites[(int)cardType];
    }

}

public enum CardState
{
    Close,
    Open,
}

public enum CardType
{
    Peech,
    Cherry,
}