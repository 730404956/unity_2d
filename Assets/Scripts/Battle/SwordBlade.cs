using UnityEngine;
using System;
using System.Collections;
namespace Acetering
{
    class HitCount
    {
        int counter;
        int max;
        public HitCount(int max)
        {
            this.max = max;
            counter = 1;
        }
        public bool Test()
        {
            if (max > counter)
            {
                counter++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    [Serializable]
    public class SwordBlade : Damager, DamagerController
    {

        [Range(1, 20)]
        public int damage_count = 1;
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!activate)
            {
                return;
            }
            Damageable aim = other.GetComponent<Damageable>();
            if (aim)
            {
                if (aim.IsInvisible())
                {
                    return;
                }
                bool hit_success = false;
                if (hit_enemys.ContainsKey(aim.name))
                {
                    if (((HitCount)hit_enemys[aim.name]).Test())
                    {
                        hit_success = aim.ReceiveDamage(this);
                    }
                }
                else
                {
                    hit_enemys.Add(aim.name, new HitCount(damage_count));
                    hit_success = aim.ReceiveDamage(this);
                }
                if (hit_success & use_force)
                {
                    other.attachedRigidbody.AddForce(force);
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerStay2D(other);
        }
    }
}