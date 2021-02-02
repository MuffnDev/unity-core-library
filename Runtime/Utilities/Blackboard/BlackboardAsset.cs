﻿using UnityEngine;

namespace MuffinDev.Core
{

	///<summary>
	/// Represents a serializable ensemble of data which can have different types of values.
	///</summary>
	[AddComponentMenu("Muffin Dev/AI/Blackboard Asset")]
    public class BlackboardAsset : ScriptableObject
    {

		#region Properties

		[SerializeField]
		private Blackboard m_Blackboard = new Blackboard();

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
			return m_Blackboard.TryGetValue(_Key, out _Value);
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
			return m_Blackboard.TryGetValue(_Key, out _Value, _DefaultValue);
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
			return m_Blackboard.GetValue<T>(_Key);
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
			return m_Blackboard.GetValue<T>(_Key, _DefaultValue);
		}

		/// <summary>
		/// Updates the value with the given key, or create it on the blackboard.
		/// </summary>
		/// <typeparam name="T">The type of the value you want to set.</typeparam>
		/// <param name="_Key">The key of the value you want to create or update.</param>
		/// <param name="_Value">The value you want to set.</param>
		public void SetValue<T>(string _Key, T _Value)
		{
			m_Blackboard.SetValue(_Key, _Value);
		}

		/// <summary>
		/// Updates the value with the given key, or create it on the blackboard.
		/// </summary>
		/// <param name="_Key">The key of the value you want to create or update.</param>
		/// <param name="_Value">The value you want to set.</param>
		public void SetValue(string _Key, object _Value)
		{
			m_Blackboard.SetValue(_Key, _Value);
		}

		/// <summary>
		/// Gets the Blackboard instance of this asset.
		/// </summary>
		public Blackboard Blackboard
		{
			get { return m_Blackboard; }
		}

		#endregion

	}

}