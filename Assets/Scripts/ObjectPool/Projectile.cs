using UnityEngine;
using System;
using UnityEngine.Events;
namespace Acetering{
[RequireComponent(typeof(Moveable))]
public class Projectile : Damager
{
    [Serializable]
    public class OnHitEvent : UnityEvent<GameObject> { };
    [SerializeField]
    public OnHitEvent onHit;
    [Tooltip("show effect after collision")]
    public Explosion explosionEffect;
    public float max_distance = 100f;
    public bool destoryAfterCollision = true;
    protected float current_distance = 0f;
    protected Moveable move_motor;


    /// <summary>
    /// launch forward the direction
    /// </summary>
    /// <param name="direction"></param>
    public void Launch(IActorPart src, Vector2 start_pos, Vector2 direction, int layer, float speed_rate = 1, float damage_rate = 1)
    {
        this.src = src;
        gameObject.layer = layer;
        this.transform.position = start_pos;
        if (beforeDamage == null)
        {
            beforeDamage = new DamageEvent();
        }
        beforeDamage.AddListener((damager, damageable) => damager.damage = (int)(damager.damage * damage_rate));
        ObjectInit();
        move_motor.FaceToDirectionImmediately(direction);
        move_motor.speed *= speed_rate;
    }
    public override void OnObjectInit()
    {
        base.OnObjectInit();
        move_motor = GetComponent<Moveable>();
        current_distance = 0f;
    }
    public override void OnObjectCreate(IRecycleObjectFactory factory)
    {
        base.OnObjectCreate(factory);
    }
    public override void OnObjectDestroy()
    {
        base.OnObjectDestroy();
        beforeDamage.RemoveAllListeners();
        Explosion explosion = GameManager.instance.objectPool.GetRecycleObject<Explosion>(explosionEffect);
        explosion.transform.position = transform.position;
        explosion.ObjectInit();
    }

    private void Update()
    {
        current_distance += move_motor.MoveForward();
        if (current_distance > max_distance)
        {
            ObjectDestroy();
        }
    }
    public virtual void Hit(GameObject target)
    {
        DoDamage(target);
        onHit?.Invoke(target);
        if (destoryAfterCollision)
        {
            ObjectDestroy();
        }
    }
    protected void OnCollisionEnter2D(Collision2D other)
    {
        Hit(other.gameObject);
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other.gameObject);
    }
}}