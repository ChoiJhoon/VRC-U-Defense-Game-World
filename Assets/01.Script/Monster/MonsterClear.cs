
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MonsterClear : UdonSharpBehaviour
{
	public GameObject objPrefab;
	public GameObject DestroyObj;

	void Start()
	{

	}

	void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == DestroyObj)
		{
			Destroy(gameObject); // A에서 오브젝트 제거됨
		}
	}
}
