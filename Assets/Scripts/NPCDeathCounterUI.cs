using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class NPCDeathCounterUI : MonoBehaviour
    {
        public static int NPCDeathCounter;
        public TMP_Text NPCDeathCounterText;
        
        private void Awake()
        {
            NPCDeathCounter = 0;
        }
        private void Update()
        {
            NPCDeathCounterText.text = $"Pranks Pulled: {NPCDeathCounter}";
        }
    }
}