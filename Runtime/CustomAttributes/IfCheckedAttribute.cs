using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    ///	Enables/Disables a field if another property is checked/unchecked.
    ///</summary>
    public class IfCheckedAttribute : PropertyAttribute
    {

        #region Members

        private string m_PropertyName = string.Empty;
        private bool m_EnableIfChecked = true;

        #endregion


        #region Initialisation / Destruction

        private IfCheckedAttribute()
        {

        }

        /// <summary>
        /// Enable/Disable a field if another property is checked/unchecked.
        /// </summary>
        /// <param name="_PropertyName">The name of the property to check.</param>
        /// <param name="_EnableIfChecked">If true, the current field will be enabled if the named
        /// property's value is true. Else, inverse the behaviour.</param>
        public IfCheckedAttribute(string _PropertyName, bool _EnableIfChecked = true)
        {
            m_PropertyName = _PropertyName;
            m_EnableIfChecked = _EnableIfChecked;
        }

        #endregion


        #region Accessors

        public string PropertyName
        {
            get { return m_PropertyName; }
        }

        public bool EnabledIfChecked
        {
            get { return m_EnableIfChecked; }
        }

        #endregion

    }

}