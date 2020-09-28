using UnityEngine;

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
        panel.Init(backpack.GetActor(), item);
        panel.Show();
    }
    public void SetUp(Actor actor)
    {
        backpack?.RemoveOnAddItemListener(AddNewItem);
        backpack?.RemoveOnRemoveItemListener(RemoveItem);
        backpack = actor.GetBackpack();
        Start();
        backpack.AddOnAddItemListener(AddNewItem);
        backpack.AddOnRemoveItemListener(RemoveItem);
        gearUI.SetUp(actor.GetGear());
    }
}