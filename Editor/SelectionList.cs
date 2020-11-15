using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using System.Linq;

namespace MuffinDev.EditorUtils
{

	///<summary>
	/// Draws a list with selectable labels. This editor utility is quite a simplified version of Unity's IMGUI Tree view.
	///</summary>
	[System.Serializable]
	public class SelectionList
	{

		#region Properties

		/// <summary>
		/// Used to send feedback when the selected item of this selection list changes.
		/// </summary>
		/// <param name="_Index">The index of the selected item in the list.</param>
		/// <param name="_Item">The content of the selected item.</param>
		/// <param name="_LastSelectedIndex">The previous selected item index in the list.</param>
		public delegate void SelectItemDelegate(int _Index, string _Item, int _LastSelectedIndex);

		[SerializeField]
		private List<string> m_Items = new List<string>();

		[SerializeField]
		private int m_SelectedIndex = 0;

		[SerializeField]
		private Vector2 m_ScrollPosition = Vector2.zero;

		private SelectItemDelegate m_OnSelectItem = null;

		private static GUIStyle s_ItemsListStyle = null;
		private static GUIStyle s_SelectedItemStyle = null;
		private static GUIStyle s_UnselectedItemStyle = null;

		#endregion


		#region Initialization

		/// <summary>
		/// Creates a SelectionList instance.
		/// </summary>
		public SelectionList() { }

		/// <summary>
		/// Creates a SelectionList instance, and initializes its items list with the given collection.
		/// </summary>
		/// <param name="_Items">The array or list of items you want to set for this selection list.</param>
		public SelectionList(IEnumerable<string> _Items)
        {
			m_Items = new List<string>(_Items);
		}

		#endregion


		#region GUI

		/// <summary>
		/// Draws the selection list using -Layout methods.
		/// </summary>
		public void DrawLayout()
		{
			DrawLayout(ItemsListStyle);
		}

		/// <summary>
		/// Draws the selection list using -Layout methods.
		/// </summary>
		/// <param name="_Style">The style to use for the items list.</param>
		public void DrawLayout(GUIStyle _Style)
        {
			m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition, _Style);
            {
				for (int i = 0; i < Items.Count(); i++)
				{
					DrawItemLayout(i, Items.ElementAt(i));
				}
			}
			EditorGUILayout.EndScrollView();
		}

		/// <summary>
		/// Draws an item of the list.
		/// </summary>
		/// <param name="_Index">The index of the drawn item.</param>
		/// <param name="_Item">The string value of the drawn item.</param>
		private void DrawItemLayout(int _Index, string _Item)
		{
			bool clicked = (_Index == SelectedIndex)
				? GUILayout.Button(_Item, SelectedItemStyle, GUILayout.ExpandWidth(true))
				: GUILayout.Button(_Item, UnselectedItemStyle, GUILayout.ExpandWidth(true));

			if (clicked)
			{
				SelectedIndex = _Index;
			}
		}

		#endregion


		#region Accessors

		/// <summary>
		/// Gets/Sets the items list.
		/// </summary>
		public IEnumerable<string> Items
		{
			get { return m_Items.AsEnumerable(); }
			set { m_Items = new List<string>(value); }
		}

		public SelectItemDelegate OnSelectItem
        {
            get { return m_OnSelectItem; }
            set { m_OnSelectItem = value; }
        }

		/// <summary>
		/// Gets/Sets the index of the selected item.
		/// </summary>
		private int SelectedIndex
		{
			get
			{
				if (m_SelectedIndex >= m_Items.Count)
				{
					int lastSelectedIndex = m_SelectedIndex;
					m_SelectedIndex = 0;
					if (OnSelectItem != null)
						OnSelectItem.Invoke(m_SelectedIndex, m_Items[m_SelectedIndex], lastSelectedIndex);
				}
				return m_SelectedIndex;
			}
			set
            {
				int lastSelectedIndex = m_SelectedIndex;
				m_SelectedIndex = value;
				if(OnSelectItem != null)
					OnSelectItem.Invoke(m_SelectedIndex, m_Items[m_SelectedIndex], lastSelectedIndex);
            }
		}

		/// <summary>
		/// Gets the default style of the items list.
		/// </summary>
		public static GUIStyle ItemsListStyle
		{
			get
			{
				if (s_ItemsListStyle == null)
				{
					s_ItemsListStyle = new GUIStyle("OL box");
					s_ItemsListStyle.padding = new RectOffset();
					s_ItemsListStyle.margin = new RectOffset(6, 6, 6, 6);
				}
				return s_ItemsListStyle;
			}
		}

		/// <summary>
		/// Gets the style for selected items.
		/// </summary>
		public static GUIStyle SelectedItemStyle
		{
			get
			{
				if (s_SelectedItemStyle == null)
				{
					s_SelectedItemStyle = new GUIStyle("OL SelectedRow");
					s_SelectedItemStyle.padding = new RectOffset(6, 4, 4, 4);
					s_SelectedItemStyle.margin = new RectOffset();
				}
				return s_SelectedItemStyle;
			}
		}

		/// <summary>
		/// Gets the style for unselected items.
		/// </summary>
		public static GUIStyle UnselectedItemStyle
		{
			get
			{
				if (s_UnselectedItemStyle == null)
				{
					s_UnselectedItemStyle = new GUIStyle(EditorStyles.label);
					s_UnselectedItemStyle.padding = new RectOffset(6, 4, 4, 4);
					s_UnselectedItemStyle.margin = new RectOffset();
				}
				return s_UnselectedItemStyle;
			}
		}

		#endregion

	}

}