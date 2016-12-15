using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NewNetworkManager : NetworkManager
{

    private Text m_Room;

    void Start()
    {
        //m_Room = GameObject.Find("GameName").GetComponent<Text>();
    }

    void Update()
    {
        if(NetworkManager.singleton.matches != null)
        {
            m_Room.text = NetworkManager.singleton.matches[0].name;
        }
    }

    public void startHost()
    {
        setPort();
        NetworkManager.singleton.StartHost();
    }

    public void joinGame()
    {
        setIPAddress();
        setPort();
        NetworkManager.singleton.StartClient();
    }

    void setIPAddress()
    {
        string ip = GameObject.Find("IPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ip;
    }

    void setPort()
    {
        NetworkManager.singleton.networkPort = 21222;
    }

    public void enableMatchMaker()
    {
        if (NetworkManager.singleton.matchMaker == null)
        {
            NetworkManager.singleton.StartMatchMaker();
            GameObject.Find("CreateInternetGame").GetComponent<Text>().enabled = true;
            GameObject.Find("JoinInternetGame").GetComponent<Text>().enabled = true;
        }
    }

    public void createInternetMatch()
    {
        if(NetworkManager.singleton.matchInfo == null)
        {
            if(NetworkManager.singleton.matches == null)
            {
                NetworkManager.singleton.SetMatchHost("mm.unet.unity3d.com", 443, true);
                string matchName = GameObject.Find("MatchName").transform.FindChild("Text").GetComponent<Text>().text;
                Debug.Log(matchName);
                NetworkManager.singleton.matchMaker.CreateMatch(matchName, (uint) 2, true, "", NetworkManager.singleton.OnMatchCreate);
            }
        }
    }

    public void findInternetMatch()
    {
        if (NetworkManager.singleton.matchInfo == null)
        {
            if (NetworkManager.singleton.matches == null)
            {
                NetworkManager.singleton.matchMaker.ListMatches(0, 20, "", NetworkManager.singleton.OnMatchList);
                Debug.Log("Matches lised");
            }
        }
    }

    public void joinMatch()
    {
        NetworkManager.singleton.matchName = NetworkManager.singleton.matches[0].name;
        NetworkManager.singleton.matchSize = (uint)2;
        NetworkManager.singleton.matchMaker.JoinMatch(NetworkManager.singleton.matches[0].networkId, "", NetworkManager.singleton.OnMatchJoined);
    }
}
