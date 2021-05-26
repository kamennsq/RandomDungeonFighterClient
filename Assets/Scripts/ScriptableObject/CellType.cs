using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellType", menuName = "CellType", order = 52)]
public class CellType : ScriptableObject
{
    [SerializeField]
    [Tooltip("Can be Buff, Debuff, MP, Coin, Mob, Boss")]
    private string typeName;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    [Tooltip("Not 0 for Coin and MP. 0 For the rest of types")]
    private int amount;

    public string getTypeName()
    {
        return typeName;
    }

    public Sprite getSprite()
    {
        return sprite;
    }

    public int getAmount()
    {
        return amount;
    }
}
