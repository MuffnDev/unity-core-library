using System;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Creates a custom editor GUI for a value to set on a Blackboard object.
	///</summary>
	[Serializable]
	public class BlackboardValueEditor_Gradient : BlackboardValueEditor<Gradient>
	{

		public override void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label)
        {
			Gradient currentValue = GetValue(_Item);
			Gradient newValue = EditorGUI.GradientField(_Position, _Label, currentValue);
			if (currentValue != newValue)
				SetValue(_Item, newValue);
        }

	}

}