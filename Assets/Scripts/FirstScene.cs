using UnityEngine;
using System.Collections;

public class FirstScene : MonoBehaviour 
{
	public GameLogic m_gameLogic = null;
	void OnEnable()
	{
		Invoke("hideScene", 2.0f);
	}

	void hideScene()
	{
		m_gameLogic.PlayGameScene();
	}
}
