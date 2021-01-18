using UnityEngine;
using System.Collections.Generic;
namespace Acetering
{
    public class BattlerAnimationManager : MonoBehaviour
    {
        protected Animator anim;
        public float rush_force = 2000;
        public int startCount = 0;
        public int endCount = 0;
        public List<Damager> damagers;
        protected IBattleMoveable moveable;
        public void EquipItem()
        {

        }
        private void Start()
        {
            moveable = transform.parent.GetComponent<IBattleMoveable>();
            anim = GetComponent<Animator>();
        }
        public Animator GetAnimator()
        {
            return anim;
        }
        public void Jump()
        {
            if (!anim.GetBool("in_air"))
            {
                anim.SetTrigger("jump");
            }
        }
        public virtual void Attack()
        {
            anim.SetTrigger("attack");

        }
        public virtual void Damaged()
        {
            anim.SetTrigger("damaged");
        }
        public virtual void Die()
        {
            anim.SetTrigger("die");
        }
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.gameObject.CompareTag("land"))
            {
                anim.SetBool("in_air", false);
            }
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!anim.GetBool("in_air"))
            {
                return;
            }
            if (other.gameObject.CompareTag("land"))
            {
                anim.SetBool("in_air", false);
            }
        }
        //***************************anim callback
        public void AttackStart()
        {
            foreach (var item in damagers)
            {
                item.Activate();
            }
            startCount++;
        }
        public void JumpStart()
        {
            anim.SetBool("in_air", true);
            moveable.Jump();
        }
        public void AttackFinish()
        {
            foreach (var item in damagers)
            {
                item.Inactivate();
            }
            endCount++;
        }
        public void Rush()
        {
            moveable.Rush(rush_force);
        }
        public void Idle()
        {
            anim.ResetTrigger("attack");
            anim.ResetTrigger("jump");
        }
        public void SetDamage(float rate)
        {
            foreach (var item in damagers)
            {
                item.atk_rate = rate;
            }
        }
        public void SetForce(float f)
        {

            foreach (var item in damagers)
            {
                if (f <= 1)
                {
                    item.ClearForce();
                }
                else
                {
                    item.SetForce(new Vector2(f, f) * moveable.GetFaceDirection().x);
                }
            }
        }
    }
}