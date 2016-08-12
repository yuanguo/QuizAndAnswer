using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Apart : MonoBehaviour {


	public int m_completedFloor = 0;

	[SerializeField] RectTransform m_rectApartFrame = null;
	[SerializeField] List<RectTransform> m_rectFloors = new List<RectTransform>();

	[SerializeField] RectTransform m_rectCrain = null;
	[SerializeField] RectTransform m_rectOthers = null;


	public int allFloors
	{
		get { return m_rectFloors.Count; }
	}

	void OnEnable()
	{

	}

	void OnDisable()
	{

	}

	public void CompletedFloor(int _floor)
	{
		m_completedFloor = _floor;

		if (_floor == 0)
		{
			m_rectCrain.gameObject.SetActive(false);
			m_rectOthers.gameObject.SetActive(false);
			m_rectApartFrame.gameObject.SetActive(false);
		}
		else if (_floor > 0)
		{
			m_rectCrain.gameObject.SetActive(true);
			m_rectOthers.gameObject.SetActive(true);
			m_rectApartFrame.gameObject.SetActive(true);

			if (_floor <= m_rectFloors.Count)
			{
				m_rectApartFrame.sizeDelta = new Vector2(m_rectApartFrame.sizeDelta.x,
					m_rectFloors[(_floor-1)].anchoredPosition.y + m_rectFloors[(_floor-1)].sizeDelta.y);

				m_rectOthers.sizeDelta = m_rectApartFrame.sizeDelta;

				m_rectCrain.anchoredPosition = new Vector2(m_rectCrain.anchoredPosition.x,
					m_rectApartFrame.sizeDelta.y - 5);
			}
			else
			{
				m_rectCrain.gameObject.SetActive(false);
				m_rectOthers.gameObject.SetActive(false);
			}
		}
	}
}
