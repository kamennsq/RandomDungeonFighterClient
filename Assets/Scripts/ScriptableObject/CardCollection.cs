using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardCollection", menuName = "CardCollection", order = 55)]
public class CardCollection : ScriptableObject
{
    [SerializeField]
    private Card[] cards;

    public Card[] getCards()
    {
        return cards;
    }

    public Card getCardByName(string cardName)
    {
        foreach (Card card in cards)
        {
            if (card.getCardName().Equals(cardName))
            {
                return card;
            }
        }
        return null;
    }
}
