using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MonsterMovemont : UdonSharpBehaviour
{
    private float Move = 1f;//몬스터의 이동 속도
    private float Velocity = 0f;//위치값
    public int MobsHpCount = 0; //몬스터들의 HP

    void Start()
    {

    }

    void Update()
    {
        Vector3 current = this.transform.position;

        Velocity += Move * Time.deltaTime;

        current.z -= Velocity * Time.deltaTime;
        this.transform.position = current;
    }
    public GameObject objDestroy; // A 오브젝트를 Inspector에서 연결
    private void OnTriggerEnter(Collider other){
        if (other.gameObject == objDestroy){
            Destroy(gameObject); // A에서 오브젝트 제거됨
        }
    }
    
}