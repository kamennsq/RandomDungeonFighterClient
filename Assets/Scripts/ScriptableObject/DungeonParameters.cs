using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonParameters", menuName = "DungeonParameters", order = 53)]
public class DungeonParameters : ScriptableObject
{
    [SerializeField]
    private CellType chosenCellType;

    public CellType getCellType()
    {
        return chosenCellType;
    }

    public void setCellType(CellType cellType)
    {
        chosenCellType = cellType;
    }
}
