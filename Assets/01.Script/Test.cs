using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;



public class Test : UdonSharpBehaviour
{
     public float Rspeed = 100.0f; //회전, 이동 할 속도
    public float Pspeed = 1.0f;

    public Vector3 targetPosition = new Vector3(0, 0, 0); //이동할 좌표
    private Transform tf; //유니티 Transform 컴포넌트 정보
    void Start(){
        tf = gameObject.transform;
    }
    void FixedUpdate(){        
        tf.Rotate(new Vector3(0,1,1), Time.deltaTime * Rspeed); // 회전축 x:0, y:1, z:1 만큼 지속적으로 돌아감 
        tf.position = Vector3.MoveTowards(tf.position, targetPosition, Time.deltaTime * Pspeed);// 좌표
    }
}
