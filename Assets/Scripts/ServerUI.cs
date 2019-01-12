using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class ServerUI : MonoBehaviour {

    void Start()
    {
        NetworkServer.Listen(Messages.Port);
        NetworkServer.RegisterHandler(Messages.FireMessageCode, ServerReceiveMessage);
    }

    void OnGUI()
    {
        string ipAddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipAddress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status: " + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connections: " + NetworkServer.connections.Count);
    }

    private void ServerReceiveMessage(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] messageValues = msg.value.Split('|');

        if(messageValues[0] == Messages.FireMessageName)
        {
            MainController.mainController.ShootAllArrows();
        }

        if(messageValues[0] == Messages.RotateUpMessageName)
        {
            MainController.mainController.RotateAllUp();
        }

        if (messageValues[0] == Messages.RotateDownMessageName)
        {
            MainController.mainController.RotateAllDown();
        }
    }

    void Update()
    {

    }
}
