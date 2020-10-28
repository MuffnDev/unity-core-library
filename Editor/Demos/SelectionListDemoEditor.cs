using UnityEngine;
using UnityEditor;

namespace MuffinDev.EditorUtils.Demos
{

	///<summary>
	/// This editor window demonsttrates how to use the SelectionList Editor GUI utility.
	///</summary>
	public class SelectionListDemoEditor : EditorWindow
	{

		#region Properties

		private const string EDITOR_WINDOW_MENU_ITEM = "Tools/Muffin Dev/Demos/Selection List";
		private const string EDITOR_WINDOW_TITLE = "Selection List";
		private const int EDITOR_WINDOW_PRIORITY = 1;

		[SerializeField]
		private string[] m_Items = { };

		[SerializeField]
		private SelectionList m_SelectionList = null;

		[SerializeField]
		private Vector2 m_ScrollPosition = Vector2.zero;

		private string m_LastSelectedItem = string.Empty;
		private SerializedProperty m_ItemsProp = null;

		#endregion


		#region Lifecycle

		private void OnEnable()
		{
			if (m_Items == null || m_Items.Length == 0)
			{
				m_Items = new string[] { "Apple", "Banana", "Lemon" };
			}

			m_SelectionList = new SelectionList(m_Items);
			m_SelectionList.OnSelectItem += OnSelectItem;

			SerializedObject obj = new SerializedObject(this);
			m_ItemsProp = obj.FindProperty(nameof(m_Items));
		}

		#endregion


		#region GUI

		private void OnGUI()
		{
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition, GUILayout.Height(120f));
            {
                m_SelectionList.Items = m_Items;
                EditorGUILayout.PropertyField(m_ItemsProp, true);
                m_ItemsProp.serializedObject.ApplyModifiedProperties();
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();
			GUI.enabled = false;
			EditorGUILayout.TextField("Last Selected Item", m_LastSelectedItem);
			GUI.enabled = true;
            m_SelectionList.DrawLayout();
		}

		/// <summary>
		/// Gets the window of this tool, or create it if it's not already open.
		/// </summary>
		[MenuItem(EDITOR_WINDOW_MENU_ITEM, false, EDITOR_WINDOW_PRIORITY)]
		private static void ShowWindow()
		{
			SelectionListDemoEditor window = GetWindow<SelectionListDemoEditor>(false, EDITOR_WINDOW_TITLE, true) as SelectionListDemoEditor;
			window.Show();
		}

        #endregion


        #region Private methods

		private void OnSelectItem(int _Index, string _Item, int _LastSelectedIndex)
        {
			m_LastSelectedItem = _Item;
		}

        #endregion

    }

}