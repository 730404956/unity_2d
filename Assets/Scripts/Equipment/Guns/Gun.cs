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
public class Gun : MonoBehaviour
{
    public bool infinity_bullets = true;
    [Range(0, Values.MAX_ACCURACY)]
    public float accuracy = 100;
    public int max_bullets = 1;
    protected Projectile bullet_prefab;
    public float reload_time = 1f;
    public Transform fire_pos;
    public bool auto_shoot = false;
    public float shooting_interval = 1f;
    protected bool shooting = false;
    protected float shooting_cooldown_timmer;
    protected bool shooting_cooldown = false;
    protected int m_current_bullets;
    protected float m_reload_timer;
    protected bool reloading = false;
    [SerializeField]
    protected float bullet_speed_up = 0f;
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


    protected virtual void UpdateEnergyBar()
    {
        // if (energy_bar != null)
        // {
        //     energy_bar?.SetBarPercentage(m_current_bullets / (float)max_bullets);
        // }
    }
    /// <summary>
    /// call once take out from weapon library
    /// </summary>
    public virtual void OnShow()
    {
        gameObject.SetActive(true);
        UpdateEnergyBar();
    }
    /// <summary>
    /// call when put back to weapon
    /// </summary>
    public virtual void OnHide(Equipment equipment,IEquipmentGear gear)
    {
        FinishUsing(equipment,gear);
        gameObject.SetActive(false);
    }
    protected virtual void ReloadComplete()
    {
        m_current_bullets = max_bullets;
        UpdateEnergyBar();
    }
    public virtual void Reload()
    {
        Invoke("ReloadComplete", reload_time);
    }

    public void Use(Equipment equipment, IEquipmentGear gear)
    {
        if (auto_shoot)
        {
            shooting = true;
            StartCoroutine(AutoShoot(equipment,gear));
        }
        else if (IsShootable())
        {
            Fire(equipment,gear);
        }
    }
    public void FinishUsing(Equipment equipment, IEquipmentGear gear)
    {
        if (auto_shoot)
        {
            shooting = false;
        }
    }
    protected virtual void Fire(Equipment equipment, IEquipmentGear gear)
    {
        //instantiate bullet object instance
        Projectile bullet = Instantiate(bullet_prefab, fire_pos.position, Quaternion.identity);
        //set transform parent
        bullet.transform.SetParent(GameObject.Find("projectiles").transform);
        //launch bullet
        Vector2 bia = new Vector2(-move_motor.face_direction.y, move_motor.face_direction.x);
        Vector2 direction = move_motor.face_direction + bia * getBias();
        bullet.Launch(direction, gear.GetLayer(), bullet_speed_up);
        //set gun's cool down timer
        shooting_cooldown_timmer = shooting_interval;
        //make gun cooldown
        shooting_cooldown = true;
        if (!infinity_bullets)
        {
            //cost bullet
            m_current_bullets -= 1;
            UpdateEnergyBar();
            if (m_current_bullets <= 0)
            {
                FinishUsing(equipment, gear);
                Reload();
            }
        }
    }
    protected bool IsShootable()
    {
        return !reloading && !shooting_cooldown && (infinity_bullets || m_current_bullets > 0);
    }
    protected IEnumerator AutoShoot(Equipment equipment,IEquipmentGear gear)
    {
        while (shooting && IsShootable())
        {
            Fire(equipment, gear);
            yield return new WaitForSeconds(shooting_interval);
        }

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