//#define BLACKBOARD_DEMO // Uncomment this line in order to enable the BlackboardValueEditor_PlayerData custom value from Blackboard editor
#if BLACKBOARD_DEMO

using System;

using UnityEngine;
using UnityEditor;

using MuffinDev.Core.EditorOnly;

namespace MuffinDev.Core.Demos
{

	///<summary>
	/// Creates a custom editor GUI for a value to set on a Blackboard object.
	///</summary>
	[Serializable]
	public class BlackboardValueEditor_PlayerData : BlackboardValueEditor<BlackboardAssetDemo.PlayerData>
	{

		public override void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label)
        {
			Rect rect = new Rect(_Position);
			rect.height = MuffinDevGUI.LINE_HEIGHT;
			MuffinDevGUI.ComputeLabelledFieldRects(rect, out Rect labelRect, out Rect fieldRect);

			// Key field
			SetKey(_Item, EditorGUI.TextField(labelRect, GetKey(_Item)));
			// Type label
			EditorGUI.LabelField(fieldRect, $"({ValueType.Name})");

			BlackboardAssetDemo.PlayerData data = GetValue(_Item);
			
			EditorGUI.indentLevel++;
            {
				// Name field
				rect.y += rect.height + MuffinDevGUI.VERTICAL_MARGIN;
				rect = EditorGUI.IndentedRect(rect);
				data.name = EditorGUI.TextField(rect, "Name", data.name);

				// Score field
				rect.y += rect.height + MuffinDevGUI.VERTICAL_MARGIN;
				data.score = EditorGUI.IntField(rect, "Score", data.score);
			}
			EditorGUI.indentLevel--;

			// Save changes
            SetValue(_Item, data);
        }

        public override float GetPropertyHeight(SerializedProperty _Item, GUIContent _Label)
        {
            return MuffinDevGUI.LINE_HEIGHT * 3 + MuffinDevGUI.VERTICAL_MARGIN * 2;
        }

    }

}

#endif