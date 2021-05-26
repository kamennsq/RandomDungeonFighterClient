using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Collections;
using System;

public class ClientSocket : MonoBehaviour {
	public static ClientSocket clientSocket;
	MyThread thread;
	public int var = 0;
	public string message = "";

	// Use this for initialization
	void Start () {
		if (clientSocket == null)
        {
			clientSocket = this;
        }
		else if (clientSocket == this)
        {
			Destroy(gameObject);
        }
		DontDestroyOnLoad(gameObject);
		thread = new MyThread ();
	}

    private void Update()
    {
        if (var == 1)
        {
			var = 2;
			SceneManager.LoadScene(1);
		}
		if (var == 3)
        {
			GameObject.Find("MessageText").GetComponent<Text>().text = "Incorrect login or password";
        }
    }

    public void Closing()
    {
		thread.Closing();
    }

	public void Sending(string message)
    {
		thread.Sending(message);
    }

    private void OnDestroy()
    {
		Closing();
    }

	public void setCharacterInfo(string json)
    {
		AccountController.setCharacterInfo(json);
	}
}

public class MyThread {
	//ClientSocket clientSocket;
	Thread thread;
	Socket s;
	public MyThread(){
		thread = new Thread (this.Connecting);
		thread.Start ();
	}
	void Connecting(){
		byte[] bytesR = new byte[1024];
		StringBuilder builder = new StringBuilder ();
		int bytesInt;
		string serverMessage;

		IPHostEntry host = Dns.GetHostEntry ("127.0.0.1"); 
		IPAddress address = (IPAddress) host.AddressList.GetValue(0); 

		IPEndPoint ipe = new IPEndPoint(address, 5051);
		s = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

		s.Connect (ipe);
		while (s.Connected){
			bytesInt = s.Receive (bytesR, bytesR.Length, 0);
			serverMessage = builder.Append(Encoding.ASCII.GetString(bytesR, 0, bytesInt)).ToString();
			if (serverMessage.Contains("loginResult"))
            {
				ResultMessage resultMessage = JsonConvert.DeserializeObject<ResultMessage>(serverMessage);
				if (resultMessage.result.Equals("true"))
                {
					ClientSocket.clientSocket.var = 1;
                }
                else
                {
					ClientSocket.clientSocket.var = 3;
				}
			}
			if (serverMessage.Contains("characterInfo"))
            {
				ClientSocket.clientSocket.setCharacterInfo(serverMessage.Trim());
			}
			if (serverMessage.Contains("newCharacterResult"))
            {
				ResultMessage resultMessage = JsonConvert.DeserializeObject<ResultMessage>(serverMessage);
				if (resultMessage.result.Equals("true"))
                {
					AccountSceneController.accountController.newCharacterSuccess();
                }
            }
			if (serverMessage.Contains("dungeonParameters"))
			{
				DungeonController.setInitialDungeonInfo(serverMessage.Trim());
			}
			if (serverMessage.Contains("newCellsToGo"))
			{
				DungeonController.setNewCellsToGo(serverMessage.Trim());
			}
			if (serverMessage.Contains("newEarnedMoney"))
			{
				DungeonController.setNewEarnedMoney(serverMessage.Trim());
			}
			if (serverMessage.Contains("newEarnedMagicPowder"))
			{
				DungeonController.setNewEarnedMagicPowder(serverMessage.Trim());
			}
			if (serverMessage.Contains("mobParameters"))
			{
				DungeonController.goToFightScene(serverMessage.Trim());
			}
			builder.Clear();
		}
	}

	public void Closing(){
		if (s.Connected == true)
        {
			Sending("{\"capture\":\"logout\"}");
			s.Shutdown(SocketShutdown.Both);
		}
		thread.Abort();
	}

	public void Sending(string message)
    {
		byte[] bytes = Encoding.ASCII.GetBytes(message);
		s.Send(bytes);
    }

	class ResultMessage
    {
		public string capture;
		public string result;
    }
}