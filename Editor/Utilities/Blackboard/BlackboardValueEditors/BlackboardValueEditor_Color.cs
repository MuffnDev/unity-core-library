using System;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Creates a custom editor GUI for a value to set on a Blackboard object.
	///</summary>
	[Serializable]
	public class BlackboardValueEditor_Color : BlackboardValueEditor<Color>
	{

		public override void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label)
        {
			Color currentValue = GetValue(_Item);
			Color newValue = EditorGUI.ColorField(_Position, _Label, currentValue);
			if (currentValue != newValue)
				SetValue(_Item, newValue);
        }

	}

}