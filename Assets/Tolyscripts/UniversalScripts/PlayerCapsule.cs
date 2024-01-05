using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[AddComponentMenu("")]
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class PlayerCapsule : UdonSharpBehaviour
{
    private VRCPlayerApi localPlayer;
    public Transform Head;
    public Transform Root;
    public float leeway = 0.15f;

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }

    private void PostLateUpdate()
    {
        Head.position = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
        Head.rotation = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
        Root.position = localPlayer.GetPosition();
        var scale = (Vector3.Distance(Head.position, Root.position) / 2) + leeway;
        Root.localScale = new Vector3(scale, scale, scale);
    }
}
