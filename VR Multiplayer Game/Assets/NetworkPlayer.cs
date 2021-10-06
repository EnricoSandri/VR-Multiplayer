using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

// This class applies the current positions of the body parts of the player avatar to the network player.
//additionally, it hides the network player instance to the player in control, this to avoid to see double avatar
public class NetworkPlayer : MonoBehaviour
{
   private PhotonView photonView;
   
   // set the networkplayer features 
   public Transform head;
   public Transform leftHand;
   public Transform rightHand;

   private void Start()
   {
      photonView = GetComponent<PhotonView>();
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
         mapNetworkPlayerPosition(head,XRNode.Head);
         mapNetworkPlayerPosition(leftHand,XRNode.LeftHand);
         mapNetworkPlayerPosition(rightHand,XRNode.RightHand);
      }
   }

   // get the inputs from the XR devices and apply it to the network player
   void mapNetworkPlayerPosition(Transform target, XRNode node)
   {
      // position values
      InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
      // rotation values
      InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);
      // apply the above values to the target.
      target.position = position;
      target.rotation = rotation;
   }
}
