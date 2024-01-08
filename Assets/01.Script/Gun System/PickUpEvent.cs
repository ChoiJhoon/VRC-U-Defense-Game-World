
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PickUpEvent : UdonSharpBehaviour
{
	public UdonBehaviour gunpickUp;//스크립트
	public string gunpickupEvent = "Trigger";

	//모든 사람들에게 보내질 필요가 없다.
	public override void OnPickup()
	{
		gunpickUp.SendCustomEvent(gunpickupEvent);
		//Networking.
	}
	
	
}
