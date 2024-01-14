using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MonsterMovemont : UdonSharpBehaviour
{
    public float MoveSpeed = 0.5f;//몬스터의 이동 속도
    private float Velocity = 0f;//위치값

	private Vector3 initialPosition; // 몬스터의 초기 위치

	public GameObject gameOVER;//게임오버 넣기

    public GameObject objPrefab;
    public GameObject DestroyObj;//PlayerLife
	public GameObject DestroyObj2;//클리어오브젝트

	void Start()
    {
		
	}

    void Update()
    {
        Vector3 current = this.transform.position;

        Velocity += MoveSpeed * Time.deltaTime;

        current.z -= Velocity * Time.deltaTime;
        this.transform.position = current;
    }
        
    private void OnTriggerEnter(Collider other){
        if (other.gameObject == DestroyObj || other.gameObject == DestroyObj2)
        {
            Destroy(gameObject); // A에서 오브젝트 제거됨
        }
    }
}