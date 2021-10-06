using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int sceneIndex;
    public int maxPlayers;
}


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;
    public GameObject roomUi;
    
    // connect to the photon server using the settings on the PhotonServerSettings file
    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Trying to connect to server...");
    }

    // Check if you are connected to the SERVER, 
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to sever");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }
    
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("We joined the lobby!");
        roomUi.SetActive(true);
    }

    // when you are connected, create/join a room.
    public void InitialiseRoom(int defaultRoomIndex)
    {
        // Get the reference of the index of the default room chosen by the player
        DefaultRoom roomsettings = defaultRooms[defaultRoomIndex];
        
        //Load scene
        PhotonNetwork.LoadLevel(roomsettings.sceneIndex);
        
        //Create the room and apply the above roomsettings.
        // Room settings:
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomsettings.maxPlayers;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        
        // Join.
        PhotonNetwork.JoinOrCreateRoom(roomsettings.Name, roomOptions, TypedLobby.Default);
    }
    
    //check if you are connected to the ROOM 
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        base.OnJoinedRoom();
    }
    
    // check if a player joined the room 
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
