
using System.Collections;
using System.Security.Cryptography;
using UdonSharp;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using VRC.SDKBase;
using VRC.Udon;

public class ShootingSystem : UdonSharpBehaviour
{
    public float gunAttackSpeed = 1.0f; //공격속도
    public GameObject bulletObject; //탄환
    public float force = 10; //탄속

    private bool boolShooting = false; //발사여부
	private bool isShooting = false; // 발사 중 여부 추가
	private bool boolOnDrop = false; //Drop 여부
	private float shootingTimer = 0.0f;

    private Vector3 initialPosition; //초기위치
    private Quaternion initialRotation; //초기방향

	public GameObject gameOVER;//게임오버 넣기
	public GameObject gamePlaying;//게임플레이
	private bool UpdateShooting;//Update에 넣을 Shooting

	void Start()
    {
        //총의 최초 위치 및 방향 저장
	    initialPosition = transform.position;
		initialRotation = transform.rotation;
	}

	void Update()
	{
		//게임이 종료되면 총알이 발사가 되지 않게.
		//게임중에는 Drop시 발사가 안되게.
		//게임이 OVER되고 Start를 할 경우 발사가 되도록.
		//게임오버.
		if (gameOVER.activeSelf || boolOnDrop == false)
		{
			boolShooting = false;
			Debug.Log("게임 오버 또는 Drop");
		}
		// 게임 중이고 Drop 중이면
		else if (gamePlaying.activeSelf && boolOnDrop)
		{
			boolShooting = false;
			Debug.Log("게임 진행 중 Drop함");
		}
		// 게임이 OVER되고 Start를 할 경우
		else if (gameOVER.activeSelf && !gamePlaying.activeSelf)
		{
			boolShooting = true;
			boolOnDrop = false; // Drop 여부 초기화
			//FristShooting(); // 최초 발사
			shootingTimer = 0.0f;
			Debug.Log("게임 오버 후 게임 스타트 버튼 On");
		}

		if (isShooting)
		{
			shootingTimer += Time.deltaTime;
			
			if (shootingTimer >= gunAttackSpeed)
			{
				//FristShooting();
				Debug.Log("최초 발사");
				shootingTimer = 0.0f; // 타이머 초기화
			}
		}
	}

	public void FristShooting()
	{
		if (gameOVER.activeSelf)
		{
			Debug.Log("FristShooting Return");
			return;
		}
		
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

		SendCustomEventDelayedSeconds("FristShooting", gunAttackSpeed);
	}
	
	public override void OnPickup()
	{
		//발사 시작
		if (!isShooting) // 발사 중이 아닌 경우에만 실행
		{
			boolOnDrop = false; // Drop 중이 아님
			isShooting = true; // 발사 중으로 설정
			FristShooting(); // 최초 발사
		}
	}

	public override void OnDrop()
	{
		// 발사 중지
		Debug.Log("발사 중지");
		boolOnDrop = true;
		isShooting = false; // 발사 중이 아님으로 설정
	}
}