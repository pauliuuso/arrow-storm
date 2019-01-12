using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class ClientUI : MonoBehaviour
{
    NetworkClient client;
    public Button fireButton;
    public Button connectButton;
    public Button rotateUpButton;
    public Button rotateDownButton;
    public InputField ipAddress;

    void Start()
    {
        client = new NetworkClient();
        fireButton.onClick.AddListener(FireClicked);
        connectButton.onClick.AddListener(ConnectClicked);
        rotateUpButton.onClick.AddListener(RotateUpClicked);
        rotateDownButton.onClick.AddListener(RotateDownClicked);
    }

    void OnGUI()
    {
        string ipAddress = Network.player.ipAddress;
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Connected: " + client.isConnected);
    }

    void Update()
    {
        if(client.isConnected)
        {
            connectButton.gameObject.SetActive(false);
            ipAddress.gameObject.SetActive(false);

            fireButton.gameObject.SetActive(true);
            rotateDownButton.gameObject.SetActive(true);
            rotateUpButton.gameObject.SetActive(true);
        }
        else
        {
            connectButton.gameObject.SetActive(true);
            ipAddress.gameObject.SetActive(true);

            fireButton.gameObject.SetActive(false);
            rotateDownButton.gameObject.SetActive(false);
            rotateUpButton.gameObject.SetActive(false);
        }
    }

    public void RotateUpClicked()
    {
        SendNetworkMessage(Messages.RotateUpMessageName);
    }

    public void RotateDownClicked()
    {
        SendNetworkMessage(Messages.RotateDownMessageName);
    }

    public void FireClicked()
    {
        SendNetworkMessage(Messages.FireMessageName);
    }

    public void ConnectClicked()
    {
        string ip = ipAddress.GetComponent<InputField>().text;

        if(ip != "")
        {
            client.Connect(ip, Messages.Port);
        }

    }

    public void SendNetworkMessage(string messageValue)
    {
        if(client.isConnected)
        {
            StringMessage message = new StringMessage();
            message.value = messageValue;
            client.Send(Messages.FireMessageCode, message);
        }
    }

}
