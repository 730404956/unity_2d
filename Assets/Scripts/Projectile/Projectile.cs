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
    [Tooltip("the force when hit something")]
    public float collision_force = 100f;
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
    public void Launch(IActorPart src, Vector2 direction, int layer, float speed_rate = 1, float damage_rate=1)
    {
        this.src = src;
        if (move_motor == null)
        {
            move_motor = GetComponent<Moveable>();
        }
        gameObject.layer = layer;
        current_distance = 0;
        move_motor.FaceToDirectionImmediately(direction);
        move_motor.speed *= speed_rate;
        beforeDamage.AddListener((damager, damageable) => damager.damage = (int)(damage_rate * damage_rate));
        gameObject.SetActive(true);
    }

    private void Update()
    {
        current_distance += move_motor.MoveForward();
        if (current_distance > max_distance)
        {
            DestroySelf();
        }

    }
    public virtual void Hit(GameObject target)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity).Play();
        DoDamage(target);
        onHit?.Invoke(target);
        if (destoryAfterCollision)
        {
            DestroySelf();
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

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
        Destroy(this);
    }
}