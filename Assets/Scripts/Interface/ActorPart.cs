using UnityEngine;

public class ActorPart : MonoBehaviour, IActorPart
{
    public IActor actor;
    public IDamageable damageable;
    protected IBackpack backpack;
    protected IEquipmentGear gear;
    protected IController controller;
    private void Awake()
    {
        damageable = GetComponent<IDamageable>();
        gear = GetComponent<IEquipmentGear>();
        backpack = GetComponent<IBackpack>();
        actor = GetComponent<IActor>();
        controller = GetComponent<IController>();
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
    public int GetLayer() {
        return gameObject.layer;
    }
    public Transform GetTransform() {
        return transform;
    }
    public IController GetController() {
        return controller;
    }
}