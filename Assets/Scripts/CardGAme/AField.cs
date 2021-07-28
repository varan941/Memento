using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AField : MonoBehaviour
{
    [SerializeField] List<Card> cards=new List<Card>();


    public void SetCard(Card card)
    {
        cards.Add(card);
        card.transform.parent = transform;        
    }

    public void AlignCards()
    {
        Debug.Log("tyty");
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
    }
    
}
