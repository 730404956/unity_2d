using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
namespace Acetering
{
    public class Backpack : ActorPart, IBackpack
    {
        public IActorPart owner;

        public List<Item> items = new List<Item>();
        [SerializeField]
        public ItemEvent OnAddItem, OnRemoveItem;
        public virtual void AddItem(Item item)
        {
            items.Add(item);
            OnAddItem?.Invoke(item, this);
        }
        public virtual void RemoveItem(Item item)
        {
            items.Remove(item);
            OnRemoveItem?.Invoke(item, this);
        }
        public IActorPart GetOwner()
        {
            return owner;
        }
        public List<Item> GetItems()
        {
            return items;
        }
        //*********************************impl
        public void AddOnAddItemListener(UnityAction<Item, IBackpack> listener)
        {
            OnAddItem.AddListener(listener);
        }
        public void RemoveOnAddItemListener(UnityAction<Item, IBackpack> listener)
        {
            OnAddItem.RemoveListener(listener);
        }
        public void AddOnRemoveItemListener(UnityAction<Item, IBackpack> listener)
        {
            OnRemoveItem.AddListener(listener);
        }
        public void RemoveOnRemoveItemListener(UnityAction<Item, IBackpack> listener)
        {
            OnRemoveItem.RemoveListener(listener);
        }
    }
}
