
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]

public class GameStartOverButton : UdonSharpBehaviour
{
    public GameObject GameStart;
    public GameObject GameOver;
    public GameObject MobsClearObj;

    public GameObject GameScene;//게임판
	[SerializeField]
	private bool isOn = false;//게임판

	private void Update()
	{
		if (GameOver.activeSelf)
		{
			MobsClearObj.SetActive(true);
		}
		/*
		else if (GameStart.activeSelf)
		{
			MobsClearObj.SetActive(false);
		}*/
		else if (!GameStart.activeSelf && !GameOver.activeSelf)
		{
			MobsClearObj.SetActive(false);
		}
	}

	private void GamePlaying()
    {
		if (GameStart.activeSelf)
		{
			GameScene.SetActive(true);
			Destroy(GameStart);
		}
		else if (GameOver.activeSelf)
		{
			GameScene.SetActive(true);
			Destroy(GameOver);
		}
	}

}