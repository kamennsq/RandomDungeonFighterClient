using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;

public static class AccountController 
{
    static Account account = new Account();

    static Character newCharacter = new Character();

    private static CharacterInfo characterInfo;

    //public static AccountController accountController;
    // Start is called before the first frame update
    /*void Start()
    {
        if (accountController == null)
        {
            accountController = this;
        }
        else if (accountController != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }*/

    /*// Update is called once per frame
    void Update()
    {
        
    }*/

    public static void sendLoginMessage()
    {
        account.capture = "login";
        account.login = GameObject.Find("LoginInputField").GetComponent<InputField>().text;
        account.password = GameObject.Find("PasswordInputField").GetComponent<InputField>().text;
        if (account.login.Length < 4)
        {
            GameObject.Find("MessageText").GetComponent<Text>().text = "Login cannot be shorter than 4 symbols";
        }
        else if (account.password.Length < 7)
        {
            GameObject.Find("MessageText").GetComponent<Text>().text = "Password cannot be shorter than 7 symbols";
        }
        else
        {
            string output = JsonConvert.SerializeObject(account);
            ClientSocket.clientSocket.Sending(output);
        }
    }

    public static void setCharacterInfo(string json)
    {
        characterInfo = JsonConvert.DeserializeObject<CharacterInfo>(json);
    }

    public static void setCharactersOnScene()
    {
        int i = 0;
        foreach (Character character in characterInfo.characters)
        {
            AccountSceneController.accountController.createNewCharacterFromServer(character.nickname, character.className, character.level, i);
            i++;
        }
    }

    public static void setDeckOnScene(int index)
    {
        AccountSceneController.accountController.setDeckInfo(characterInfo.characters[index].deck);
    }

    public static void logOut()
    {
        ClientSocket.clientSocket.Sending("{\"capture\":\"logout\"}");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public static void toDungeon()
    {
        ClientSocket.clientSocket.Sending("{\"capture\":\"chooseCharacter\", \"id\":" + AccountSceneController.accountController.getCurrentCharacter() + "}");
        ClientSocket.clientSocket.Sending("{\"capture\":\"newDungeon\"}");
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public static void sendNewCharacter(string nickname, string className)
    {
        newCharacter.nickname = nickname;
        newCharacter.className = className;
        newCharacter.level = "1";
        CharacterInfo newCharacterInfo = new CharacterInfo();
        newCharacterInfo.capture = "newCharacter";
        newCharacterInfo.characters = new Character[1];
        newCharacterInfo.characters[0] = newCharacter;
        ClientSocket.clientSocket.Sending(JsonConvert.SerializeObject(newCharacterInfo));
    }

    class Account
    {
        public string capture;
        public string login;
        public string password;
    }

    class CharacterInfo
    {
        public string capture;
        public Character[] characters;
    }

    class Character
    {
        public string nickname;
        public string className;
        public string level;
        public Card[] deck;
    }

    public class Card
    {
        public string cardType;
        public string cardName;
        public string cardLevel;
        public string cardDamage;
        public string cardDefense;
    }
}
