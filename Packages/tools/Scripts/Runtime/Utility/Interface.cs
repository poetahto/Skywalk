using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace poetools.Runtime
{
    [PublicAPI]
    public static class Interface
    {
        /// <summary>
        /// Searches all provided objects for ones that implement the designated interface.
        /// Instead of allocating space for the results, we simply store them in the provided array.
        /// </summary>
        /// <remarks>Elements are simply added to the array - the user must clear it beforehand.</remarks>
        /// <param name="objects">The objects to search.</param>
        /// <param name="results">An array to store the results.</param>
        /// <param name="includeInactive">Should disabled components be included?</param>
        /// <typeparam name="T">The type to search for.</typeparam>
        /// <returns></returns>
        
        public static List<T> Find<T>(IEnumerable<GameObject> objects, List<T> results, bool includeInactive = false)
        {
            foreach (var rootGameObject in objects)
            {
                T[] childrenInterfaces = rootGameObject.GetComponentsInChildren<T>(includeInactive);
                
                foreach (T childInterface in childrenInterfaces)
                    results.Add(childInterface);
            }
            
            return results;
        }

        #region Find Overloads

            /// <summary>
            /// Searches all provided objects for ones that implement the designated interface.
            /// </summary>
            /// <param name="objects">The objects to search.</param>
            /// <param name="includeInactive">Should disabled components be included?</param>
            /// <typeparam name="T">The type to search for.</typeparam>
            /// <returns></returns>
            
            public static List<T> Find<T>(IEnumerable<GameObject> objects, bool includeInactive = false)
            {
                var interfaces = new List<T>();
                return Find(objects, interfaces, includeInactive);
            }

            /// <summary>
            /// Searches all objects in the provided scene for ones that implement the designated interface.
            /// Instead of allocating space for the results, we simply store them in the provided array.
            /// </summary>
            /// <remarks>Elements are simply added to the array - the user must clear it beforehand.</remarks>
            /// <param name="scene">The scene to search.</param>
            /// <param name="results">An array to store the results.</param>
            /// <param name="includeInactive">Should disabled components be included?</param>
            /// <typeparam name="T">The type to search for.</typeparam>
            /// <returns></returns>
            
            public static List<T> Find<T>(Scene scene, List<T> results, bool includeInactive = false)
            {
                GameObject[] rootGameObjects = scene.GetRootGameObjects();
                return Find(rootGameObjects, results, includeInactive);
            }

            /// <summary>
            /// Searches all objects in the provided scene for ones that implement the designated interface.
            /// </summary>
            /// <param name="scene">The scene to search.</param>
            /// <param name="includeInactive">Should disabled components be included?</param>
            /// <typeparam name="T">The type to search for.</typeparam>
            /// <returns></returns>
            
            public static List<T> Find<T>(Scene scene, bool includeInactive = false)
            {
                GameObject[] rootGameObjects = scene.GetRootGameObjects();
                return Find<T>(rootGameObjects, includeInactive);
            }

            /// <summary>
            /// Searches all objects in the active scene for ones that implement the designated interface.
            /// Instead of allocating space for the results, we simply store them in the provided array.
            /// </summary>
            /// <remarks>Elements are simply added to the array - the user must clear it beforehand.</remarks>
            /// <param name="results">An array to store the results.</param>
            /// <param name="includeInactive">Should disabled components be included?</param>
            /// <typeparam name="T">The type to search for.</typeparam>
            /// <returns></returns>
            
            public static List<T> Find<T>(List<T> results, bool includeInactive = false)
            {
                Scene scene = SceneManager.GetActiveScene();
                return Find(scene, results, includeInactive);
            }

            /// <summary>
            /// Searches all objects in the active scene for ones that implement the designated interface.
            /// </summary>
            /// <param name="includeInactive">Should disabled components be included?</param>
            /// <typeparam name="T">The type to search for.</typeparam>
            /// <returns></returns>
            
            public static List<T> Find<T>(bool includeInactive = false)
            {
                Scene scene = SceneManager.GetActiveScene();
                return Find<T>(scene, includeInactive);
            }

        #endregion
    }
}