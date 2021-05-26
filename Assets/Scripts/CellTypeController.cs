using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTypeController : MonoBehaviour
{
    private CellType cellType;

    [SerializeField]
    private int index;

    private Color initialColor;

    private bool isMarked;
    private bool isUnmarked;

    private bool isStepDone;

    // Start is called before the first frame update
    void Start()
    {
        isMarked = false;
        isUnmarked = false;
        isStepDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUnmarked)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = initialColor;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isUnmarked = false;
        }
        if (isMarked)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            isMarked = false;
        }
    }

    public void setCellType(CellType type)
    {
        cellType = type;
        gameObject.GetComponent<SpriteRenderer>().sprite = cellType.getSprite();
    }

    public void markNextCell()
    {
        isMarked = true;
        isStepDone = false;
    }

    public void unmarkCell()
    {
        isUnmarked = true;
    }

    public CellType getCellTypeOfCell()
    {
        return cellType;
    }

    private void OnMouseDown()
    {
        if (!isStepDone)
        {
            ClientSocket.clientSocket.Sending("{\"capture\":\"moveToNewCell\"}");
            DungeonSceneController.dungeonController.changeCurrentCell();
            isStepDone = true;
        }
    }

}
