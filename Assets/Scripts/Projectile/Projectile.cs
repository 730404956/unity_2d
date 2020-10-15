using UnityEngine;
using System;
using UnityEngine.Events;
[RequireComponent(typeof(Moveable))]
public class Projectile : Damager
{
    [Serializable]
    public class OnHitEvent : UnityEvent<GameObject> { };
    [SerializeField]
    public OnHitEvent onHit;
    [Tooltip("show effect after collision")]
    public ParticleSystem explosionEffect;
    public float max_distance = 100f;
    public bool destoryAfterCollision = true;
    protected float current_distance = 0f;
    protected Moveable move_motor;


    /// <summary>
    /// launch forward the direction
    /// </summary>
    /// <param name="direction"></param>
    public void Launch(IActorPart src, Vector2 direction, int layer, float speed_rate = 1, float damage_rate = 1)
    {
        this.src = src;
        gameObject.layer = layer;
        move_motor.FaceToDirectionImmediately(direction);
        move_motor.speed *= speed_rate;
        beforeDamage.AddListener((damager, damageable) => damager.damage = (int)(damager.damage * damage_rate));
        OnObjectInit();
    }
    protected override void OnObjectInit()
    {
        current_distance = 0f;
    }
    protected override void OnObjectCreate(IRecycleObjectFactory factory) {
        move_motor = GetComponent<Moveable>();
    }
    protected override void OnObjectDestroy()
    {
        beforeDamage.RemoveAllListeners();
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
        Instantiate(explosionEffect, transform.position, Quaternion.identity).Play();
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
}