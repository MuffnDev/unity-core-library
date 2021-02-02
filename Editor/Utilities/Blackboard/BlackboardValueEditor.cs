using System;

using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Creates a custom editor GUI for a value to set on a Blackboard object.
	///</summary>
	[Serializable]
	public abstract class BlackboardValueEditor
	{

		public virtual void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label) { }

		public virtual float GetPropertyHeight(SerializedProperty _Property, GUIContent _Label)
        {
			return MuffinDevGUI.LINE_HEIGHT;
        }

		public virtual VisualElement CreatePropertyGUI(SerializedProperty _Property)
		{
			return null;
		}

		public abstract Type ValueType { get; }

	}

}