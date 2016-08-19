using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour 
{

	public FirstScene m_firstScene = null;
	public GamePlay m_secondScene = null;
	public RectTransform m_gameOverScene = null;


	void OnEnable()
	{
	}


	public void PlayFirstScene()
	{
		m_firstScene.gameObject.SetActive(true);
		m_secondScene.gameObject.SetActive(false);
		m_gameOverScene.gameObject.SetActive(false);
	}

	public void PlayGameScene()
	{
		m_firstScene.gameObject.SetActive(false);
		m_secondScene.gameObject.SetActive(true);
		m_gameOverScene.gameObject.SetActive(false);
	}

	public void PlayGameOver()
	{
		m_firstScene.gameObject.SetActive(false);
		m_gameOverScene.gameObject.SetActive(true);
	}
}
