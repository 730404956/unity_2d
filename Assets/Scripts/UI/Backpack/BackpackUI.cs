using UnityEngine;

public class BackpackUI : UIBase
{
    protected IBackpack backpack;
    public EquipmentGearUI gearUI;
    public ItemInfoPanel panel;
    [SerializeField]
    protected Transform content;
    public void Start()
    {
        foreach (Item item in backpack.GetItems())
        {
            item.transform.SetParent(content);
        }
    }
    public virtual void AddNewItem(Item item, IBackpack backpack)
    {
        item.transform.SetParent(content);
        item.gameObject.SetActive(true);

    }
    public virtual void RemoveItem(Item item, IBackpack backpack)
    {
        item.transform.SetParent(null);
        item.gameObject.SetActive(false);
    }
    public virtual void ShowItemInfo(Item item)
    {
        panel.Init(backpack.GetActor(), item);
        panel.Show();
    }
    public void SetUp(Actor actor)
    {
        backpack = actor.GetBackpack();
        backpack.AddOnAddItemListener(AddNewItem);
        backpack.AddOnRemoveItemListener(RemoveItem);
        gearUI.SetUp(actor.GetGear());
    }
}