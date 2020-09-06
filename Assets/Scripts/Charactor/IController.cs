using UnityEngine;
using System;

public class IController : MonoBehaviour
{
    protected Moveable move_motor;
    protected IEquipmentGear gear;
    protected Animator anim;
    protected virtual void Start()
    {
        //get Moveable component
        move_motor = GetComponent<Moveable>();
        gear = GetComponent<EquipmentGear>();
        anim = GetComponent<Animator>();
    }
    protected virtual void Update() {
        if (!Mathf.Approximately(move_motor.face_direction[0], 0))
        {
            if (move_motor.face_direction[0] > 0)
                anim.SetFloat("move_x", 1);
            else
                anim.SetFloat("move_x", -1);
        }
    }
    public void Disable(bool disable) {
        this.enabled = !disable;
    }
    public virtual void OnDamaged(IDamageable self, Damager src)
    {
        print(this + "damaged by" + src);
    }
    public virtual void OnDied(IDamageable self, Damager src) { 
        print(this + "killed by" + src);
        Destroy(this);
        Destroy(gameObject);
    }
}