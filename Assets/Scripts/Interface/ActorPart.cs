using UnityEngine;

public class ActorPart : MonoBehaviour, IActorPart
{
    private bool inited = false;
    public IActor actor;
    public IDamageable damageable;
    protected IBackpack backpack;
    protected IEquipmentGear gear;
    protected IController controller;
    private void Awake()
    {
        if (!inited)
        {
            damageable = GetComponent<IDamageable>();
            gear = GetComponent<IEquipmentGear>();
            backpack = GetComponent<IBackpack>();
            actor = GetComponent<IActor>();
            controller = GetComponent<IController>();
            inited = true;
            damageable.Init(gear, backpack, actor, controller, damageable);
            backpack.Init(gear, backpack, actor, controller, damageable);
            actor.Init(gear, backpack, actor, controller, damageable);
            gear.Init(gear, backpack, actor, controller, damageable);
        }
    }
    public void Init(IEquipmentGear gear, IBackpack backpack, IActor actor, IController controller, IDamageable damageable)
    {
        if (inited)
            return;
        this.gear = gear;
        this.backpack = backpack;
        this.actor = actor;
        this.controller = controller;
        this.damageable = damageable;
        inited = true;
    }
    public IEquipmentGear GetGear()
    {
        return gear;
    }
    public IActor GetActor()
    {
        return actor;
    }
    public IDamageable GetDamageable()
    {
        return damageable;
    }
    public IBackpack GetBackpack()
    {
        return backpack;
    }
    public int GetLayer()
    {
        return gameObject.layer;
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public IController GetController()
    {
        return controller;
    }
}