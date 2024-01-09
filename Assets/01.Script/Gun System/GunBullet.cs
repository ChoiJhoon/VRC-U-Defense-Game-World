using System.Collections;
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

	/*
	public override void OnPickup()
	{
		//들자마자 총이 발사되는
		//while문 언젠가 종료가 되어야함.

		//VRC내에 몇초 후에 실행을 시키는 코드
		//oneShoot();
		canShoot = true;
		if (canShoot == true)
		{
			SendCustomEventDelayedSeconds(nameof(ShootAfterDelay), gunShootingSpeed);
		}
		else
		{
			return;
		}
		//SendCustomEventDelayedSeconds("oneShoot", gunShootingSpeed);//이벤트로 인식시키려면 public이여야한다.
		//또는 업데이트를 하여서 조건문을 걸어줘서 시간이 지나는 걸 측정시키는 것		
	}*/

	public override void OnPickup()
	{
		//원래 oneShoot();였지만 bool로 isShooting 발사여부 체크를 한다.
		isShooting = true; //
		oneShoot(); //최초 발사
	}

	//숙제. 드랍시 총알이 안날라가게.
	public override void OnDrop()
	{
		isShooting = false; // 발사 중지
	}

	//나중. OnPickupUseDown을 사용해서 총알 ON/OFF 시스템 작성

}

