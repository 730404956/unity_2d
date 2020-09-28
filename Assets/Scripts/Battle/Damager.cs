using UnityEngine;
using UnityEngine.Events;
public class DamageEvent : UnityEvent<Damager, Damageable> { }
public class Damager : MonoBehaviour
{

    public int damage { get { return m_damage; } set { m_damage = value; } }
    [SerializeField]
    protected int m_damage;
    protected IActorPart src;
    protected DamageEvent beforeDamage, afterDamage;


    protected virtual void Start()
    {
        print("in damager start");
    }

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
}