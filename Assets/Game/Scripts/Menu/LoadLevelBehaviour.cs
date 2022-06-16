using UnityEngine;

namespace Game.Scripts
{
    public class LoadLevelBehaviour : MonoBehaviour
    {
        public void Test(string sceneName)
        {
            SceneTransitionSystem.Instance.TransitionToScene(sceneName);
        }
    }
}