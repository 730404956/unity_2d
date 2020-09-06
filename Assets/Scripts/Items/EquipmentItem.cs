using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
public class EquipmentItem : Item
{
    public Equipment equipment;
    private ItemOperation op_equip, op_takeoff;
    protected override void Awake()
    {
        base.Awake();
        op_equip = new ItemOperation("装备", OperationOverTag.Close_Info_After_Op, EquipItem, ChangeOperation);
        op_takeoff = new ItemOperation("取下", OperationOverTag.Close_Info_After_Op, TakeOffItem, CollectItem, ChangeOperation);
        if (operations == null)
        {
            operations = new List<ItemOperation>();
        }
        operations.Add(op_equip);
        for (int i = operations.Count - 1; i > 0; i--)
        {
            operations[i] = operations[i - 1];
        }
        operations[0] = op_equip;
    }
    public void EquipItem(Item item, IActorPart part)
    {
        part.GetGear().Equip(equipment);
    }
    public void TakeOffItem(Item item, IActorPart part)
    {
        part.GetGear().TakeOff(equipment);
    }
    public override void DropItem(IBackpack backpack) {
        TakeOffItem(this,backpack);
        base.DropItem(backpack);
    }

    public void ChangeOperation(Item item, IActorPart part)
    {
        if (part.GetGear().GetEquipSlotName(equipment) != String.EquipSlotName.None)
        {
            operations[0] = op_takeoff;
        }
        else
        {
            operations[0] = op_equip;
        }
    }
}