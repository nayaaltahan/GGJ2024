using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "MonkeyGTA/PlayerModel", order = 0)]
    public class PlayerModel : ScriptableObject
    {
        public float speed = 10.0f;
    }
}