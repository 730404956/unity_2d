using UnityEngine;
using System;
using UnityEngine.Events;
public class Damageable : ActorPart, IDamageable, Consumable
{
    [Serializable]
    public class OnDamaged : UnityEvent<IDamageable, Damager> { };
    [SerializeField]
    protected int m_max_health;//max health points
    protected int m_current_health;//current health, won't be bigger than max
    protected bool invincible = false;//if true, ignore damage
    protected float invincible_timer;// timer to time the rest invincible time
    [SerializeField]
    protected float invincible_time = 0.5f;//invincible time after be damaged



    [SerializeField]
    [Tooltip("call back when damaged but before update hp")]
    protected OnDamaged beforeDamaged;
    [SerializeField]
    [Tooltip("call back when damaged and after update hp(should update hp bar, show damage text...)")]
    protected OnDamaged onDamaged;
    [SerializeField]
    [Tooltip("after damaged and update hp, if still alive, call this method")]
    protected OnDamaged afterDamaged;
    [SerializeField]
    [Tooltip("after damaged and update hp, if died, call this method")]
    protected OnDamaged died;


    protected virtual void Start()
    {
        //set health as max
        m_current_health = m_max_health;
    }
    protected virtual void Update()
    {
        if (invincible)
        {
            invincible_timer -= Time.deltaTime;
            if (invincible_timer < 0)
            {
                invincible = false;
            }
        }
    }
    /// <summary>
    /// hit or damaged by a damager
    /// </summary>
    /// <param name="src"></param>
    public virtual bool ReceiveDamage(Damager src)
    {
        if (!invincible)
        {
            //check if damage is less than 0
            if (src.damage < 0)
            {
                print("damage shouldn't be less than 0!");
            }
            beforeDamaged?.Invoke(this, src);
            //update current health
            m_current_health = Mathf.Clamp(m_current_health - src.damage, 0, m_current_health);
            //call back to OnDamaged method, deal with damaged event
            onDamaged?.Invoke(this, src);
            //died
            if (m_current_health == 0)
            {
                died?.Invoke(this, src);
            }
            else
            {//not die
                //set invincible time
                SetInvincible(invincible_time);
                //call back after damaged listener
                afterDamaged?.Invoke(this, src);
            }
            //receive damage success
            return true;
        }
        else
        {
            //receive damage failed, because of invincible time
            return false;
        }
    }
    public bool SetInvincible(float time)
    {
        if (time > Values.float_zero)
        {
            //set invincible
            invincible = true;
            //set invincible timer
            invincible_timer = time;
            return invincible;
        }
        return false;
    }
    //*************************consumable
    public int GetConsumableMax()
    {
        return m_max_health;
    }
    public int GetConsumableNow()
    {
        return m_current_health;
    }
    //**************************listener******************************
    public void AddBeforeDamageListener(UnityAction<IDamageable, Damager> listener)
    {
        beforeDamaged.AddListener(listener);
    }
    public void AddAfterDamageListener(UnityAction<IDamageable, Damager> listener)
    {
        afterDamaged.AddListener(listener);
    }
    public void RemoveAfterDamageListener(UnityAction<IDamageable, Damager> listener)
    {
        afterDamaged.RemoveListener(listener);
    }
    public void RemoveBeforeDamageListener(UnityAction<IDamageable, Damager> listener)
    {
        beforeDamaged.RemoveListener(listener);
    }
    public int GetCurrentHP()
    {
        return m_current_health;
    }
    public int GetMaxHP()
    {
        return m_max_health;
    }
}