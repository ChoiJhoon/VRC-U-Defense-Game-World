
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class LifePoint : UdonSharpBehaviour
{
    public TMP_Text HPText;  //인스펙터에서 연결해야함.
    private int HPPoint = 0;

    void Start()
    {
        
    }
}
