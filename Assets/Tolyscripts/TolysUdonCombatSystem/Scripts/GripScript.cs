
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.Animations;
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class GripScript : UdonSharpBehaviour
{
    private Rigidbody body;
    public ParentConstraint gunHandleParentConstraint;
    public ParentConstraint gunGripParentConstraint;

    private void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    private void OnDrop()
    {
        body.isKinematic = false;
        AttachSelfToGun();
    }

    private void OnPickup()
    {
        body.isKinematic = true;
    }

    public void AttachSelfToGun()
    {
        //make the parent constraint active to return to the default position
        gunHandleParentConstraint.enabled = true;
    }

    public void DetachSelfFromGun()
    {
        //make the parent constraint inactive to allow the grip to be moved around
        gunHandleParentConstraint.enabled = false;
    }

    public void AttachGunToSelf()
    {
        //make the parent constraint active to return to the default position
        gunGripParentConstraint.enabled = true;
    }

    public void DetachGunFromSelf()
    {
        //make the parent constraint inactive to allow the grip to be moved around
        gunGripParentConstraint.enabled = false;
    }
}
