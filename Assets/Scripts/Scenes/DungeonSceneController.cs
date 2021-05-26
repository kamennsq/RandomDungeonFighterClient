using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSceneController : MonoBehaviour
{
    public static DungeonSceneController dungeonController;

    [Header("UI Elements")]
    [SerializeField] private Text coinsText;
    [SerializeField] private Text magicPowderText;
    [SerializeField] private Text currentHealthText;
    [SerializeField] private Text cellsToGoText;

    [Header("Map")]
    [SerializeField] private CellTypeCollection cellTypeCollection;
    [SerializeField] private CellTypeController[] cells;

    [Header("Private variables")]
    private int coins;
    private int magicPowder;
    private int currentHealth;
    private int maxHealth;
    private int cellsToGo;
    private int currentCell;
    private bool toFight;

    // Start is called before the first frame update
    void Awake()
    {
        dungeonController = this;
    }

    private void Start()
    {
        StartCoroutine(waitTimeToLoad());
    }

    // Update is called once per frame
    void Update()
    {
        cellsToGoText.text = "Cells to go: " + cellsToGo;
        currentHealthText.text = "Current HP: " + currentHealth;
        magicPowderText.text = "Magic Powder: " + magicPowder;
        coinsText.text = "Coins: " + coins;
        if (toFight)
        {
            toFight = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
    }

    public void goToFight()
    {
        toFight = true;
    }

    public void increaseCoins(int amount)
    {
        coins = amount;
    }

    public void increaseMagicPowder(int amount)
    {
        magicPowder = amount;
    }

    public void changeCurrentHealth(int amount)
    {
        currentHealth = amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void setCellsToGo(int amount)
    {
        cellsToGo = amount;
        foreach (CellTypeController cell in cells)
        {
            cell.unmarkCell();
        }
        if (cellsToGo > 0 && currentCell + cellsToGo - 1 < cells.Length) cells[currentCell + cellsToGo - 1].markNextCell();
        else if (cellsToGo > 0 && currentCell + cellsToGo - 1 >= cells.Length) cells[cells.Length - 1].markNextCell();
        else if (cellsToGo == 0 && currentCell > 0) cells[currentCell + cellsToGo - 1].markNextCell();
    }

    private void setInitialDungeonParametersOnScene()
    {
        DungeonController.DungeonParameters parameters = DungeonController.GetDungeonParameters();
        coins = parameters.earnedMoney;
        increaseCoins(coins);
        magicPowder = parameters.earnedMagicPowder;
        increaseMagicPowder(magicPowder);
        maxHealth = parameters.maxHealthPoints;
        currentHealth = parameters.currentHealthPoints;
        changeCurrentHealth(currentHealth);
        cellsToGo = parameters.cellsToGo;
        currentCell = parameters.currentCell;
        setCellsToGo(cellsToGo);
        foreach (DungeonController.MapCell mapCell in parameters.map)
        {
            cells[mapCell.cellID - 1].setCellType(cellTypeCollection.getCellTypeByIndex(mapCell.cellType));
        }
        if (currentCell == cells.Length && cellsToGo == 0) ClientSocket.clientSocket.Sending("{\"capture\":\"finishDungeon\"}"); ;
    }

    public void throwDice()
    {
        ClientSocket.clientSocket.Sending("{\"capture\":\"throwDice\"}");
    }

    IEnumerator waitTimeToLoad()
    {
        yield return new WaitForSeconds(1f);
        setInitialDungeonParametersOnScene();
    }

    public void changeCurrentCell()
    {
        currentCell += cellsToGo;
    }
}
