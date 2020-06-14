using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Defines the component searching method.
    /// Used by AutoAssignAttribute.
    /// </summary>
    public enum EAutoAssignMethod
    {
        /// <summary>
        /// Gets a matching component using GetComponent().
        /// </summary>
        GetComponent,

        /// <summary>
        /// Gets a matching component using GetComponentInChildren().
        /// </summary>
        GetComponentInChildren,

        /// <summary>
        /// Gets a matching component using GetComponentInParent().
        /// </summary>
        GetComponentInParent,

        /// <summary>
        /// Gets a matching component using GetComponentFromRoot() extension.
        /// </summary>
        GetComponentFromRoot,

        /// <summary>
        /// Gets a matching component using GameObject.FindObjectOfType().
        /// </summary>
        FindObjectOfType
    }

    /// <summary>
    /// Automaticcaly get a component to fill this property field.
    /// </summary>
    public class AutoAssignAttribute : PropertyAttribute
    {

        #region Properties

        private EAutoAssignMethod m_AutoAssignMethod = EAutoAssignMethod.GetComponent;

        #endregion


        #region Initialization

        /// <summary>
        /// Creates an AutoAssignAttribute instance that uses EAutoAssignMethod.GetComponent method.
        /// </summary>
        public AutoAssignAttribute() { }

        /// <summary>
        /// Creates an AutoAssignAttribute instance that uses the given method.
        /// </summary>
        public AutoAssignAttribute(EAutoAssignMethod _AutoAssignMethod)
        {
            m_AutoAssignMethod = _AutoAssignMethod;
        }

        #endregion


        #region Accessors

        public EAutoAssignMethod AutoAssignMethod
        {
            get { return m_AutoAssignMethod; }
        }

        #endregion

    }

}