using System;

using UnityEngine;

namespace MuffinDev.Core
{

    /// <summary>
    /// Defines how the reference should be searched.
    /// </summary>
    [Flags]
    public enum EComponentRefScope
    {
        /// <summary>
        /// Gets the reference on the decorated component's GameObject only.
        /// </summary>
        Local = 0,

        /// <summary>
        /// Gets the reference on the decorated component's GameObject or in its children.
        /// </summary>
        Children = 1,

        /// <summary>
        /// Gets the reference from an object in the scene.
        /// </summary>
        World = 2
    }

    ///<summary>
    /// This attribute is meant to be used with the Component's extension method InitComponentRefs(), which automatically get
    /// component or GameObject references on a GameObject, its children or even globally in the scene.
    ///</summary>
	[AttributeUsage(AttributeTargets.Field)]
    public class ComponentRefAttribute : PropertyAttribute
	{

		private string m_RefObjectName = null;
        private EComponentRefScope m_Scope = EComponentRefScope.Local;

        /// <summary>
        /// Helps you to initialize references to components or GameObject easily.
        /// Call Component.InitComponentRefs() method inside a component in order to initializes references to components or GameObjects
        /// for any property of that component that use this attribute.
        /// </summary>
        /// <param name="_AllowChildren">If enabled, the reference will be searched on the GameObject and in its hierarchy.</param>
        /// <param name="_AllowScene">If enabled, the reference will be searched in the scene (using FindObjectOfType()).</param>
        public ComponentRefAttribute(bool _AllowChildren = false, bool _AllowScene = false)
        {
            m_Scope = EComponentRefScope.Local;
            if (_AllowChildren)
                m_Scope |= EComponentRefScope.Children;
            if (_AllowScene)
                m_Scope |= EComponentRefScope.World;
        }

        /// <summary>
        /// Helps you to initialize references to components or GameObject easily.
        /// Call Component.InitComponentRefs() method inside a component in order to initializes references to components or GameObjects
        /// for any property of that component that use this attribute.
        /// </summary>
        /// <param name="_ChildName">The name of the child object from which you want to get the reference.</param>
        public ComponentRefAttribute(string _ChildName)
        {
            m_Scope = EComponentRefScope.Local | EComponentRefScope.Children;
            m_RefObjectName = _ChildName;
        }

        /// <summary>
        /// Helps you to initialize references to components or GameObject easily.
        /// Call Component.InitComponentRefs() method inside a component in order to initializes references to components or GameObjects
        /// for any property of that component that use this attribute.
        /// </summary>
        /// <param name="_ReferenceObjectName">The name of the object from which you want to get the reference.</param>
        /// <param name="_WorldOnly">If enabled, the reference will be get from the world, so not from this GameObject or one of its
        /// child.</param>
        public ComponentRefAttribute(string _ReferenceObjectName, bool _WorldOnly)
        {
            m_Scope = _WorldOnly
                ? EComponentRefScope.World
                : EComponentRefScope.Local | EComponentRefScope.Children | EComponentRefScope.World;
            m_RefObjectName = _ReferenceObjectName;
        }

        /// <summary>
        /// Helps you to initialize references to components or GameObject easily.
        /// Call Component.InitComponentRefs() method inside a component in order to initializes references to components or GameObjects
        /// for any property of that component that use this attribute.
        /// </summary>
        /// <param name="_Scope">Defines if you want to get references from this object (Local), its children (Children), in the scene
        /// (World), or several scopes using flags (for example EComponentRefScope.Local | EComponentRefScope.Children).</param>
        public ComponentRefAttribute(EComponentRefScope _Scope)
        {
            m_Scope = _Scope;
        }

        /// <summary>
        /// Helps you to initialize references to components or GameObject easily.
        /// Call Component.InitComponentRefs() method inside a component in order to initializes references to components or GameObjects
        /// for any property of that component that use this attribute.
        /// </summary>
        /// <param name="_ReferenceObjectName">The name of the object from which you want to get the reference.</param>
        /// <param name="_Scope">Defines if you want to get references from this object (Local), its children (Children), in the scene
        /// (World), or several scopes using flags (for example EComponentRefScope.Local | EComponentRefScope.Children).</param>
        public ComponentRefAttribute(string _ReferenceObjectName, EComponentRefScope _Scope)
        {
            m_RefObjectName = _ReferenceObjectName;
            m_Scope = _Scope;
        }

        /// <summary>
        /// The name of the object on which the reference should be get.
        /// </summary>
        public string RefObjectName
        {
            get { return m_RefObjectName; }
        }
        
        /// <summary>
        /// The scope to use for searching a reference.
        /// </summary>
        public EComponentRefScope Scope
        {
            get { return m_Scope; }
        }

	}

}