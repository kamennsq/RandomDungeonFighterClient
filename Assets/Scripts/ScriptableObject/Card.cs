using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 54)]
public class Card : ScriptableObject
{
    [SerializeField]
    private string cardName;

    [SerializeField]
    private string cardType;

    [SerializeField]
    private string cardLevel;

    [SerializeField]
    private Sprite sprite;

    public string getCardName()
    {
        return cardName;
    }

    public string getCardType()
    {
        return cardType;
    }

    public string getLevel()
    {
        return cardLevel;
    }

    public Sprite getSprite()
    {
        return sprite;
    }
}
