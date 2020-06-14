using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Unity-friendly implementation of singleton pattern for MonoBehaviour objects.
    /// </summary>
    public class MonoSingleton<TSingletonType> : MonoBehaviour
        where TSingletonType : MonoBehaviour
    {

        #region Properties

        private static TSingletonType s_Instance = null;
        private static bool s_Initialized = false;

        #endregion


        #region Initialization

        private void Awake()
        {
            TSingletonType currentInstance = (this as TSingletonType);

            if(SetInstance(currentInstance))
            {
                MonoSingletonOnInit();
                MonoSingletonOnAwake();
            }
            else
            {
                MonoSingletonRejected();
                Destroy(currentInstance);
            }
        }

        private void OnDestroy()
        {
            if(s_Instance == (this as TSingletonType))
            {
                s_Instance = null;
            }

            MonoSingletonOnDestroy();
        }

        #endregion


        #region Protected Methods

        /// <summary>
        /// Called before MonoSingletonOnAwake(), only if the current instance has been set as the MonoSingleton instance.
        /// </summary>
        protected virtual void MonoSingletonOnInit() { }

        /// <summary>
        /// Called at Awake message, no matter if the instance is set as the MonoSingleton instance.
        /// </summary>
        protected virtual void MonoSingletonOnAwake() { }

        /// <summary>
        /// Called before MonoSingletonOnAwake(), only if the current instance can't be set as the MonoSingleton instance.
        /// </summary>
        protected virtual void MonoSingletonRejected()
        {
            string objType =
            #if UNITY_EDITOR
            typeof(TSingletonType).Name;
            #else
            "that type";
            #endif
            Debug.LogWarning(string.Format("There's more than 1 instance of {0} in the scene. The object {1} is rejected as .", objType, name));
        }

        /// <summary>
        /// Called at OnDestroy message, no matter if the instance is set as the MonoSingleton instance.
        /// </summary>
        protected virtual void MonoSingletonOnDestroy() { }

        #endregion


        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static MonoSingleton<TSingletonType> CreateInstanceInScene()
        {
            // Use reflection if we are in Editor to set name
			string gameObjectName =
			#if UNITY_EDITOR
			typeof(TSingletonType).Name + "_Singleton";
			#else
            // Out of the editor, set a default name
			"SingletonInstance";
			#endif

            GameObject obj = new GameObject(gameObjectName);
            return obj.AddComponent<TSingletonType>() as MonoSingleton<TSingletonType>;
        }

        private bool SetInstance(TSingletonType _Instance)
        {
            if(s_Instance == null && _Instance != null)
            {
                s_Instance = _Instance;
                s_Initialized = true;
            }
            return (s_Instance == _Instance);
        }

        #endregion


        #region Accessors

        public static TSingletonType Instance
        {
            get { return (Application.isPlaying) ? GetInstance() : GetInstance(false); }
        }

        private static TSingletonType GetInstance(bool _UseStaticInstance = true)
        {
            if(_UseStaticInstance)
            {
                if (!s_Initialized)
                {
                    MonoSingleton<TSingletonType> instanceInScene = FindObjectOfType<MonoSingleton<TSingletonType>>();
                    if (instanceInScene == null)
                    {
                        instanceInScene = CreateInstanceInScene();
                    }
                    instanceInScene.SetInstance(instanceInScene as TSingletonType);
                }

                return s_Instance;
            }
            else
            {
                MonoSingleton<TSingletonType>[] instancesInScene = FindObjectsOfType<MonoSingleton<TSingletonType>>();
                if(instancesInScene.Length == 1)
                {
                    return instancesInScene[0] as TSingletonType;
                }
                else if(instancesInScene.Length > 1)
                {
                    string objType =
                    #if UNITY_EDITOR
                    typeof(TSingletonType).Name;
                    #else
                    "that type";
                    #endif
                    Debug.LogWarning(string.Format("There's more than 1 instance of {0} in the scene.", objType));
                    return instancesInScene[0] as TSingletonType;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

    }

}