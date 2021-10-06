using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// this class applies the current positions of the body parts of the player avatar to the network player.
public class NetworkPlayer : MonoBehaviour
{
   // set the networkplayer features 
   public Transform head;
   public Transform leftHand;
   public Transform rightHand;

   private void Update()
   {
      // map each transform 
      mapNetworkPlayerPosition(head,XRNode.Head);
      mapNetworkPlayerPosition(leftHand,XRNode.LeftHand);
      mapNetworkPlayerPosition(rightHand,XRNode.RightHand);
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
