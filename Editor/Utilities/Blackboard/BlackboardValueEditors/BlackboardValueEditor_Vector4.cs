using System;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Creates a custom editor GUI for a value to set on a Blackboard object.
	///</summary>
	[Serializable]
	public class BlackboardValueEditor_Vector4 : BlackboardValueEditor<Vector4>
	{

		public override void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label)
        {
			Vector4 currentValue = GetValue(_Item);
			Vector4 newValue = EditorGUI.Vector4Field(_Position, _Label, currentValue);
			if (currentValue != newValue)
				SetValue(_Item, newValue);
        }

	}

}