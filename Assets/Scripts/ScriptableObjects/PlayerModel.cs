using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "MonkeyGTA/PlayerModel", order = 0)]
    public class PlayerModel : ScriptableObject
    {
        public float speed = 10.0f;
        public float jumpForce = 10.0f;
        public LayerMask groundLayer; // Layer to identify what is considered as ground
        public float groundCheckDistance = 0.1f; // Distance to check for ground
        public float gravity = -9.81f; // Gravity force
        public AnimationCurve GravityCurve;
        public float TimeToFullGravity = 2f;
        public AnimationCurve JumpCurve;
        public float AirTime = 2f;
    }
}