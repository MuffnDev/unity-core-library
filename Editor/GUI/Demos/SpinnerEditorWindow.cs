using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly.Demos
{

	///<summary>
	/// Editor Window that demonstrates the Spinner utility usage.
	///</summary>
	public class SpinnerEditorWindow : EditorWindow
	{

		#region Properties

		private const string MENU_ITEM = "Tools/Muffin Dev/Demos/Loading Spinner";
        private const string WINDOW_TITLE = "Loading Spinner Demo";

        private const float SPINNER_SIZE = 24;
        private const float SPINNER_OFFSET = 8;

        private Spinner m_Spinner = null;

        #endregion


        #region Lifecycle

        private void OnEnable()
        {
            m_Spinner = new Spinner();
        }

        // Make the window update each editor frame, so the spinner can be updated and the GUI repainted
        private void Update()
        {
            if (m_Spinner != null)
                m_Spinner.Update();

            Repaint();
        }

        #endregion


        #region Public Methods

        [MenuItem(MENU_ITEM, false)]
        public static void ShowWindow()
        {
            SpinnerEditorWindow window = GetWindow<SpinnerEditorWindow>(false, WINDOW_TITLE, true);
            window.Show();
        }

        #endregion


        #region UI

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("This spinner icon is drawn using the Spinner utility class.\nThe animation is updated using the Update() message of EditorWindow. But when it comes to a custom inspector script (by inheriting from Editor), you must override RequireConstantRepaint() to make the GUI be repainted each editor frame.", MessageType.Info);
            EditorGUILayout.Space();

            Rect rect = EditorGUILayout.GetControlRect(false, SPINNER_SIZE, GUILayout.Width(SPINNER_SIZE));
            rect.x += SPINNER_OFFSET;
            m_Spinner.DrawGUI(rect);
		}

        #endregion

    }

}