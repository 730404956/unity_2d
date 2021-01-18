using UnityEngine;
namespace Acetering
{

    public class BackpackUI : UIBase
    {
        protected IBackpack backpack;
        public EquipmentGearUI gearUI;
        public ItemInfoPanel panel;
        [SerializeField]
        protected Transform content;
        protected void Start()
        {
            foreach (Item item in backpack.GetItems())
            {
                item.SetUIPos(content);
            }
        }
        public virtual void AddNewItem(Item item, IBackpack backpack)
        {
            item.SetUIPos(content);

        }
        public virtual void RemoveItem(Item item, IBackpack backpack)
        {
            item.SetUIPos(null);
        }
        public virtual void ShowItemInfo(Item item)
        {
            panel.BindItem(backpack, item);
            panel.Show();
        }
        public void SetUp(IActorPart actor)
        {
            backpack?.RemoveOnAddItemListener(AddNewItem);
            backpack?.RemoveOnRemoveItemListener(RemoveItem);
            backpack = actor.GetBackpack();
            panel.Init(this);
            Start();
            backpack.AddOnAddItemListener(AddNewItem);
            backpack.AddOnRemoveItemListener(RemoveItem);
            gearUI.SetUp(actor.GetGear());
        }
        public override void Show()
        {
            GameManager.instance.main_actor.GetController().SetEnable(false);
            base.Show();
        }
        public override void Hide()
        {
            base.Hide();
            GameManager.instance.main_actor.GetController().SetEnable(true);
        } 
    }
}