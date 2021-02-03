using System;

using UnityEngine;

namespace MuffinDev.Core
{

	///<summary>
	/// 
	///</summary>
	public static class SerializationUtility
	{

		/// <summary>
		/// Serializes the given data into a string.
		/// This method uses JSONUtility to serialize objects, or just uses object.ToString() for primitives.
		/// </summary>
		/// <typeparam name="T">The type of the data to serialize.</typeparam>
		/// <param name="_Data">The data you want to serialize.</param>
		/// <returns>Returns the serialized data. Note that if the data is null, it returns an empty string.</returns>
		public static string SerializeToString<T>(T _Data)
        {
			return SerializeToString((object)_Data);
        }

		/// <summary>
		/// Serializes the given data into a string.
		/// This method uses JSONUtility to serialize objects, or just uses object.ToString() for primitives.
		/// </summary>
		/// <param name="_Data">The data you want to serialize.</param>
		/// <returns>Returns the serialized data. Note that if the data is null, it returns an empty string.</returns>
		public static string SerializeToString(object _Data)
        {
			if(_Data == null)
				return string.Empty;

			Type dataType = _Data.GetType();
			string dataTypeName = dataType.AssemblyQualifiedName;
			if (dataType.IsReallyPrimitive())
            {
				return _Data.ToString();
            }
			else
            {
#if UNITY_EDITOR
				return JsonUtility.ToJson(_Data, true);
#else
				return JsonUtility.ToJson(_Data);
#endif
			}
		}

		/// <summary>
		/// Deserializes data from the given string. Note that the string should've been made with SerializationUtility.SerializeToString()
		/// method in order to work as expected.
		/// </summary>
		/// <typeparam name="T">The expected type of the data to deserialize.</typeparam>
		/// <param name="_SerializedData">The serializdd data as string.</param>
		/// <returns>Returns the deserialized data, or null if it failed.</returns>
		public static T DeserializeFromString<T>(string _SerializedData)
        {
			object data = DeserializeFromString(typeof(T), _SerializedData);
			if (data == null)
				return default;

			try
            {
				return (T)data;
            }
			catch (InvalidCastException) { }

			return default;
		}

		/// <summary>
		/// Deserializes data from the given string. Note that the string should've been made with SerializationUtility.SerializeToString()
		/// method in order to work as expected.
		/// </summary>
		/// <param name="_DataType">The expected type of the data to deserialize.</param>
		/// <param name="_SerializedData">The serializdd data as string.</param>
		/// <returns>Returns the deserialized data, or null if it failed.</returns>
		public static object DeserializeFromString(string _DataType, string _SerializedData)
        {
			Type dataType = Type.GetType(_DataType);
			if (dataType == null)
				return null;
			return DeserializeFromString(dataType, _SerializedData);
        }

		/// <summary>
		/// Deserializes data from the given string. Note that the string should've been made with SerializationUtility.SerializeToString()
		/// method in order to work as expected.
		/// </summary>
		/// <param name="_DataType">The expected type of the data to deserialize.</param>
		/// <param name="_SerializedData">The serializdd data as string.</param>
		/// <returns>Returns the deserialized data, or null if it failed.</returns>
		public static object DeserializeFromString(Type _DataType, string _SerializedData)
		{
			if (_DataType == null)
				return null;

			object data = null;
			if (_DataType.IsReallyPrimitive())
			{
				bool notEmpty = !string.IsNullOrEmpty(_SerializedData);

				if (_DataType == typeof(bool))
					data = notEmpty ? bool.Parse(_SerializedData) : false;
				else if (_DataType == typeof(int))
					data = notEmpty ? int.Parse(_SerializedData) : 0;
				else if (_DataType == typeof(float))
					data = notEmpty ? float.Parse(_SerializedData) : 0f;
				else if (_DataType == typeof(string))
					data = _SerializedData;
				else if (_DataType == typeof(char))
					data = notEmpty ? char.Parse(_SerializedData) : 0;
				else if (_DataType == typeof(double))
					data = notEmpty ? double.Parse(_SerializedData) : 0;
				else if (_DataType == typeof(byte))
					data = notEmpty ? byte.Parse(_SerializedData) : 0;
				else if (_DataType == typeof(sbyte))
					data = notEmpty ? sbyte.Parse(_SerializedData) : 0;
				else if (_DataType == typeof(decimal))
					data = notEmpty ? decimal.Parse(_SerializedData) : 0;
				else if (_DataType == typeof(uint))
					data = notEmpty ? uint.Parse(_SerializedData) : 0;
				else if (_DataType == typeof(long))
					data = notEmpty ? long.Parse(_SerializedData) : 0;
				else if (_DataType == typeof(ulong))
					data = notEmpty ? ulong.Parse(_SerializedData) : 0;
				else if (_DataType == typeof(short))
					data = notEmpty ? short.Parse(_SerializedData) : 0;
				else if (_DataType == typeof(ushort))
					data = notEmpty ? ushort.Parse(_SerializedData) : 0;
				else
				{
					Debug.LogWarning("Impossible to deserialize data of type: " + _DataType.FullName);
					return null;
				}
			}
			else
			{
				data = Activator.CreateInstance(_DataType);
				JsonUtility.FromJsonOverwrite(_SerializedData, data);
			}

			return data;
		}

	}

}