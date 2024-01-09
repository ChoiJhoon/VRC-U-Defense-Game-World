
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BulletDestroy : UdonSharpBehaviour
{
	public float maxDistance = 30.0f; // 총알의 최대 거리
	private Vector3 initialPosition; // 총알의 초기 위치

	void Start()
	{
		initialPosition = transform.position; // 총알이 생성된 초기 위치 저장
	}

	void Update()
	{
		float distanceTravelled = Vector3.Distance(initialPosition, transform.position); // 초기 위치에서 현재 위치까지의 거리 계산
		if (distanceTravelled >= maxDistance)
		{
			Destroy(gameObject); // 총알 파괴
		}
	}
}
