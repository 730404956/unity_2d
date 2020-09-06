using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    protected string id;
    public Collectable collectable;
    public string item_name;
    public string description;
    public Image image;
    public string attr;
    public List<ItemOperation> operations;
    public void ShowInfo()
    {
        GameManager.instance.backPackUI.ShowItemInfo(this);
        print("show item info");
    }
    public void CollectItem(IBackpack backpack)
    {
        backpack.AddItem(this);
        gameObject.SetActive(true);
    }
    public void SetPos(RectTransform tf)
    {
        if (tf)
        {
            gameObject.SetActive(true);
            transform.SetParent(tf);
            transform.localPosition = Vector2.zero;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public virtual void DropItem(IBackpack backpack)
    {
        backpack.RemoveItem(this);
        collectable.Init(backpack.GetPosition());
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