// Uncomment this directive to mark Wwise as enabled.
//#define WWISE

using UnityEngine;

namespace MuffinDev.Core.Integrations
{

    /// <summary>
    /// This is a utility component for testing Wwise engine integration.
    /// It allows you to bind events, states, and other Wwise controls to an input directly in the inspector.
    /// </summary>
    [AddComponentMenu("Muffin Dev/Integrations/Wwise/Wwise Controls Player")]
    public class WwiseControlsPlayer : MonoBehaviour
    {

        #if !WWISE
        private const string WWISE_DISABLED_LOG_MESSAGE = "WwiseDemoPlayer can't work because Wwise is not marked enabled. To do so, uncomment the #define directive in the WwiseControlsPlayer script.";
        #endif

        #region Enums & Subclasses

        /// <summary>
        /// Represents a control for Wwise engine.
        /// </summary>
        private abstract class WwiseControl
        {
            [SerializeField]
            [Tooltip("Displayed name in the inspector.")]
            private string m_ControlName = string.Empty;

            [SerializeField]
            [Tooltip("Input key for applying this control.")]
            private KeyCode m_Key = KeyCode.Space;

            /// <summary>
            /// Apply this control.
            /// </summary>
            public abstract void Execute(GameObject _GameObject);

            public string ControlName { get { return m_ControlName; } }
            public KeyCode Key { get { return m_Key; } }
        }

        /// <summary>
        /// Posts an event to Wwise engine, using AkSoundEngine.PostEvent().
        /// </summary>
        [System.Serializable]
        private class WwiseEventControl : WwiseControl
        {
            [SerializeField]
            [Tooltip("The event to trigger in Wwise engine.")]
            private string m_EventName = string.Empty;

            /// <summary>
            /// Posts the "eventName" event to Wwise engine.
            /// </summary>
            public override void Execute(GameObject _GameObject)
            {
                #if WWISE
                AkSoundEngine.PostEvent(m_EventName, _GameObject);
                #else
                Debug.LogWarning(WwiseControlsPlayer.WWISE_DISABLED_LOG_MESSAGE);
                #endif
            }

            public string EventName { get { return m_EventName; } }
        }

        /// <summary>
        /// Sets the state of a group in Wwise engine, using AkSoundEngine.SetState().
        /// </summary>
        [System.Serializable]
        private class WwiseStateControl : WwiseControl
        {
            [SerializeField]
            private string m_StateGroupName = string.Empty;

            [SerializeField]
            private string m_TargetStateName = string.Empty;

            /// <summary>
            /// Sets the state to "targetStateName" in the states group "stateGroupName".
            /// </summary>
            public override void Execute(GameObject _GameObject)
            {
                #if WWISE
                AkSoundEngine.SetState(m_StateGroupName, m_TargetStateName);
                #else
                Debug.LogWarning(WwiseControlsPlayer.WWISE_DISABLED_LOG_MESSAGE);
                #endif
            }

            public string StateGroupName { get { return m_StateGroupName; } }
            public string TargetStateName { get { return m_TargetStateName; } }
        }

        /// <summary>
        /// Sets the value of an RTPC variable in Wwise engine, using AkSoundEngine.SetRTPCValue().
        /// </summary>
        [System.Serializable]
        private class WwiseRTPCControl : WwiseControl
        {
            [SerializeField]
            [Tooltip("Name of the RTPC variable in Wwise.")]
            private string m_Name = string.Empty;

            [SerializeField]
            private float m_Value = 0f;

            /// <summary>
            /// Sets the value "value" of the variable "name".
            /// </summary>
            public override void Execute(GameObject _GameObject)
            {
                #if WWISE
                AkSoundEngine.SetRTPCValue(m_Name, m_Value);
                #else
                Debug.LogWarning(WwiseControlsPlayer.WWISE_DISABLED_LOG_MESSAGE);
                #endif
            }

            public string Name { get { return m_Name; } }
            public float Value { get { return m_Value; } }
        }

        #endregion


        #region Properties

        [SerializeField]
        [Tooltip("Controls for posting events to Wwise engine, using AkSoundEngine.PostEvent()")]
        private WwiseEventControl[] m_EventControls = { };

        [SerializeField]
        [Tooltip("Controls for setting the state of a state group in Wwise engine, using AkSoundEngine.SetState()")]
        private WwiseStateControl[] m_StateControls = { };

        [SerializeField]
        [Tooltip("Controls for setting the value of an RTPC variable in Wwise engine, using AkSoundEngine.SetRTPCValue()")]
        private WwiseRTPCControl[] m_RTPCVariableControls = { };

        #endregion


        #region Lifecycle

        private void Update()
        {
            if (ExecuteControl(m_EventControls)) { return; }
            if (ExecuteControl(m_StateControls)) { return; }
            if (ExecuteControl(m_RTPCVariableControls)) { return; }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Checks if the key of a control in the given list is pressed, and execute that control if true.
        /// </summary>
        /// <returns>Returns true if a control has been executed, otherwise false.</returns>
        private bool ExecuteControl(WwiseControl[] _Controls)
        {
            foreach (WwiseControl control in _Controls)
            {
                if (Input.GetKeyDown(control.Key))
                {
                    control.Execute(gameObject);
                    return true;
                }
            }

            return false;
        }

        #endregion

    }

}