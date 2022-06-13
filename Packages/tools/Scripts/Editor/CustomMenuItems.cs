using UnityEngine;
using UnityEditor;

namespace poetools.Editor
{
    public class CustomMenuItems : MonoBehaviour
    {
        [MenuItem("GameObject/poetools/3D Trigger", false, 10)]
        private static void Create3DTrigger(MenuCommand menuCommand)
        {
            var newObject = CreateNewGameObject(menuCommand);

            newObject.name = "Trigger";
            newObject.AddComponent<BoxCollider>();
            newObject.AddComponent<Trigger>();
        }
        
        [MenuItem("GameObject/poetools/Debug Tools", false, 10)]
        private static void CreateFrameCounter(MenuCommand menuCommand)
        {
            GameObject newObject = CreateNewGameObject(menuCommand);

            newObject.name = "Debug Tools";
            newObject.AddComponent<DebugSettings>();
        }

        private static GameObject CreateNewGameObject(MenuCommand menuCommand)
        {
            var newObject = new GameObject();
            GameObjectUtility.SetParentAndAlign(newObject, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(newObject, "Create " + newObject.name);
            Selection.activeObject = newObject;

            return newObject;
        }
    }
}