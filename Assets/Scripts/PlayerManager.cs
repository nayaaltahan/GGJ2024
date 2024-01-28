using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        
        public Transform PlayerTransform { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                Debug.LogError("There can only be one PlayerManager in the scene!");
                return;
            }

            PlayerTransform = FindObjectOfType<PlayerController>().transform;
        }
    }
}