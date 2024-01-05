
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]

public class GunBullet : UdonSharpBehaviour
{
    public UdonBehaviour aBehaviour;
   
   //PickUp시 마우스를 누를 경우
    public void OnPickupUseDown()
    {
        //여기에 필요한 코드 작성
    }
}