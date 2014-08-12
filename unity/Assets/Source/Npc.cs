using UnityEngine;

namespace Assets.Source
{
    public class Npc : MonoBehaviour
    {

        public float MoveSpeed;
        public float Range;
        public Transform Target;

        private CharacterController _controller;
        public AnimationClip AttackClip;
        public AnimationClip Idle;
        public AnimationClip Run;
        public AnimationClip DefendClip;
        public AnimationClip DieClip;

        public float MaxHealth;
        public float CurrentHealth;
        public float AttackDamage;
        private bool isDefending;

        public float ImpactTime;
        private bool _isImpacting;

        public void Awake ()
        {
            CurrentHealth = MaxHealth;
            _controller = GetComponent<CharacterController>();
        }

        public void Update()
        {
            if (InRange())
            {
                animation.CrossFade(AttackClip.name);
                Attack();
                if (animation[AttackClip.name].time > 0.9 * animation[AttackClip.name].length)
                {
                    _isImpacting = false;
                }
            }
            else
            {
                Chase();
            }

            if (IsDead())
                Die();
        }

        public void Attack()
        {
            if (animation[AttackClip.name].time > animation[AttackClip.name].length * ImpactTime && !_isImpacting && animation[AttackClip.name].time < 0.9 * animation[AttackClip.name].length)
            {
                Target.GetComponent<Player>().Defend(AttackDamage, transform);
                _isImpacting = true;
            }
        }

        private bool InRange()
        {
            return Vector3.Distance(transform.position, Target.position) < Range;
        }

        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }

        public void Defend(float damage)
        {
            CurrentHealth -= damage;
        }

        private void Die()
        {
            animation.Play(DieClip.name);
            if (animation[DieClip.name].time > animation[DieClip.name].length*0.95f)
                Destroy(gameObject);
        }

        private void Chase()
        {
            transform.LookAt(Target.position);
            _controller.SimpleMove(transform.forward * MoveSpeed);
            animation.CrossFade(Run.name);
        }

        public void OnMouseOver()
        {
            Target.gameObject.GetComponent<Player>().Target = transform;
        }
    }
}
