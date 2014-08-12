using UnityEngine;

namespace Assets.Source
{
    public class Player : MonoBehaviour
    {
        public Transform Target;
        public AnimationClip AttackClip;
        public AnimationClip DieClip;
        public bool IsAttacking;
        public float AttackDamage;
        public float ImpactTime;
        private bool _isImpacting;
        public float Health;
        public bool IsDying;

        public float CombatEscapeTimeTotal;
        public float CurrentEscapeTime;

        public void Update () 
        {
            if (IsDead())
            {
                Die();
                return;
            }

            if (Target != null && Input.GetKey(KeyCode.Space))
            {
                animation.CrossFade(AttackClip.name);
                IsAttacking = true;
                transform.LookAt(Target.position);
            }

            if (animation[AttackClip.name].time >= 0.9 * animation[AttackClip.name].length)
            {
                IsAttacking = false;
                _isImpacting = false;
            }

            Impact();
        }

        private void Die()
        {
            if (!IsDying)
            {
                animation.Play(DieClip.name);
                IsDying = true;
            }

//            if (IsDying && !animation.IsPlaying(DieClip.name)) 
                    // done dying
        }

        public void Impact()
        {
            if (Target != null && animation.IsPlaying(AttackClip.name) && !_isImpacting &&
                (animation[AttackClip.name].time >= ImpactTime*animation[AttackClip.name].length &&
                 animation[AttackClip.name].time <= 0.9*animation[AttackClip.name].length))
            {
                StartEscapeTimer();
                Target.GetComponent<Npc>().Defend(AttackDamage);
                _isImpacting = true;
            }
        }

        public void StartEscapeTimer()
        {
            CurrentEscapeTime = CombatEscapeTimeTotal;
            CancelInvoke("Escape");
            InvokeRepeating("Escape", 1, 1);
        }

        public void Escape()
        {
            CurrentEscapeTime -= 1;
            if (CurrentEscapeTime <= 0)
                Target = null;
        }

        public bool IsDead()
        {
            return Health <= 0;
        }

        public void Defend(float damage, Transform offender)
        {
            // target = first target that hits the player
            if (Target == null)
            {
                StartEscapeTimer();
                Target = offender;
            }
            Health -= damage;
        }
    }
}
