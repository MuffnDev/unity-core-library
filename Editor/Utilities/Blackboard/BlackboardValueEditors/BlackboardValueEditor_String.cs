using System;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Creates a custom editor GUI for a value to set on a Blackboard object.
	///</summary>
	[Serializable]
	public class BlackboardValueEditor_String : BlackboardValueEditor<string>
	{

		public override void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label)
        {
			string currentValue = GetValue(_Item);
			string newValue = EditorGUI.TextField(_Position, _Label, currentValue);
			if (currentValue != newValue)
				SetValue(_Item, newValue);
        }

	}

}