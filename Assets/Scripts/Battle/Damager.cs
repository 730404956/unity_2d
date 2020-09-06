using UnityEngine;

public class Damager : MonoBehaviour
{
    
    public int damage { get { return m_damage; } set {m_damage = value; } }
    [SerializeField]
    protected int m_damage;

    private Collider2D m_collider;

    protected virtual void Start()
    {
        print("in damager start");
        m_collider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// do damage to a damager, if success, return true;
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public virtual bool DoDamage(Damageable target)
    {
        return target.ReceiveDamage(this);
    }

    public void DoDamage(GameObject target)
    {
        Damageable tar= target.GetComponent<Damageable>();
        if(tar!=null)
            DoDamage(tar);
    }

}