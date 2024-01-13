using System.Collections;
using System.Security.Cryptography;
using UdonSharp;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using VRC.SDKBase;
using VRC.Udon;
[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]

public class GunBullet : UdonSharpBehaviour

{
	public float gunShootingSpeed = 1.0f;//공격속도
	public GameObject bulletObj;//탄환
	public float force = 10; //탄속

	private bool isShooting = false; // 발사 여부

	public void oneShoot()
	{
		if (!isShooting) return;

		Rigidbody rb;

		GameObject Temp = VRCInstantiate(bulletObj);

		Temp.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);//탄환 생성되는 곳이 GunBody

		rb = Temp.GetComponent<Rigidbody>();

		if (rb == null)
		{
			Debug.LogError("Rigidbody component not found on the object.");
		}

		//Apply a force forward once at the start
		Vector3 forceDirection = transform.forward;
		rb.AddForce(forceDirection * force, ForceMode.Impulse);

		SendCustomEventDelayedSeconds("oneShoot", gunShootingSpeed);

		//Tip
		//처음에 SendEvent를 주면 바로 총알이 날라가는 건 아님.
		//이유 : 처음에 실행할 때 SendCustomEventDelayedSeconds으로 실행된다.
	}


	public override void OnPickup()
	{
		//원래 oneShoot();였지만 bool로 isShooting 발사여부 체크를 한다.
		isShooting = true; //
		
		oneShoot(); //최초 발사
	
	}

	public override void OnDrop()
	{
		isShooting = false; // 발사 중지
	}

	//1. update
	//2. ISR 방식 : gunShootingSpeed + 마우스 클릭한 시간(shootingTIme)
	//3. time 쪼개기
	//0.02초로 나눠서 fixedUpdate
}

//나중. OnPickupUseDown을 사용해서 총알 ON/OFF 시스템 작성
