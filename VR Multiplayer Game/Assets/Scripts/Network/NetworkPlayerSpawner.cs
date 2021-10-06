using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    
    //when player joins room do:
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
       
        //Spawn the player prefab for all the CLIENTS in the room
        //p.s the string in the instantiate function must be the same as the one in the project.
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
    }

    // when player leaves room do:
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        //Destroy the player prefab for all the CLIENTS in the room
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
