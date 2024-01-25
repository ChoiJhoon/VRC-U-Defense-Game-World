
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

	private Score scoreScript;

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
			/*ScoreManager scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
			if (scoreManager != null)
			{
				scoreManager.AddScore(10);  // 몬스터가 죽을 때마다 10점씩 점수 추가
			}*/
			Die();
			Destroy(gameObject); //이걸 쓰면 제거됨
		}
	}

	//HP
	public TextMeshPro HPText;  //인스펙터에서 연결해야함.
	public GameObject scoreObject;

	public void DisplayHP(int HPS)
	{
		HPText.text = $"</color><color=#ff0000>{HPS:#,##0}</color>";
	}

	void Start()
	{
		if (scoreObject != null)
		{
			scoreScript = scoreObject.GetComponent<Score>();

			if (scoreScript == null)
			{
				Debug.LogError("Score script not found on the assigned object.");
			}
			else
			{
				Debug.Log("Score script successfully assigned.");
			}
		}
		else
		{
			Debug.LogError("Score object not assigned in the Inspector.");
		}

		DisplayHP(maxCount);
	}

	private void Die()
	{
		// 몬스터가 죽을 때의 처리
		if (scoreScript != null)
		{
			scoreScript.AddScoreOnMonsterDeath(1); // 몬스터가 죽을 때마다 1점씩 점수 추가
			Debug.Log("Score added on monster death.");
		}

		Destroy(gameObject);
	}

}
