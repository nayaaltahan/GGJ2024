using System.Linq;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

namespace Feature.NPC.Scripts.States
{
    public class AttackingState : NpcBaseState
    {
        private float _lastAttackTimestamp;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private Collider[] _hitColliders = new Collider[1];

        public AttackingState(NpcState type) : base(type)
        {
        }

        public override void OnEnterState(NpcStateController stateController)
        {
            base.OnEnterState(stateController);
            stateController.SetAgentAlertSettings();
            stateController.DisableNavMeshAgent();
        }

        public override void OnUpdate(NpcStateController stateController)
        {
            base.OnUpdate(stateController);
            stateController.transform.LookAt(stateController.PlayerTransform);
            stateController.TargetPosition = stateController.PlayerTransform.position + stateController.RandomPoint * stateController.Settings.PatrolToAttackDistance;

            if (CanAttack(stateController.Settings.AttackCooldown))
            {
                _lastAttackTimestamp = Time.time;
                StartAttack(stateController);
            }

            if (Vector3.Distance(stateController.TargetPosition, stateController.transform.position) > stateController.Settings.ExitAttackStateRange)
            {
                stateController.SetState(NpcState.Chase);
            }
        }

        private async void StartAttack(NpcStateController stateController)
        {
            stateController.NpcAnimator.SetTrigger(Attack);

            await UniTask.Delay(stateController.Settings.AttackProjectileDelay);
            AudioManager.instance.Play3DOneShot("event:/SFX/Attacks/impact_hit", stateController.transform.position);
            var hit = Physics.OverlapSphereNonAlloc(stateController.AttackFromTransform.position, 1f, _hitColliders, stateController.Settings.AttackLayer);
            if (hit > 0)
            {
                AudioManager.instance.Play3DOneShot("event:/SFX/monkey_cry", stateController.transform.position);
                var force = stateController.transform.forward * 10;
                stateController.PlayerHealth.TakeDamage(stateController.Settings.AttackDamage, force);
            }
            // Debug draw the spherecast
            
        }

        // Debug draw the spherecast

    private bool CanAttack(float attackCooldown)
    {
        if (_lastAttackTimestamp + attackCooldown < Time.time)
        {
            return true;
        }

        return false;
    }

    public override void OnDrawGizmosSelected(NpcStateController stateController)
    {
        base.OnDrawGizmosSelected(stateController);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(stateController.TargetPosition, .2f);
    }
}

}