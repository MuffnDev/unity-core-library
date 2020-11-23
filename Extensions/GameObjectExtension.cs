using System;
using System.Collections.Generic;

using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for GameObject objects.
    ///</summary>
    public static class GameObjectExtension
	{
        
        /// <summary>
        /// Gets the component of the given type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
        public static T GetComponentFromRoot<T>(this GameObject _Obj)
            where T : Component
        {
            Queue<GameObject> gameObjects = new Queue<GameObject>();
            gameObjects.Enqueue(_Obj);

            GameObject currentGameObject = null;
            while((currentGameObject = gameObjects.Dequeue()) != null)
            {
                T comp = currentGameObject.GetComponent<T>();
                if(comp != null)
                {
                    return comp;
                }

                int childCount = currentGameObject.transform.childCount;
                for(int i = 0; i < childCount; i++)
                {
                    gameObjects.Enqueue(currentGameObject.transform.GetChild(i).gameObject);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the component of the given type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
        public static Component GetComponentFromRoot(this GameObject _Obj, Type _ComponentType)
        {
            Queue<GameObject> gameObjects = new Queue<GameObject>();
            gameObjects.Enqueue(_Obj);

            GameObject currentGameObject = null;
            while ((currentGameObject = gameObjects.Dequeue()) != null)
            {
                Component comp = currentGameObject.GetComponent(_ComponentType);
                if (comp != null)
                {
                    return comp;
                }

                int childCount = currentGameObject.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    gameObjects.Enqueue(currentGameObject.transform.GetChild(i).gameObject);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the component of the named type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
        public static Component GetComponentFromRoot(this GameObject _Obj, string _ComponentTypeName)
        {
            Queue<GameObject> gameObjects = new Queue<GameObject>();
            gameObjects.Enqueue(_Obj);

            GameObject currentGameObject = null;
            while ((currentGameObject = gameObjects.Dequeue()) != null)
            {
                Component comp = currentGameObject.GetComponent(_ComponentTypeName);
                if (comp != null)
                {
                    return comp;
                }

                int childCount = currentGameObject.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    gameObjects.Enqueue(currentGameObject.transform.GetChild(i).gameObject);
                }
            }

            return null;
        }

    }

}