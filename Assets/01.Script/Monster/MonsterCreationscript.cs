using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System.Diagnostics;
[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]

public class MonsterCreationscript : UdonSharpBehaviour
{
    const float create_interval = 0.18f;//생성 시간
    float mCreatTime = 0; //객채 생성 이후 시간 
    float mTotalTime = 0;//경과한 전체 시간
    float mNextCreateInterval = create_interval;//다음 객체 생성까지의 간격
	int mPhase = 1; //
	public GameObject Monsters;//
	[SerializeField]
	private float MobGenSpeed;

    public float x1 = 0f;
    public float x2 = 0f;

	void Start()
    {
    }

    void Update()
    {
        mTotalTime += Time.deltaTime;//경과한 전체 시간을 증가
        mCreatTime += Time.deltaTime;//마지막 객체 생성 이후의 시간을 증가

        //객체를 생성할 시간인지 확인시켜줌
        if (mCreatTime > mNextCreateInterval){
            mCreatTime = 0;//마지막 객체 생성 이후의 시간을 재설정
            mNextCreateInterval = create_interval - (MobGenSpeed * mTotalTime);//경과한 시간을 기반으로 다음 객체 생성 간격 업데이트

            //다음 간격이 최소 임계값보다 낮아지지 않도록 해주는 코드
            if (mNextCreateInterval < MobGenSpeed){
                mNextCreateInterval = MobGenSpeed;
            }
            for (int i = 0; i < mPhase && i < 5; i++){
                creatMonster(8f + i * 0.2f);
            }
        }
    }
    private void creatMonster(float z){
        float y = 0.5f;
        float x = Random.Range(x1, x2);//지정된 범위 내에서 무작위 x좌표 생성하는 코드[수정필요]
        createObject(Monsters, new Vector3(x, y, z), Quaternion.identity);// 주어진 위치와 회전으로 GameObject를 생성하는 메서드
        }

    //주어진 위치와 회전으로 Object를 생성하는 메서드
    private GameObject createObject(GameObject original, Vector3 position, Quaternion rotation){
        return (GameObject)Instantiate(original, position, rotation);
    }
}
