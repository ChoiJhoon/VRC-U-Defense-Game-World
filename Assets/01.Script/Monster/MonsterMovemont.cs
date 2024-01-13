using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MonsterMovemont : UdonSharpBehaviour
{
    public float MoveSpeed = 0.5f;//몬스터의 이동 속도
    private float Velocity = 0f;//위치값

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

    public GameObject objDestroy; // A 오브젝트를 Inspector에서 연결
    private void OnTriggerEnter(Collider other){
        if (other.gameObject == objDestroy){
            Destroy(gameObject); // A에서 오브젝트 제거됨
        }
    }
    
}