using System;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Creates a custom editor GUI for a value to set on a Blackboard object.
	///</summary>
	[Serializable]
	public class BlackboardValueEditor_Vector2 : BlackboardValueEditor<Vector2>
	{

		public override void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label)
        {
			MuffinDevGUI.ComputeLabelledFieldRects(_Position, out Rect labelRect, out Rect fieldRect);

			// Key field
			SetKey(_Item, EditorGUI.TextField(labelRect, GetKey(_Item)));
			// Value field
			Vector2 currentValue = GetValue(_Item);
			Vector2 newValue = EditorGUI.Vector2Field(fieldRect, string.Empty, currentValue);
			if (currentValue != newValue)
				SetValue(_Item, newValue);
		}

	}

}