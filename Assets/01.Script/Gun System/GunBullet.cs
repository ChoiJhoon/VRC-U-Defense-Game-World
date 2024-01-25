using System.Collections;
using System.Security.Cryptography;
using UdonSharp;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using VRC.SDKBase;
using VRC.Udon;


public class GunBullet : UdonSharpBehaviour
{
	public GameObject bulletObject; //탄환
	public float force = 30; //탄속
	public bool GunShot = true; //단발
	public bool GunSafe = false; //안전

	private Vector3 initialPosition; //초기위치
	private Quaternion initialRotation; //초기방향

	public GameObject gameOVER;//게임오버 넣기
	public GameObject gamePlaying;//게임플레이


	public override void OnPickupUseDown()
	{
		//isShooting();
		if (GunShot && !GunSafe)
		{
			Debug.Log("격발");
			isShooting();
		}
		else if (!GunShot && GunSafe) 
		{
			Debug.Log("안전");
		}
	}

	public override void OnDrop()
	{
		OnDroping();
	}

	//발사 시스템
	public void isShooting()
	{
		Debug.Log("FristShooting 실행1");

		Rigidbody rb;
		GameObject Temp = VRCInstantiate(bulletObject);
		Temp.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
		rb = Temp.GetComponent<Rigidbody>();

		if (rb == null)
		{
			Debug.LogError("Rigidbody component not found on the object.");
		}

		Debug.Log("FristShooting 실행2");

		Vector3 forceDirection = transform.forward;
		rb.AddForce(forceDirection * force, ForceMode.Impulse);

		//SendCustomEventDelayedSeconds("isShooting");
	}

	void Start()
	{
		//총의 최초 위치 및 방향 저장
		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}

	void Update()
	{
		gamePlayingShotSystem();
	}
	
	public void gamePlayingShotSystem()
	{
		//게임이 종료되면 총알이 발사가 되지 않게.
		//게임중에는 Drop시 발사가 안되게.
		//게임이 OVER되고 Start를 할 경우 발사가 되도록.
		
		//게임오버.
		if (gameOVER.activeSelf)
		{
			GunShot = false;
			GunSafe = true;
			Debug.Log("조준 간 안전");
		}
		else if(gamePlaying.activeSelf) 
		{
			GunShot = true;
			GunSafe = false;
			Debug.Log("조준 간 단발");
		}
	}

	public void OnDroping()
	{
		if (gameOVER.activeSelf)
		{
			transform.position = initialPosition;
			transform.rotation = initialRotation;
		}
	}
}
//나중. OnPickupUseDown을 사용해서 총알 ON/OFF 시스템 작성