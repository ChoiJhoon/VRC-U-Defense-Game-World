
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class particle : UdonSharpBehaviour
{
    public Vector3 O_Vector3;
    
    Quaternion Quater;

    void Start()
    {
    }
    public override void OnPlayerParticleCollision(VRCPlayerApi player)
    {
        player.TeleportTo(O_Vector3, Quater);
    }

}
