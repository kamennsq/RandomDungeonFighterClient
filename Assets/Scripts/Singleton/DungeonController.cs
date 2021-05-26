using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

public static class DungeonController
{
    private static DungeonParameters dungeonParameters;

    public static Mob mob;

    public static void setInitialDungeonInfo(string json)
    {
        dungeonParameters = JsonConvert.DeserializeObject<DungeonParameters>(json);
    }

    public static void setNewCellsToGo(string json)
    {
        DungeonParameters tempParameters = JsonConvert.DeserializeObject<DungeonParameters>(json);
        dungeonParameters.cellsToGo = tempParameters.cellsToGo;
        DungeonSceneController.dungeonController.setCellsToGo(dungeonParameters.cellsToGo);
    }

    public static void setNewEarnedMoney(string json)
    {
        DungeonParameters tempParameters = JsonConvert.DeserializeObject<DungeonParameters>(json);
        dungeonParameters.earnedMoney = tempParameters.earnedMoney;
        DungeonSceneController.dungeonController.increaseCoins(dungeonParameters.earnedMoney);
    }

    public static void setNewEarnedMagicPowder(string json)
    {
        DungeonParameters tempParameters = JsonConvert.DeserializeObject<DungeonParameters>(json);
        dungeonParameters.earnedMagicPowder = tempParameters.earnedMagicPowder;
        DungeonSceneController.dungeonController.increaseMagicPowder(dungeonParameters.earnedMagicPowder);
    }

    public static DungeonParameters GetDungeonParameters()
    {
        return dungeonParameters;
    }

    public static void goToFightScene(string json)
    {
        mob = JsonConvert.DeserializeObject<Mob>(json);
        DungeonSceneController.dungeonController.goToFight();
    }

    public class DungeonParameters
    {
        public string capture;
        public int earnedMoney;
        public int earnedMagicPowder;
        public int maxHealthPoints;
        public int currentHealthPoints;
        public int currentCell;
        public int cellsToGo;
        public Item[] inventory;
        public MapCell[] map;
    }

    public class Item
    {
        public string itemName;
        public int amount;
    }

    public class MapCell
    {
        public int cellID;
        public int cellType;
    }

    public class Mob
    {
        public string capture;
        public int health;
        public int deck;
    }
}
