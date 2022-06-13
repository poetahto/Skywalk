using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [RequireComponent(typeof(LayoutGroup))]
    public class LayoutGroupController : MonoBehaviour
    {
        [Header("Default Settings")]
        [SerializeField] private GameObject childPrefab;
        [SerializeField] private int spawnCount;
        
        private void Start()
        {
            Clear();
            AddChildren(childPrefab, spawnCount);
        }

        public void AddChildren(GameObject prefab, int count)
        {
            for (int i = 0; i < count; i++)
                Instantiate(prefab, transform);
        }

        public void Clear()
        {
            for (int i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }
    }
}