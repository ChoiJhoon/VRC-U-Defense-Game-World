
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class PickupEventTransferer : UdonSharpBehaviour
{
    public UdonBehaviour targetBehaviour;
    public string pickupUseDownEvent = "TriggerPull";
    public string pickupUseUpEvent = "TriggerRelease";
    public string pickupDropEvent = "Drop";
    public string pickupEvent = "Pickup";
    public void OnPickupUseDown()
    {
        targetBehaviour.SendCustomEvent(pickupUseDownEvent);
    }

    public void OnPickupUseUp()
    {
        targetBehaviour.SendCustomEvent(pickupUseUpEvent);
    }

    public void OnDrop()
    {
        targetBehaviour.SendCustomEvent(pickupDropEvent);
    }
    public void OnPickup()
    {
        Networking.SetOwner(Networking.LocalPlayer, targetBehaviour.gameObject);
        targetBehaviour.SendCustomEvent(pickupEvent);
    }
}
