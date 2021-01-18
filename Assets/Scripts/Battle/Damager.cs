using UnityEngine;
using UnityEngine.Events;
using System.Collections;
namespace Acetering
{
    public interface DamagerController
    {
        void Activate();
        void Inactivate();
    }
    public class DamageEvent : UnityEvent<Damager, Damageable> { }
    public class Damager : RecycleObject
    {

        public int damage { get { return (int)(m_damage * atk_rate); } set { m_damage = value; } }
        [SerializeField]
        protected int m_damage;
        public float atk_rate { get { return m_atk_rate; } set { m_atk_rate = Mathf.Clamp(value, 0, 10); } }
        [SerializeField]
        protected float m_atk_rate;
        //TODO:set src
        public IActorPart src { get; set; }
        protected DamageEvent beforeDamage, afterDamage;
        protected bool activate = false;
        protected Hashtable hit_enemys = new Hashtable(5);
        protected Vector2 force;
        protected bool use_force = false;

        /// <summary>
        /// do damage to a damager, if success, return true;
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool DoDamage(Damageable target)
        {
            beforeDamage?.Invoke(this, target);
            bool hit = target.ReceiveDamage(this);
            if (hit)
            {
                afterDamage?.Invoke(this, target);
            }
            return hit;
        }
        public void Activate()
        {
            activate = true;
            hit_enemys.Clear();
        }
        public void Inactivate()
        {
            activate = false;
        }
        public void SetForce(Vector2 force)
        {
            use_force = true;
            this.force = force;
        }
        public void ClearForce()
        {
            use_force = false;
        }

        public bool DoDamage(GameObject target)
        {
            Damageable tar = target.GetComponent<Damageable>();
            if (tar != null)
                return DoDamage(tar);
            return false;
        }
        //***********************************setter&getter
        public IActorPart GetSrc()
        {
            return src;
        }
        //***********************************empty impl
        public override void OnObjectInit()
        {
            atk_rate = 1;
        }
        public override void OnObjectDestroy()
        {

        }
        public override void OnObjectCreate(IRecycleObjectFactory factory)
        {

        }
    }
}
