using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellTypeCollection", menuName = "CellTypeCollection", order = 51)]
public class CellTypeCollection : ScriptableObject
{
    [SerializeField]
    private CellType[] cellTypes;

    public CellType getCellTypeByIndex(int index)
    {
        if (index > -1 && index < cellTypes.Length)
        {
            return cellTypes[index];
        }
        else
        {
            return cellTypes[0];
        }
    }

    public CellType[] getCellTypes()
    {
        return cellTypes;
    }
}
