using UnityEngine;

namespace MuffinDev.Core
{

    /// <summary>
    /// Defines the component searching method.
    /// Used by AutoAssignAttribute.
    /// </summary>
    [System.Obsolete("Since the [AutoAssign] attribute is deprecated, you should use [ComponentRef] attribute instead, which provides the EComponentRefScope enumeration.")]
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
    [System.Obsolete("You should use [ComponentRef] attribute instead, which is more flexible and can be used in combination with Component.InitComponentRefs() to automatize the Components and GameObject references initialization.")]
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