using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
[Serializable]
public class ItemEvent : UnityEvent<Item, IBackpack> { };
public interface Item
{
    void ShowInfo();
    string GetName();
    string GetDescription();
    string GetAttribute();
    Image GetImage();
    List<ItemOperation> GetOperations();
    void SetUIPos(Transform t);
}
public class BaseItem : MonoBehaviour, Item
{
    protected string id;
    public Collectable collectable;
    public string item_name;
    public string description;
    [Tooltip("please set this component in inspector")]
    public Image image;
    [SerializeField]
    protected string attr;
    public List<ItemOperation> operations;
    public void ShowInfo()
    {
        GameManager.instance.backPackUI.ShowItemInfo(this);
    }
    public void CollectItem(IBackpack backpack)
    {
        backpack.AddItem(this);
    }

    public void SetUIPos(Transform tf)
    {
        if (tf)
        {
            transform.SetParent(tf);
            transform.localPosition = Vector2.zero;
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public virtual void DropItem(IBackpack backpack)
    {
        backpack.RemoveItem(this);
        collectable.Init(backpack.GetTransform().position);
    }
    //***********************************rewrite
    public void CollectItem(Collectable collectable, IBackpack backpack)
    {
        CollectItem(backpack);
    }
    public void CollectItem(Item item, IBackpack backpack)
    {
        CollectItem(backpack);
    }
    public void DropItem(Item item, IBackpack backpack)
    {
        DropItem(backpack);
    }
    protected virtual void Awake()
    {

    }
    //********************************************impl
    public string GetDescription() { return description; }
    public string GetName() { return item_name; }
    public virtual string GetAttribute() { return attr; }
    public Image GetImage() { return image; }
    public List<ItemOperation> GetOperations() { return operations; }

}
[Serializable]
public class ItemOperation
{
    public string operation_name;
    public ItemEvent callBack;
    public OperationOverTag tag;
    public ItemOperation(string name, OperationOverTag tag, params UnityAction<Item, IBackpack>[] callBack)
    {
        this.callBack = new ItemEvent();
        operation_name = name;
        foreach (var c in callBack)
        {
            this.callBack.AddListener(c);
        }
        this.tag = tag;
    }
}
public enum OperationOverTag { Default, Close_Info_After_Op, Close_Backpack_After_Op };