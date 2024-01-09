
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using System.Collections;
using VRC.Udon.Common;

public class gun : UdonSharpBehaviour
{
    public float ReloadTime = 0.2f;

    bool GunShotCheck;
    bool GunShotCheckOnce;
    bool CheckEnd;
    public GameObject Gun;
    float time;
    public AudioSource gunsound;
    int Computer = 0;
    public GameObject Aim;

    void Start()
    {
        CheckEnd = true;
    }
    private void Update()
    {
        
        if (GunShotCheck)
        {
            if (GunShotCheckOnce)
            {
                gunsound.Play();
                time = Time.time;
                GunShotCheckOnce = false;

            }

            if (time + ReloadTime < Time.time) { 
                Gun.SetActive(false);
                GunShotCheck = false;
                CheckEnd = true ;
            }
        }
    }
    public override void OnPickup()
    {
        Aim.SetActive(true);
    }
    public override void OnPickupUseDown()
    {
        if (Networking.LocalPlayer.IsUserInVR() == false)
        {
            if (Computer > 0)
            {
                if (CheckEnd)
                {
                    CheckEnd = false;
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "GunShot");

                }
            }
            else
                Computer++;
        }
        else
        {
            if (CheckEnd)
            {
                CheckEnd = false;
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "GunShot");

            }
        }
    }
    public void GunShot()
    {
        Gun.SetActive(true);
        GunShotCheck = true;
        GunShotCheckOnce = true;
       
    }
    public override void OnDrop() { 
        if(Networking.LocalPlayer.IsUserInVR()==false)
            Computer = 0;
        Aim.SetActive(false);
    }
}
