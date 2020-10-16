using UnityEngine;
using UnityEngine.Events;
public class DamageEvent : UnityEvent<Damager, Damageable> { }
public class Damager : RecycleObject
{

    public int damage { get { return m_damage; } set { m_damage = value; } }
    [SerializeField]
    protected int m_damage;
    protected IActorPart src;
    protected DamageEvent beforeDamage, afterDamage;

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
    protected override void OnObjectCreate(IRecycleObjectFactory factory)
    {
        beforeDamage = new DamageEvent();
        afterDamage = new DamageEvent();
    }
    protected override void OnObjectDestroy()
    {
        beforeDamage.RemoveAllListeners();
    }
    public override IRecycleObject Copy(IRecycleObject prototype)
    {
        if (prototype is Damager)
        {
            Damager damager = prototype as Damager;
            this.m_damage = damager.m_damage;
        }
        return base.Copy(prototype);
    }
    //***********************************setter&getter
    public IActorPart GetSrc()
    {
        return src;
    }
    //***********************************empty impl
    protected override void OnObjectInit() { }
}