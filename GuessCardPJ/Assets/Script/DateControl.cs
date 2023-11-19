using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateControl
{
    public  List<float> GameTimes { get; private set; }
    public float ChooseTime { get; private set; }
    public int WinCount { get; private set; }
    public List<Card> cards;
    private List<Card> compares;
    public List<Card> Compares { get { return compares; } }
    private const int MaxCompareCount=2;
    public int TotalCardType { get { return (int)(cards.Count / 2f); } }


    public void init(List<Card> cards)
    {
        this.cards = cards;
        compares = new List<Card>();
        GameTimes = new List<float> { 10f,20f,30f,40f };
        //WinCiunt = 0;
        //ChooseTime = 0;
        //FirstSetCardType();
        Reset();

    }

    public void Reset()
    {
        WinCount = 0;
        //CardSort();
        compares.Clear();
        ChooseTime = 0;

    }

    //public void FirstSetCardType()
    //{
    //    int temptype = 0;

    //    for (int i =0;i<cards.Count;i+=2)
    //    {
    //        cards[i].CardType = (CardType)temptype;
    //        cards[i + 1].CardType = (CardType)temptype;
    //        temptype++;
    //    }
    //}

    public void AddCard(Card card)
    {
        Debug.Log("加牌來比較");
        Debug.Log($"MaxCompareCount:{MaxCompareCount}");
        Debug.Log($"CanAddCard :{CanAddCard(card)}");
        if (compares.Count <= MaxCompareCount && CanAddCard(card))
        {
            Debug.Log("開始添加");
            compares.Add(card);
            Debug.Log($"當前比較的List數量為{compares.Count}");
        }

        Debug.Log($"compares : {compares.Count}");
    }

    private bool Repeatclick(Card card)
    {
        if (!compares.Contains(card))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanAddCard(Card card)
    {
        if (card.CardState == CardState.Close)
        {
            if (Repeatclick(card))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 用的是Fisher-Yates排序演算法
    /// </summary>
    public void CardSort()
    {
        int index = cards.Count;
        while (index>1)
        {
            int rand = Random.Range(0, index);
            index--;
            CardType tempcardType = cards[index].CardType;
            cards[index].CardType = cards[rand].CardType;
            cards[rand].CardType = tempcardType;

        }
    }

    public bool CompareCards()
    {

        if (compares[0].CardType == compares[1].CardType)
        {
            Debug.Log($"Card0 :{compares[0].CardType}");
            Debug.Log($"Card1 :{compares[1].CardType}");
            WinCount++;
            Debug.Log($"WinCount:{WinCount}");
            return true;
        }
        else
        {
            for (int i = 0; i < compares.Count; i++)
            {
                compares[i].CardState = CardState.Close;
            }
            return false;
        }
       
        
    }

    public void CompareCardsClear()
    {
        compares.Clear();
    }

    public void SetGameTime(float gameTime)
    {
        ChooseTime = gameTime;
        Debug.Log($"選擇挑戰的遊戲時間為{ChooseTime}");
    }

    public float StartGameTiming()
    {
        ChooseTime -= Time.deltaTime;
        if (ChooseTime <= 0)
        {
            return 0;
        }
        else
        {
            return ChooseTime;
        }
        
    }

    

}
