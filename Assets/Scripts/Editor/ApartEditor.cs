using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

using System.Collections;



[CustomEditor(typeof(Apart), true)]
[CanEditMultipleObjects]
public class ApartEditor : Editor
{

 	SerializedProperty m_completedFloor;
// 
// 	SerializedProperty m_rectApartFrame;
// 	SerializedProperty m_rectFloors;
// 
// 	SerializedProperty m_rectCrain;
// 	SerializedProperty m_rectOthers;

	
	void OnEnable()
	{
 		m_completedFloor = serializedObject.FindProperty("m_completedFloor");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Apart apart = (Apart)target;
		if (apart != null && m_completedFloor != null)
			apart.CompletedFloor(m_completedFloor.intValue);
	}
}
