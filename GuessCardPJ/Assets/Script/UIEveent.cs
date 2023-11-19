using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEveent : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
 
    [SerializeField]
    private Card card;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (card.CardState == CardState.Close)
        {
            card.GetAnimator().SetBool("CardJump", true);
        }
        else
        {
            return;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (card.CardState == CardState.Close)
        {
            card.GetAnimator().SetBool("CardJump", false);
        }
        else
        {
            return;
        }
        
    }

}

