﻿using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorUtils
{

    ///<summary>
    /// This utility allows you to force Unity to recompile code.
    /// You can use Recompiler.Recompile() to do it through code, or from Assets > Recompile.
    ///</summary>
    public class Recompiler : ScriptableObject
    {
        
        /// <summary>
        /// Recompiles code.
        /// </summary>
        [MenuItem("Assets/Recompile", false, 40)]
        public static void Recompile()
        {
            string scriptPath = ScriptableObjectExtension.GetScriptPath<Recompiler>();
            AssetDatabase.ImportAsset(scriptPath, ImportAssetOptions.ForceUpdate);
        }

    }

}