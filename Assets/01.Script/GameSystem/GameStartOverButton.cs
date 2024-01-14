
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]

public class GameStartOverButton : UdonSharpBehaviour
{
    public GameObject GameStart;
    public GameObject GameOver;

    public GameObject GameScene;//게임판
	[SerializeField]
	private bool isOn = false;//게임판

	private void GamePlaying()
    {
        if (GameStart)
        {
            GameScene.SetActive(true);
            Destroy(GameStart);
		}
        else if (GameOver)
        {
            GameScene.SetActive(true);
            Destroy(GameOver);
        }
    }

}