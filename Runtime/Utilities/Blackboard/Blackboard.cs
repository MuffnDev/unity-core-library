using System;
using System.Collections.Generic;

using UnityEngine;


namespace MuffinDev.Core
{

	///<summary>
	/// Represents a serializable ensemble of data which can have different types of values.
	///</summary>
	[Serializable]
	public class Blackboard
	{

		#region Subclasses

		[Serializable]
		private class SerializedEntry : ISerializationCallbackReceiver
		{

			#region Properties

			[SerializeField]
			private string m_Key = string.Empty;

			[SerializeField, TextArea(3, 20)]
			private string m_SerializedData = string.Empty;

			[SerializeField]
			private string m_DataTypeName = string.Empty;

			private object m_Data = null;

			#endregion


			#region Initialization

			public SerializedEntry(string _Key, object _Value)
			{
				m_Key = _Key;
				m_Data = _Value;
			}

			#endregion


			#region Serialization

			public void OnAfterDeserialize()
            {
				if (string.IsNullOrEmpty(m_DataTypeName))
					return;

				Type type = Type.GetType(m_DataTypeName);
				if (type == null)
				{
					Debug.LogWarning("Impossible to deserialize data of type " + m_DataTypeName);
					return;
				}

				if (type.IsReallyPrimitive())
				{
					if (type == typeof(bool))
						m_Data = bool.Parse(m_SerializedData);
					else if (type == typeof(int))
						m_Data = int.Parse(m_SerializedData);
					else if (type == typeof(float))
						m_Data = float.Parse(m_SerializedData);
					else if (type == typeof(string))
						m_Data = m_SerializedData;
					else if (type == typeof(char))
						m_Data = char.Parse(m_SerializedData);
					else if (type == typeof(double))
						m_Data = double.Parse(m_SerializedData);
					else if (type == typeof(byte))
						m_Data = byte.Parse(m_SerializedData);
					else if (type == typeof(sbyte))
						m_Data = sbyte.Parse(m_SerializedData);
					else if (type == typeof(decimal))
						m_Data = decimal.Parse(m_SerializedData);
					else if (type == typeof(uint))
						m_Data = uint.Parse(m_SerializedData);
					else if (type == typeof(long))
						m_Data = long.Parse(m_SerializedData);
					else if (type == typeof(ulong))
						m_Data = ulong.Parse(m_SerializedData);
					else if (type == typeof(short))
						m_Data = short.Parse(m_SerializedData);
					else if (type == typeof(ushort))
						m_Data = ushort.Parse(m_SerializedData);
					else
					{
						Debug.LogWarning("Impossible to deserialize data of type " + m_DataTypeName);
						return;
					}
				}
				else
				{
					m_Data = Activator.CreateInstance(type);
					JsonUtility.FromJsonOverwrite(m_SerializedData, m_Data);
				}
			}

			public void OnBeforeSerialize()
			{
				if(m_Data == null)
                {
					m_DataTypeName = null;
					m_SerializedData = null;
					return;
                }

				Type dataType = m_Data.GetType();
				m_DataTypeName = dataType.AssemblyQualifiedName;
				if (dataType.IsReallyPrimitive())
                {
					m_SerializedData = m_Data.ToString();
                }
				else
                {
#if UNITY_EDITOR
					m_SerializedData = JsonUtility.ToJson(m_Data, true);
#else
					m_SerializedData = JsonUtility.ToJson(m_Data);
#endif
				}

			}

			#endregion


			#region Public API

			public string Key
			{
				get { return m_Key; }
			}

			public object Data
			{
				get
				{
					return m_Data;
				}
				set
				{
					m_Data = value;
				}
			}

			#endregion

		}

		#endregion


		#region Properties

		[SerializeField]
		private List<SerializedEntry> m_SerializedEntries = new List<SerializedEntry>();

        #endregion


        #region Public API

        /// <summary>
        /// Try to get the value by its key.
        /// </summary>
        /// <typeparam name="T">The expected type of the value.</typeparam>
        /// <param name="_Key">The key of the value you want to get.</param>
        /// <param name="_Value">The found value, or the default value if the expected type doesn't match, or if the key doesn't
        /// exist.</param>
        /// <returns>Returns true if the value has been found, or false if the expected type doesn't match or if the key doesn't
        /// exist.</returns>
        public bool TryGetValue<T>(string _Key, out T _Value)
		{
			int i = GetValueIndex(_Key);
			if (i != -1)
			{
				try
				{
					_Value = (T)m_SerializedEntries[i].Data;
					return true;
				}
				catch (InvalidCastException) { }
				catch (NullReferenceException) { }
			}

			_Value = default;
			return false;
		}

		/// <summary>
		/// Try to get the value by its key.
		/// </summary>
		/// <typeparam name="T">The expected type of the value.</typeparam>
		/// <param name="_Key">The key of the value you want to get.</param>
		/// <param name="_Value">The found value, or the default value if the expected type doesn't match, or if the key doesn't
		/// exist.</param>
		/// <param name="_DefaultValue">The default value to return if the expected type doesn't match or if the key doesn't exist.</param>
		/// <returns>Returns true if the value has been found, or false if the expected type doesn't match or if the key doesn't
		/// exist.</returns>
		public bool TryGetValue<T>(string _Key, out T _Value, T _DefaultValue)
		{
			int i = GetValueIndex(_Key);
			if (i != -1)
			{
				try
				{
					_Value = (T)m_SerializedEntries[i].Data;
					return true;
				}
				catch (InvalidCastException) { }
			}

			_Value = _DefaultValue;
			return false;
		}

		/// <summary>
		/// Gets a value by its key.
		/// </summary>
		/// <typeparam name="T">The expected type of the value.</typeparam>
		/// <param name="_Key">The key of the value you want to get.</param>
		/// <returns>Returns the found value, or the default value if the expected type doesn't match, or if the key doesn't
		/// exist.</returns>
		public T GetValue<T>(string _Key)
		{
			if (TryGetValue(_Key, out T value))
			{
				return value;
			}
			return default;
		}

		/// <summary>
		/// Gets a value by its key.
		/// </summary>
		/// <typeparam name="T">The expected type of the value.</typeparam>
		/// <param name="_Key">The key of the value you want to get.</param>
		/// <param name="_DefaultValue">The default value to use if the expected value can't be found on the blackboard.</param>
		/// <returns>Returns the found value, or the default value if the expected type doesn't match, or if the key doesn't exist.</returns>
		public T GetValue<T>(string _Key, T _DefaultValue)
		{
			if (TryGetValue(_Key, out T value))
			{
				return value;
			}
			return _DefaultValue;
		}

		/// <summary>
		/// Updates the value with the given key, or create it on the blackboard.
		/// </summary>
		/// <typeparam name="T">The type of the value you want to set.</typeparam>
		/// <param name="_Key">The key of the value you want to create or update.</param>
		/// <param name="_Value">The value you want to set.</param>
		public void SetValue<T>(string _Key, T _Value)
		{
			SetValue(_Key, (object)_Value);
		}

		/// <summary>
		/// Updates the value with the given key, or create it on the blackboard.
		/// </summary>
		/// <param name="_Key">The key of the value you want to create or update.</param>
		/// <param name="_Value">The value you want to set.</param>
		public void SetValue(string _Key, object _Value)
		{
			int i = GetValueIndex(_Key);
			if (i != -1)
				m_SerializedEntries[i].Data = _Value;
			else
				m_SerializedEntries.Add(new SerializedEntry(_Key, _Value));
		}

		#endregion


		#region Private methods

		/// <summary>
		/// Gets the index in the list of the value by its key.
		/// </summary>
		/// <param name="_Key">The key of the value you want to get the index.</param>
		/// <returns>Returns the found value's index, or -1 if the key doesn't exist.</returns>
		private int GetValueIndex(string _Key)
		{
			for (int i = 0; i < m_SerializedEntries.Count; i++)
			{
				if (m_SerializedEntries[i].Key == _Key)
					return i;
			}
			return -1;
		}

		#endregion

	}

}