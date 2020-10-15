/*
 * File: Gun.cs
 * Project/package: Guns
 * File Created: Friday, 13th March 2020 2:05:48 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Sunday, 6th September 2020 1:55:08 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
using System.Collections;
public class Gun : Weapon, Consumable
{
    public bool infinity_bullets = true;
    public bool auto_shoot = false;
    public float shooting_interval = 1f;
    public float bullet_speed_rate = 1f;
    public float bullet_damage_rate = 1f;
    [Range(0, Values.MAX_ACCURACY)]
    public float accuracy = 100;
    public int max_bullets = 1;
    protected Projectile bullet_prefab;
    public float reload_time = 1f;
    [SerializeField]
    protected Transform fire_pos;
    protected bool shooting = false;
    protected float shooting_cooldown_timmer;
    protected bool shooting_cooldown = false;
    protected int m_current_bullets;
    protected float m_reload_timer;
    protected bool reloading = false;
    protected Moveable move_motor;
    // protected Bar energy_bar;
    protected virtual void Start()
    {
        m_current_bullets = max_bullets;
        bullet_prefab = GetComponentInChildren<Projectile>(true);
        move_motor = GetComponent<Moveable>();
    }
    protected virtual void Update()
    {
        if (shooting_cooldown)
        {
            shooting_cooldown_timmer -= Time.deltaTime;
            if (shooting_cooldown_timmer < 0)
            {
                shooting_cooldown = false;
            }
        }
    }
    public virtual void AimDirection(Vector2 direction)
    {
        move_motor.FaceTowards(direction);
    }
    protected virtual float getBias()
    {
        float bia = (1 - accuracy / Values.MAX_ACCURACY) * Values.MAX_ACCURACY_BIAS;
        print(bia);
        return Random.Range(-bia, bia);
    }

    /// <summary>
    /// call once take out from weapon library
    /// </summary>
    public virtual void OnShow()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// call when put back to weapon
    /// </summary>
    public virtual void OnHide()
    {
        FinishUsing();
        gameObject.SetActive(false);
    }
    protected virtual void ReloadComplete()
    {
        m_current_bullets = max_bullets;
    }
    public virtual void Reload()
    {
        Invoke("ReloadComplete", reload_time);
    }

    public override void Use()
    {
        base.Use();
        if (auto_shoot)
        {
            shooting = true;
            StartCoroutine(AutoShoot());
        }
        else if (IsShootable())
        {
            Fire();
        }
    }
    public override void FinishUsing()
    {
        base.FinishUsing();
        
        if (auto_shoot)
        {
            shooting = false;
        }
    }
    protected virtual void Fire()
    {
        //instantiate bullet object instance
        Projectile bullet = GameManager.instance.objectPool.GetRecycleObject<Projectile>(bullet_prefab);
        //launch bullet
        Vector2 bia = new Vector2(-move_motor.face_direction.y, move_motor.face_direction.x);
        Vector2 direction = move_motor.face_direction + bia * getBias();
        bullet.Launch(gear, direction, gear.GetLayer(), bullet_speed_rate);
        //set gun's cool down timer
        shooting_cooldown_timmer = shooting_interval;
        //make gun cooldown
        shooting_cooldown = true;
        if (!infinity_bullets)
        {
            //cost bullet
            m_current_bullets -= 1;
            FinishUsing();
            if (m_current_bullets <= 0)
            {
                Reload();
            }
        }
    }
    protected bool IsShootable()
    {
        return !reloading && !shooting_cooldown && (infinity_bullets || m_current_bullets > 0);
    }
    protected IEnumerator AutoShoot()
    {
        while (shooting && IsShootable())
        {
            Fire();
            yield return new WaitForSeconds(shooting_interval);
        }

    }
    //**************************************************impl
    public int GetConsumableMax()
    {
        return max_bullets;
    }
    public int GetConsumableNow()
    {
        return m_current_bullets;
    }
    //***************************************************rewrite methods*****************************************************************
    public void AimDirection(float x, float y)
    {
        move_motor.FaceTowards(new Vector2(x, y));
    }
    public void AimAt(float x, float y)
    {
        AimAt(new Vector2(x, y));
    }
    public void AimAt(Vector2 position)
    {
        AimDirection(position - move_motor.position);
    }
}