using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]

public class Life : UdonSharpBehaviour
{
    public GameObject Monsters;
    private int count = 0;
    public int maxCount = 5;//PlayerHP
    public int StartLife = 5;//시작 LIfe
    private bool isEnabled = true; //다시시작을 위해 Destroy에서 isEnabled로 변경
    
    public GameObject GameOVER;

    public GameObject GameScene;
    
    private void OnTriggerEnter(Collider other)
    {
        //충돌한 오브젝트가 몬스터가 움직이는 스크립트를 가지고 있는지 확인
        MonsterMovemont MobsDamageSystem = other.gameObject.GetComponent<MonsterMovemont>();
        //가지고 있다면 데미지를 주어 count가 됨.
        if (MobsDamageSystem != null)
        {
            maxCount--;
        }
        if(count == maxCount)
        {
            //Destroy(gameObject); //이걸 쓰면 제거됨
            isEnabled = false;
            GameScene.SetActive(isEnabled); //오브젝트 비활성화
			GameOVER.SetActive(true);
			//Destroy(GameScene);
		}
        
        //게임 시작 전 Life 초기화
        if(GameOVER == true && count == maxCount)
        {
            maxCount = StartLife;
        }
    }
}
