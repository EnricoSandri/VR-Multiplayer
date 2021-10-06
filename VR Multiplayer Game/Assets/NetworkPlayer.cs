using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

// This class applies the current positions of the Rig to the avatar of the network player.
//additionally, it hides the network player instance to the player in control, this to avoid to see double avatar
public class NetworkPlayer : MonoBehaviour
{
   private PhotonView photonView;
   
   // set the network Player features 
   public Transform head;
   public Transform leftHand;
   public Transform rightHand;
   
   //Rig reference
   private Transform headRig;
   private Transform leftHandRig;
   private Transform rightHandRig;

   private void Start()
   {
      photonView = GetComponent<PhotonView>();
      
      //Find the rig of the avatar.
      XRRig rig = FindObjectOfType<XRRig>();
      headRig = rig.transform.Find("Camera Offset/Main Camera");
      leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
      rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");
   }

   private void Update()
   {
      // check if the prefab is spawned by the player or someone else. if its the players, hide the avatar features
      //Also, only the player can modify the transform. so it too is in the if();
      if (photonView.IsMine)
      {
         head.gameObject.SetActive(false);
         leftHand.gameObject.SetActive(false);
         rightHand.gameObject.SetActive(false);
         
         // map each transform 
         MapNetworkPlayerPosition(head,headRig);
         MapNetworkPlayerPosition(leftHand,leftHandRig);
         MapNetworkPlayerPosition(rightHand,rightHandRig);
      }
   }

   // Apply the transform of the rig to the avatar
   void MapNetworkPlayerPosition(Transform target, Transform rigTransform)
   {
      target.position = rigTransform.position;
      target.rotation = rigTransform.rotation;
   }
}
