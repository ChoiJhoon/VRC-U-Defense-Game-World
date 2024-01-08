
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
		public float force = 10;

	public void oneShoot()
	{
		Rigidbody rb;

		GameObject Temp = VRCInstantiate(bulletObj);

		Temp.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);//탄환 생성되는 곳이 GunBody

		rb = Temp.GetComponent<Rigidbody>();

		if (rb == null)
		{
			Debug.LogError("Rigidbody component not found on the object.");
		}

		// Apply a force forward once at the start
		Vector3 forceDirection = transform.forward;
		rb.AddForce(forceDirection * force, ForceMode.Impulse);

		SendCustomEventDelayedSeconds("oneShoot", gunShootingSpeed);//oneShoot 반복
		//Tip
		//처음에 SendEvent를 주면 바로 총알이 날라가는 건 아님.
		//이유 : 처음에 실행할 때 SendCustomEventDelayedSeconds으로 실행된다.

	}

	private void Update()
	{
		
	}

	public override void OnPickup()
	{
		//들자마자 총이 발사되는
		//while문 언젠가 종료가 되어야함.

		//VRC내에 몇초 후에 실행을 시켜라는 아이
		oneShoot();
		//SendCustomEventDelayedSeconds("oneShoot", gunShootingSpeed);//이벤트로 인식시키려면 public이여야한다.
		//또는 업데이트를 하여서 조건문을 걸어줘서 시간이 지나는 걸 측정시키는 것		
	}

	//숙제. 드랍시 총알이 안날라가게.

	/*
	public TFTSystemScript tftSysyem;
	public GameObject bulletObj; //탄환
	public GameObject firePos;
	public float fireRate = 1.5f; //발사 간격(초단위)
	float nextFireTime = 1f; //다음 발사시간?
	public float J = 1;
	
	private bool isFiring = false;
	
	
	//PickUp 시 계속 발사
	public void Trigger()
	{
		StartFiring();//탄환발사 시스템
		tftSysyem.loltftSystem();//TFT 증강시스템 코드 가져오기
	}
	
	//탄환 발사 시스템
	public void StartFiring()
	{
		if (!isFiring)
		{
			isFiring = true;
			//StartCoroutine(FireLoop()); Udon은 StartCoroutine이 없음
			Instantiate(bulletObj, firePos.transform.position, firePos.transform.rotation);
			transform.Translate(Vector3.forward * J);
		}
	}

	public void StopFiring()
	{
		isFiring = false;
	}
	
	void Update()
	{
		if (isFiring && Time.time >= nextFireTime)
		{
			Fire();
			nextFireTime = Time.time + fireRate;
		}
	}

	/*private IEnumerator FireLoop() 
	{
		while (isFiring)
		{
			Fire();//탄환 발사
			yield return new WaitForSeconds(fireRate);
		}
		yield return null;
	}
	*/
	//발사 transform에 따라 탄환을 생성

	//private void Fire()
	//{
	//	Instantiate(bulletObj);
	//}

}