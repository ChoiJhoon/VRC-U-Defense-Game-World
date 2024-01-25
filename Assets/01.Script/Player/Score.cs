
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Score : UdonSharpBehaviour
{

	private int gameScore = 0;

	[SerializeField]
	public GameObject MobsObject;//프리팹 넣기

	[SerializeField]
	private TextMeshPro ScoreText;

	void Start()
	{
		ScoreDisplay(gameScore);
	}

	public void ScoreDisplay(int Score)
	{
		ScoreText.text = $"</color><color=#ffffff>Score: <color=#ffffff>{Score:#,##0}</color>";
	}

	public void AddScoreOnMonsterDeath(int points)
	{
		gameScore += points;
		ScoreDisplay(gameScore);
		//몹이 죽을 때 로직 추가 작성 가능.
	}
}
