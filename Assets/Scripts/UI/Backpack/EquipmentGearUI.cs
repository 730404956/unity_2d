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
        m_gear = gear;
        gear.AddOnEquipListener(Equip);
    }
    public void Equip(Equipment equipment, IEquipmentGear gear)
    {
        String.EquipSlotName slotName = gear.GetEquipSlotName(equipment);
        foreach (var slot in slots) {
            if (slot.slot_name == slotName) {
                equipment.item.SetPos(slot.transform);
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
    public EquipmentType slot_type;
    public RectTransform transform;
}