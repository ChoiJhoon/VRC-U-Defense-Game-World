
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Score : UdonSharpBehaviour
{

	private int gameScore = 0;

    void Start()
    {
		ScoreDisplay(gameScore);
	}
	
	[SerializeField]
	private TextMeshPro ScoreText;

	public void ScoreDisplay(int Score)
	{
		ScoreText.text = $"</color><color=#ffffff>Score: <color=#ffffff>{Score:#,##0}</color>";
	}
}
