using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
namespace Acetering
{
    public class EquipmentItem : BaseItem
    {
        public Equipment equipment;
        private ItemOperation opt;
        protected override void Awake()
        {
            base.Awake();
            opt = new ItemOperation("装备", OperationOverTag.Close_Info_After_Op, 10, EquipItem, ChangeOperation);
            if (operations == null)
            {
                operations = new List<ItemOperation>();
            }
            operations.Add(opt);
        }
        protected void EquipItem(Item item, IActorPart part)
        {
            part.GetGear().Equip(equipment);
        }
        protected void TakeOffItem(Item item, IActorPart part)
        {
            part.GetGear().TakeOff(equipment);
        }
        public override void DropItem(IBackpack backpack)
        {
            TakeOffItem(this, backpack);
            base.DropItem(backpack);
        }

        public void ChangeOperation(Item item, IActorPart part)
        {
            if (part.GetGear().GetEquipSlotName(equipment) != String.EquipSlotName.None)
            {
                opt.Change("取下", TakeOffItem, ChangeOperation, CollectItem);
            }
            else
            {
                opt.Change("装备", EquipItem, ChangeOperation);
            }
        }
    }
}