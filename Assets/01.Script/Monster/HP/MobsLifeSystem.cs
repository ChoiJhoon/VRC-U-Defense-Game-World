
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]

public class MobsLifeSystem : UdonSharpBehaviour
{
	private int count = 0;
	public int maxCount = 1; //Mobs Hp
	//점수가 올라갈 때 마다 HP 올려주기

	private void OnTriggerEnter(Collider other)

	{
		//충돌한 오브젝트가 몬스터가 움직이는 스크립트를 가지고 있는지 확인
		BulletDestroy GunsDamageSystem = other.gameObject.GetComponent<BulletDestroy>();

		//가지고 있다면 데미지를 주어 count가 됨.
		if (GunsDamageSystem != null)
		{
			maxCount--;
			DisplayHP(maxCount);
		}

		if (count == maxCount)
		{
			Destroy(gameObject); //이걸 쓰면 제거됨
		}
	}

	//HP
	public TextMeshPro HPText;  //인스펙터에서 연결해야함.

	public void DisplayHP(int HPS)
	{
		HPText.text = $"</color><color=#ff0000>{HPS:#,##0}</color>";
	}

	void Start()
	{
		DisplayHP(maxCount);
	}

}
