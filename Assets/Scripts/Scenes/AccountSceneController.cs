using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AccountSceneController : MonoBehaviour
{
    public static AccountSceneController accountController;

    [Header("Canvases")]
    [SerializeField] private GameObject defaultCanvas;
    [SerializeField] private GameObject newCharacterCanvas;
    [SerializeField] private GameObject deckCanvas;

    [Header("Models")]
    [SerializeField] private GameObject characterModel;

    [Header("Characters")]
    [SerializeField] private GameObject[] characterPanels;
    [SerializeField] private GameObject[] nicknameTexts;
    [SerializeField] private GameObject[] classNameTexts;
    [SerializeField] private GameObject[] levelTexts;
    [SerializeField] private GameObject[] images;

    [Header("Buttons")]
    [SerializeField] private GameObject newCharacterButton;

    [Header("New Character")]
    [SerializeField] private GameObject nickname;
    [SerializeField] private GameObject className;
    [SerializeField] private string[] classes;

    [Header("Deck")]
    [SerializeField] private GameObject[] cards;

    [Header("Collections")]
    [SerializeField] private CardCollection attackCardCollection;
    [SerializeField] private CardCollection buffCardCollection;

    [Header("Hidden properties")]
    private int currentClassName;
    private bool newCharacterSucceeded;
    private int curCharacter;

    // Start is called before the first frame update
    void Awake()
    {
        accountController = this;
    }

    private void Start()
    {
        currentClassName = 0;
        curCharacter = -1;
        newCharacterSucceeded = false;
        StartCoroutine(waitTimeToLoad());
    }
    // Update is called once per frame
    void Update()
    {
        if (newCharacterSucceeded)
        {
            StartCoroutine(waitTimeToLoad());
            newCharacterSucceeded = false;
        }
    }

    public void createNewCharacterFromServer(string nickname, string className, string level, int index)
    {
        if (index >= 0 && index < characterPanels.Length)
        {
            characterPanels[index].SetActive(true);
            nicknameTexts[index].GetComponent<Text>().text = nickname;
            classNameTexts[index].GetComponent<Text>().text = className;
            levelTexts[index].GetComponent<Text>().text = "Level " + level;
        }
        if (characterPanels[3].activeSelf)
        {
            newCharacterButton.SetActive(false);
        }
        if (characterPanels[0].activeSelf)
        {
            setCurrentCharacter(0);
        }
    }

    IEnumerator waitTimeToLoad()
    {
        yield return new WaitForSeconds(1f);
        AccountController.setCharactersOnScene();
        defaultCanvas.SetActive(true);
        newCharacterCanvas.SetActive(false);
    }

    public void activateCharacterCanvas()
    {
        defaultCanvas.SetActive(false);
        newCharacterCanvas.SetActive(true);
    }

    public void cancelCharacterCreation()
    {
        defaultCanvas.SetActive(true);
        newCharacterCanvas.SetActive(false);
    }

    public void createCharacter()
    {
        AccountController.sendNewCharacter(nickname.GetComponent<InputField>().text, className.GetComponent<Text>().text);
    }

    public void newCharacterSuccess()
    {
        newCharacterSucceeded = true;
    }

    public void nextClassName()
    {
        currentClassName++;
        if (currentClassName >= classes.Length)
        {
            currentClassName = 0;
        }
        className.GetComponent<Text>().text = classes[currentClassName];
    }

    public void previousClassName()
    {
        currentClassName--;
        if (currentClassName < 0)
        {
            currentClassName = classes.Length - 1;
        }
        className.GetComponent<Text>().text = classes[currentClassName];
    }

    public void openDeck()
    {
        defaultCanvas.SetActive(false);
        deckCanvas.SetActive(true);
        characterModel.SetActive(false);
        AccountController.setDeckOnScene(curCharacter);
    }

    public void closeDeck()
    {
        deckCanvas.SetActive(false);
        defaultCanvas.SetActive(true);
        characterModel.SetActive(true);
    }

    public void setDeckInfo(AccountController.Card[] deck)
    {
        int i = 0;
        foreach(AccountController.Card card in deck)
        {
            cards[i].SetActive(true);
            switch (card.cardType)
            {
                case "attack": cards[i].GetComponent<SpriteRenderer>().sprite = attackCardCollection.getCardByName(card.cardName).getSprite(); break;
                case "defense": cards[i].GetComponent<SpriteRenderer>().sprite = buffCardCollection.getCardByName(card.cardName).getSprite(); break;
            }
        }
    }

    public void setCurrentCharacter(int index)
    {
        if (curCharacter >= 0) characterPanels[curCharacter].GetComponent<Image>().color = Color.white;
        curCharacter = index;
        characterPanels[index].GetComponent<Image>().color = Color.yellow;
    }

    public int getCurrentCharacter()
    {
        return curCharacter;
    }

    public void toDungeon()
    {
        AccountController.toDungeon();
    }
}
