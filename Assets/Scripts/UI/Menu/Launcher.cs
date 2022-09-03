using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_Text output;
    [SerializeField] private Animator LoadingScreenAnimator;
  
    void Start()
    {
        Screen.SetResolution(800, 600, FullScreenMode.Windowed);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("ConnectedToMaster");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        LoadingScreenAnimator.Play("LoadingScreen");
    }

    public void JoinRoom ()
    {
        if (input.text != "") PhotonNetwork.JoinRoom(input.text);
        else output.text = "Please enter room name!";
    }

    public void CreateRoom()
    {
        if (input.text != "")
        {
            RoomOptions options = new RoomOptions();
            PhotonNetwork.CreateRoom(input.text, options, TypedLobby.Default);
        }
        else
        {
            output.text = "Please enter room name!";
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    void Update()
    {
        
    }
}
