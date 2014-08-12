using UnityEngine;

namespace Assets.Source
{
    public class Attack : MonoBehaviour
    {
        public Transform Target;
        public AnimationClip attack;
        public AnimationClip DieClip;
        public bool IsAttacking;
        public float AttackDamage;
        public float ImpactTime;
        private bool _isImpacting;
        public float Health;
        public bool IsDying;

        public void Update () 
        {
            if (IsDead())
            {
                Die();
                return;
            }

            if (Target != null && Input.GetKey(KeyCode.Space))
            {
                animation.CrossFade(attack.name);
                IsAttacking = true;
                transform.LookAt(Target.position);
            }

            if (animation[attack.name].time >= 0.9 * animation[attack.name].length)
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
            if (Target != null && animation.IsPlaying(attack.name) && !_isImpacting)
            {
                if (animation[attack.name].time >= ImpactTime && animation[attack.name].time >= 0.9 * animation[attack.name].length)
                {
                    Target.GetComponent<NPC>().Defend(AttackDamage);
                    _isImpacting = true;
                }
            }
        }

        public bool IsDead()
        {
            return Health <= 0;
        }

        public void Defend(float damage)
        {
            Health -= damage;
        }
    }
}
