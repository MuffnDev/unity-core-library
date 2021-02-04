using System;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for System.Type objects.
    ///</summary>
    public static class TypeExtension
    {

        /// <summary>
        /// Gets the full type name string, including the assmebly name. The output string looks like:
        /// "TypeNamespace.TypeName, AssemblyName, Version=0.0.0.0, Culture=neutral, PublicKeyKoken=null"
        /// </summary>
        /// <param name="_Type">The type you wan to get the full name string.</param>
        /// <returns>Returns the computed full name string, or null if the given type is null.</returns>
        public static string GetFullNameWithAssembly(this Type _Type)
        {
            return _Type != null ? $"{_Type.FullName}, {_Type.Assembly}" : null;
        }

        /// <summary>
        /// Checks if the type is "really" a primitive. This method is meant to replace the Type.IsPrimitive property, which will return
        /// false even if the type is a string, decimal, long, ulong, short or ushort.
        /// </summary>
        /// <param name="_Type">The type you want to check as primitive.</param>
        /// <returns>Returns true if the type is "really" a primitive, otherwise false.</returns>
        public static bool IsReallyPrimitive(this Type _Type)
        {
            return
                _Type.IsPrimitive ||
                _Type == typeof(string) ||
                _Type == typeof(decimal) ||
                _Type == typeof(long) ||
                _Type == typeof(ulong) ||
                _Type == typeof(short) ||
                _Type == typeof(ushort);
        }

    }

}