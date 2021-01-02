using System;
using System.Collections.Generic;

using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    /// Represents a component that can be "modded", so you can manage native classes instances as mods that will be serialized and
    /// deserialized as expected.
    ///</summary>
    public abstract class ModdableComponent<TModType> : MonoBehaviour, ISerializationCallbackReceiver
        where TModType : new()
    {

        #region Subclasses

        /// <summary>
        /// Thrown when the type of a serialized mod can't be found from the project assemblies.
        /// </summary>
        public class UnknownModTypeException : Exception
        {
            public string fullTypeName;
            public string typeName;

            public UnknownModTypeException(string _Message, string _FullTypeName, string _TypeName)
                : base(_Message)
            {
                fullTypeName = _FullTypeName;
                typeName = _TypeName;
            }
        }

        /// <summary>
        /// Contains the serialized data of a mod.
        /// </summary>
        [System.Serializable]
        private class SerializedMod
        {
            public string fullTypeName { get; private set; } = null;
            public string typeName { get; private set; } = null;
            public string json { get; private set; } = null;

            /// <summary>
            /// Creates a new serialized mod.
            /// </summary>
            public SerializedMod(TModType _Mod)
            {
                Serialize(_Mod);
            }

            /// <summary>
            /// Serializes the data of the given mod.
            /// </summary>
            public void Serialize(TModType _Action)
            {
                fullTypeName = _Action.GetType().FullName;
                typeName = _Action.GetType().Name;
                json = JsonUtility.ToJson(_Action);
            }

            /// <summary>
            /// Creates an instance of a mod, and initialize its valus by deserializing the JSON data.
            /// </summary>
            /// <returns>Returns the initialized mod instance.</returns>
            public TModType Deserialize()
            {
                Type type = Type.GetType(typeName);
                if (type == null)
                    throw new UnknownModTypeException($"The mod of type \"{typeName}\" doesn't exist", fullTypeName, typeName);

                TModType action = (TModType)Activator.CreateInstance(type);
                JsonUtility.FromJsonOverwrite(json, action);
                return action;
            }
        }

        #endregion


        #region Properties

        // Contains the serialized data of all mods on this component.
        [SerializeField, HideInInspector]
        private SerializedMod[] m_SerializedMods = { };

        // Contains the loaded mod instances.
        private List<TModType> m_Mods = new List<TModType>();

        #endregion


        #region Serialization

        /// <summary>
        /// Called before Unity serializes this object.
        /// </summary>
        public virtual void OnBeforeSerialize()
        {
            SerializeMods();
        }

        /// <summary>
        /// Called after Unity deserializes this object.
        /// </summary>
        public virtual void OnAfterDeserialize()
        {
            DeserializeMods();
        }

        /// <summary>
        /// Serializes the mods in the list.
        /// </summary>
        protected void SerializeMods()
        {
            m_SerializedMods = new SerializedMod[m_Mods.Count];
            int i = 0;
            foreach (TModType mod in m_Mods)
            {
                string json = JsonUtility.ToJson(mod);
                m_SerializedMods[i] = new SerializedMod(mod);
                i++;
            }
        }

        /// <summary>
        /// Deserializes the mods from data saved on disk.
        /// </summary>
        protected void DeserializeMods()
        {
            m_Mods.Clear();
            foreach (SerializedMod serializedMod in m_SerializedMods)
            {
                m_Mods.Add(serializedMod.Deserialize());
            }
        }

        #endregion


        #region Accessors

        /// <summary>
        /// Gets/sets the list of loaded mods.
        /// </summary>
        protected List<TModType> Mods
        {
            get { return m_Mods; }
            set { m_Mods = value; }
        }

        #endregion

    }

}