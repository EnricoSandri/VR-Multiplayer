using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// this class extends XRGrabInteractable so when the player grabs an object it requests ownership.
public class XRGrabNetworkInteractable : XRGrabInteractable
{
    private PhotonView photonView;
   
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    // override the "grabfunction" so to ask and transfer the ownership 
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        photonView.RequestOwnership();
        base.OnSelectEntered(interactor);
    }
}
