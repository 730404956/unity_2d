using UnityEngine;
using System;
using UnityEngine.Events;
[RequireComponent(typeof(Moveable))]
public class Projectile : MonoBehaviour
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
    protected float current_distance = 0f;
    protected Moveable move_motor;
    protected bool isMoving = false;
    public bool destoryAfterCollision = true;
    public bool isTrigger;


    /// <summary>
    /// launch forward the direction
    /// </summary>
    /// <param name="direction"></param>
    public void Launch(Vector2 direction,int layer, float speed = 0,Collider2D src=null)
    {
        if (move_motor == null)
        {
            move_motor = GetComponent<Moveable>();
        }
        gameObject.layer = layer;
        current_distance = 0;
        move_motor.FaceToDirectionImmediately(direction);
        move_motor.speed += speed;
        isMoving = true;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isMoving)
        {
            current_distance += move_motor.MoveForward();
            if (current_distance > max_distance)
            {
                Destroy(gameObject);
                Destroy(this);
            }
        }
    }
    protected void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity).Play();
        onHit?.Invoke(other.gameObject);
        if (destoryAfterCollision)
        {
            DestroySelf();
        }
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger to"+ LayerMask.LayerToName(other.gameObject.layer));
        Instantiate(explosionEffect, transform.position, Quaternion.identity).Play();
        //check if this is a trigger
        if (isTrigger)
        {
            onHit?.Invoke(other.gameObject);
            if (destoryAfterCollision)
            {
                DestroySelf();
            }
        }
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
        Destroy(this);
    }
}