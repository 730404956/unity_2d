using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
public interface IActorPart
{
    IEquipmentGear GetGear();
    IBackpack GetBackpack();
    IActor GetActor();
    IController GetController();
    IDamageable GetDamageable();
    int GetLayer();
    Vector2 GetPosition();
}
public interface IDamageable
{
    int GetCurrentHP();
    int GetMaxHP();
    bool ReceiveDamage(Damager damager);
    void AddBeforeDamageListener(UnityAction<IDamageable, Damager> listener);
    void AddAfterDamageListener(UnityAction<IDamageable, Damager> listener);
    void RemoveAfterDamageListener(UnityAction<IDamageable, Damager> listener);
    void RemoveBeforeDamageListener(UnityAction<IDamageable, Damager> listener);
}
public interface IActor : IActorPart
{
}
public interface IEquipmentGear : IActorPart
{
    void Equip(Equipment equipment);
    void TakeOff(Equipment equipment);
    String.EquipSlotName GetEquipSlotName(Equipment equipment);
    void AddOnEquipListener(UnityAction<Equipment, IEquipmentGear> listener);
    void RemoveOnEquipListener(UnityAction<Equipment, IEquipmentGear> listener);
    void AddOnTakeOffListener(UnityAction<Equipment, IEquipmentGear> listener);
    void RemoveOnTakeOffListener(UnityAction<Equipment, IEquipmentGear> listener);
    Weapon GetWeapon();
}
public interface IBackpack : IActorPart
{
    void AddItem(Item item);
    void RemoveItem(Item item);
    List<Item> GetItems();
    void AddOnAddItemListener(UnityAction<Item, IBackpack> listener);
    void RemoveOnAddItemListener(UnityAction<Item, IBackpack> listener);
    void AddOnRemoveItemListener(UnityAction<Item, IBackpack> listener);
    void RemoveOnRemoveItemListener(UnityAction<Item, IBackpack> listener);
}