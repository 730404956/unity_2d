/*
 * File: WeaponLibrary.cs
 * Project/package: Equipment
 * File Created: Monday, 16th March 2020 6:20:23 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Sunday, 6th September 2020 2:49:50 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
/// <summary>
/// library to store actors weapons in bag, current weapon in hands can be exchanged with those in library through ExchangeCurrentWeapon();
/// </summary>
public class EquipmentGear : ActorPart, IEquipmentGear
{

    public List<EquipmentSlot> slots = new List<EquipmentSlot>();
    public EquipmentEvent onEquip, OnTakeOff;
    public void Equip(Equipment equipment)
    {
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.isEmpty && slot.type == equipment.type)
            {
                equipment.OnEquip(this);
                onEquip.Invoke(equipment);
                slot.SetEquipment(equipment);
                print("equip to empty pos");
                return;
            }
        }
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.type == equipment.type)
            {
                print("change equipment");
                equipment.OnEquip(this);
                onEquip.Invoke(equipment);
                slot.SetEquipment(equipment);
                return;
            }
        }
    }
    public void TakeOff(Equipment equipment)
    {
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.m_equipment == equipment)
            {
                slot.Clear();
                OnTakeOff.Invoke(equipment);
                equipment.OnTakeOff();
            }
        }
    }
    public Weapon GetWeapon()
    {
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.type == String.EquipmentType.Weapon)
            {
                return (Weapon)slot.m_equipment;
            }
        }
        return null;
    }
    public String.EquipSlotName GetEquipSlotName(Equipment equipment)
    {
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.m_equipment == equipment)
            {
                return slot.slot_name;
            }
        }
        return String.EquipSlotName.None;
    }
    //******************************impl
    public void AddOnEquipListener(UnityAction<Equipment> listener)
    {
        onEquip.AddListener(listener);
    }
    public void RemoveOnEquipListener(UnityAction<Equipment> listener)
    {
        onEquip.RemoveListener(listener);
    }
    public void AddOnTakeOffListener(UnityAction<Equipment> listener)
    {
        OnTakeOff.AddListener(listener);
    }
    public void RemoveOnTakeOffListener(UnityAction<Equipment> listener)
    {
        OnTakeOff.RemoveListener(listener);
    }


}
[Serializable]
public class EquipmentSlot
{
    public String.EquipSlotName slot_name;
    public bool isEmpty { get { return m_equipment == null; } }
    public Transform model_slot;
    public String.EquipmentType type;
    [HideInInspector]
    public Equipment m_equipment;
    public void SetEquipment(Equipment equipment)
    {
        m_equipment = equipment;
        equipment.BindModel(model_slot);
    }
    public void Clear()
    {
        m_equipment = null;
    }
    public bool Equals(EquipmentSlotUI obj)
    {
        return obj.slot_name.Equals(slot_name);
    }
}
