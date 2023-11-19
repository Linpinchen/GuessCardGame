using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show : MonoBehaviour
{
    public string Ready { get { return "AryYouReady?"; } }
    public string Victory { get { return "Congratulation!!"; } }
    public string GameOver { get { return "Game Over!!"; } }

    public void PlayAnimationByBool(Animator animator ,string animatorParmetes,bool PlayOn)
    {
        animator.SetBool(animatorParmetes, PlayOn);
    }

    public void OpenCard(Card card,bool isopen)
    {
        card.GetAnimator().SetBool("CardOpen", isopen);
    }

    public void StattJump(Card card, bool isopen)
    {
        card.GetAnimator().SetBool("CardJump", isopen);
    }

    public bool ShowWinCount(Text WinCounttext,float time,int WinCount)
    {
        if (time >= 1)
        {
            return true;
        }
        else
        {
            WinCounttext.text = Mathf.Lerp(0,WinCount,time).ToString("0");
            return false;
        }
    }

    public void SetCardSprite(List<Card> cards,Sprite[] sprites)
    {
        for (int i=0;i<cards.Count;i++)
        {
            cards[i].SetImage(sprites);
        }
    }

}
