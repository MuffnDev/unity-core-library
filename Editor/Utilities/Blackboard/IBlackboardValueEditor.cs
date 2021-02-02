using System;

using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// 
	///</summary>
	public interface IBlackboardValueEditor
	{

		/// <summary>
		/// Draws the GUI of the value (using IMGUI).
		/// </summary>
		/// <param name="_Position">The position and available space to draw the GUI.</param>
		/// <param name="_Item">The serialized property that represent an entry of a blackboard.</param>
		/// <param name="_Label">The label of the property.</param>
		void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label);

		/// <summary>
		/// Gets the expected height of the field in the editor.
		/// </summary>
		/// <param name="_Item">The serialized property that represent an entry of a blackboard.</param>
		/// <param name="_Label">The label of the property.</param>
		float GetPropertyHeight(SerializedProperty _Item, GUIContent _Label);

		/// <summary>
		/// Creates a VisualElement to draw the GUI of the item.
		/// </summary>
		/// <param name="_Item">The serialized property that represent an entry of a blackboard.</param>
		VisualElement CreatePropertyGUI(SerializedProperty _Item);

		Type ValueType { get; }

	}

}