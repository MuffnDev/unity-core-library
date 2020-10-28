using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MuffinDev.EditorUtils
{

	///<summary>
	/// Contains some utility methods for C# reflection.
	///</summary>
	public class ReflectionUtility
	{

        public const string UNITY_CSHARP_ASSEMBLY_NAME = "Assembly-CSharp";
        public const string UNITY_EDITOR_CSHARP_ASSEMBLY_NAME = "Assembly-CSharp-Editor";
        private static readonly string[] NOT_PROJECT_ASSEMBLIES =
        {
            "mscorlib",
            "UnityEngine",
            "UnityEditor",
            "Unity.",
            "System",
            "Mono.",
            "netstandard",
            "Microsoft"
        };

        /// <summary>
        /// Gets all the types that implement the given generic type in the given assembly.
        /// NOTE: This method is inspired by this Stack Overflow answer: https://stackoverflow.com/a/8645519/6699339
        /// </summary>
        /// <param name="_GenericType">The type of the generic class you want to find inheritors. Uses the "open generic" syntax. As an
        /// example, if your class is MyGenericClass<T>, use typeof(MyGenericClass<>), without any value inside the less-than/greater-than
        /// characters.</param>
        /// <param name="_Assembly">The assembly where you want to find the given generic type implementations.</param>
        /// <returns>Returns an enumerable that contains all the found types.</returns>
        public static IEnumerable<Type> GetAllTypesImplementingGenericType(Type _GenericType, Assembly _Assembly)
        {
            return from type in _Assembly.GetTypes()
                   let baseType = type.BaseType
                   where baseType != null && baseType.IsGenericType && !type.IsGenericType && _GenericType.IsAssignableFrom(baseType.GetGenericTypeDefinition())
                   select type;
        }

        /// <summary>
        /// Gets an Assembly with the given name.
        /// </summary>
        /// <param name="_AssemblyName">The name of the Assembly you want to get.</param>
        /// <returns>Returns the found assembly, otherwise null.</returns>
        public static Assembly GetAssemblyByName(string _AssemblyName)
        {
            if (string.IsNullOrEmpty(_AssemblyName))
                return null;

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetName().Name == _AssemblyName)
                {
                    return assembly;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the Unity's CSharp assembly, which is the assembly that contains your custom scripts.
        /// </summary>
        public static Assembly GetUnityProjectAssembly()
        {
            return GetAssemblyByName(UNITY_CSHARP_ASSEMBLY_NAME);
        }

        /// <summary>
        /// Gets the Unity's CSharp Editor assembly, which is the assembly that contains your custom scripts in /Editor directories.
        /// </summary>
        public static Assembly GetUnityEditorProjectAssembly()
        {
            return GetAssemblyByName(UNITY_EDITOR_CSHARP_ASSEMBLY_NAME);
        }

        /// <summary>
        /// Gets all the project assemblies, ignoring all native .NET assemblies, UnityEngine assemblies, UnityEditor assemblies, etc.
        /// </summary>
        /// <returns>Returns the found assemblies.</returns>
        public static Assembly[] GetProjectAssemblies()
        {
            List<Assembly> projectAssemblies = new List<Assembly>();
            // For each assembly
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                string assemblyName = assembly.GetName().Name;
                bool isProjectAssembly = true;
                // If the assembly starts with one of the "not-project" assemblies, skip it
                foreach(string notProjectAssemblyName in NOT_PROJECT_ASSEMBLIES)
                {
                    if (assemblyName.StartsWith(notProjectAssemblyName))
                    {
                        isProjectAssembly = false;
                        break;
                    }
                }

                // Add the assembly to the list if possible
                if (isProjectAssembly)
                    projectAssemblies.Add(assembly);
            }

            return projectAssemblies.ToArray();
        }

    }

}