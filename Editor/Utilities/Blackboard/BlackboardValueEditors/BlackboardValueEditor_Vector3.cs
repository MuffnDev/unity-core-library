using System;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Creates a custom editor GUI for a value to set on a Blackboard object.
	///</summary>
	[Serializable]
	public class BlackboardValueEditor_Vector3 : BlackboardValueEditor<Vector3>
	{

		public override void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label)
        {
			MuffinDevGUI.ComputeLabelledFieldRects(_Position, out Rect labelRect, out Rect fieldRect);

			// Key field
			SetKey(_Item, EditorGUI.TextField(labelRect, GetKey(_Item)));
			// Value field
			Vector3 currentValue = GetValue(_Item);
			Vector3 newValue = EditorGUI.Vector3Field(fieldRect, string.Empty, currentValue);
			if (currentValue != newValue)
				SetValue(_Item, newValue);
        }

	}

}