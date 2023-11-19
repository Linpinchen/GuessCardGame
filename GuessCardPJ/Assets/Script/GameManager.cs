using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Card> cards;
    [SerializeField]
    private List<Button> cardbuttons;
    [SerializeField]
    private List<Button> timeButtons;
    [SerializeField]
    private Button startGameButton;
    private DateControl dateControl;
    private Show show;
    [SerializeField]
    private Text gametimeText;
    [SerializeField]
    private Text gameWinText;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private Text winCounttext;
    private bool gameIsStart;
    [SerializeField]
    private Sprite[] cardsprites;
    [SerializeField]
    private Image blackMask;
    [SerializeField]
    private Text BlackMaskText;
    private bool canOpenCard;
    private float lerptime;
    private IEnumerator coroutineEvent;

   
    void Start()
    {
        Init();
    }

    void Update()
    {
        if (gameIsStart)
        {
            float gametiming = dateControl.StartGameTiming();
            gametimeText.text = gametiming.ToString("0.00");

            if (gametiming<=0)
            {
                BlackMaskText.text = show.GameOver;
                StartWinShow();
            }
        }

    }

    private void Init()
    {
        dateControl = new DateControl();
        dateControl.init(cards);
        show = new Show();
        gameIsStart = false;
        canOpenCard = true;
        StartGameEvent();
        SetCardButtonEvent();
        SetTimeButtonEvent();
        Reset();
    }

    private void Reset()
    {
        dateControl.Reset();
        dateControl.CardSort();
        show.SetCardSprite(cards, cardsprites);
        gametimeText.text = "0";
        winCounttext.text = "0";
        lerptime = 0;
        for (int i=0;i<cards.Count;i++)
        {
            show.OpenCard(cards[i],false);
        }

    }

    private void StartGameEvent()
    {
        startGameButton.onClick.AddListener(delegate
        {
            if (dateControl.ChooseTime!=0)
            {
                blackMask.gameObject.SetActive(false);
                startGameButton.interactable = false;
                ChangeCard();
                gameIsStart = true;
                scrollRect.horizontal = false;
                Debug.Log($"gameISStart :{gameIsStart}");
               
            }
           
        });
    }

    private void SetCardButtonEvent()
    {
        for (int i=0;i<cardbuttons.Count;i++)
        {
            int temp = i;
            cardbuttons[temp].onClick.AddListener(delegate()
            {
                if (canOpenCard)
                {
                    Debug.Log(temp);
                    show.StattJump(cards[temp],false);
                    show.OpenCard(cards[temp],true);
                    dateControl.AddCard(cards[temp]);
                    cards[temp].CardState = CardState.Open;
                    ComparesCard();
                    Debug.Log($"比較牌的總數量為(Compares)：{dateControl.Compares.Count}");
                }
            });

        }
    }

    private void SetTimeButtonEvent()
    {
        for (int i =0;i<timeButtons.Count;i++)
        {
            int tempi = i;
            timeButtons[tempi].onClick.AddListener(delegate()
            {
                if (!gameIsStart)
                {
                    Debug.Log($"Button:{tempi} , 選則的遊戲時間-->Datecontrol:{dateControl.ChooseTime}");
                    dateControl.SetGameTime(dateControl.GameTimes[tempi]);
                }
	        });
        }
    }

    private void ChangeCard()
    {
        dateControl.CardSort();
        show.SetCardSprite(cards, cardsprites);
    }

    private void ComparesCard()
    {
        Debug.Log("開始比較");
        if (dateControl.Compares.Count == 2)
        {
            CardTypeCompares();
        }
       
    }

    private void CardTypeCompares()
    {
        canOpenCard = false;
        Debug.Log("有兩張牌可以比");
        if (!dateControl.CompareCards())
        {
            Debug.Log("比對失敗");
            coroutineEvent = CloseCard();
            StartCoroutine(coroutineEvent);
        }
        else
        {
            Debug.Log("比對成功");
            canOpenCard = true;
            gameWinText.text = dateControl.WinCount.ToString();
            dateControl.CompareCardsClear();
            ComparesAllCard();
        }
        

    }

    private void ComparesAllCard()
    {
        if (dateControl.WinCount==dateControl.TotalCardType)
        {
            blackMask.gameObject.SetActive(false);
            BlackMaskText.text = show.Victory;
            StartWinShow();
        }
    }

    private void StartWinShow()
    {
        lerptime += Time.deltaTime;
        if (show.ShowWinCount(winCounttext,lerptime,dateControl.WinCount))
        {
            gameIsStart = false;
            //dateControl.Reset();

            //未做項目
            // 播放倒數計時功能的文字物件的動畫 慢慢放大到三倍 並移動到總配對次數動畫下方 時長 3秒
            //開啟 滑動時間功能以
            Reset();
            startGameButton.interactable = true;
            Debug.Log($"gameISStart :{gameIsStart}");
        }
    }

    IEnumerator CloseCard()
    {
        Debug.Log("開始執行協程");
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < dateControl.Compares.Count; i++)
        {
            cards[i].CardState = CardState.Close;
            show.PlayAnimationByBool(dateControl.Compares[i].GetAnimator(), "CardOpen", false);
        }
        dateControl.CompareCardsClear();
        canOpenCard = true;
    }
}
