using UnityEngine;
using System;
using System.Collections.Generic;

public class EquipmentGearUI : MonoBehaviour
{
    IBackpack backpack;
    public List<EquipmentSlotUI> slots;
    IEquipmentGear m_gear;

    public void SetUp(IEquipmentGear gear)
    {
        m_gear?.RemoveOnEquipListener(Equip);
        m_gear = gear;
        gear.AddOnEquipListener(Equip);
    }
    public void Equip(Equipment equipment)
    {
        String.EquipSlotName slotName = equipment.gear.GetEquipSlotName(equipment);
        foreach (var slot in slots) {
            if (slot.slot_name == slotName) {
                equipment.item.SetUIPos(slot.transform);
            }
        }
    }
    public void TakeOff(Equipment equipment, IEquipmentGear gear)
    {
        
    }
}
[Serializable]
public class EquipmentSlotUI
{
    public String.EquipSlotName slot_name;
    public String.EquipmentType slot_type;
    public RectTransform transform;
}