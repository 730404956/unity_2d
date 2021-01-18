using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
namespace Acetering
{
    [Serializable]
    public class ItemEvent : UnityEvent<Item, IBackpack> { };

    public interface Item
    {
        /// <summary>
        /// show info on UI panel
        /// </summary>
        void ShowInfo();
        /// <summary>
        /// get itemname method
        /// </summary>
        /// <returns>the name shown on UI</returns>
        string GetName();
        /// <summary>
        /// 
        /// </summary>
        /// <returns>the description shown on UI</returns>
        string GetDescription();
        /// <summary>
        /// 
        /// </summary>
        /// <returns>the attr shown on UI</returns>
        string GetAttribute();
        /// <summary>
        /// 
        /// </summary>
        /// <returns>the type of the item</returns>
        string GetItemType();
        /// <summary>
        /// 
        /// </summary>
        /// <returns>the image shown in backpack UI</returns>
        Image GetImage();
        /// <summary>
        /// operations that the item accepts
        /// </summary>
        /// <returns>operations</returns>
        List<ItemOperation> GetOperations();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t">set ui pos in backpack or grid</param>
        void SetUIPos(Transform t);
    }
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class BaseItem : MonoBehaviour, Item
    {
        protected string id;
        [SerializeField]
        protected string type;
        public Collectable collectable;
        public string item_name;
        public string description;
        [Tooltip("please set this component in inspector")]
        private Image image;
        [SerializeField]
        protected string attr;
        public List<ItemOperation> operations;
        protected virtual void Awake()
        {
            image = GetComponent<Image>();
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(ShowInfo);
        }
        /// <summary>
        /// call backpack ui to show the info of this item.
        /// </summary>
        public void ShowInfo()
        {
            GameManager.instance.backPackUI.ShowItemInfo(this);
        }
        /// <summary>
        /// add item to backpack
        /// </summary>
        /// <param name="backpack"></param>
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
        //********************************************impl
        public string GetDescription() { return description; }
        public string GetName() { return item_name; }
        public virtual string GetAttribute() { return attr; }
        public Image GetImage() { return image; }
        public string GetItemType() { return type; }
        public List<ItemOperation> GetOperations()
        {
            operations.Sort();
            return operations;
        }

    }
    [Serializable]
    public class ItemOperation : IComparable<ItemOperation>
    {
        [Range(0, 100)]
        public int priority = 50;
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
        public ItemOperation(string name, OperationOverTag tag, int priority, params UnityAction<Item, IBackpack>[] callBack) : this(name, tag, callBack)
        {
            this.priority = priority;
        }
        public void Change(string name, params UnityAction<Item, IBackpack>[] callBack)
        {
            this.operation_name = name;
            foreach (var c in callBack)
            {
                this.callBack.AddListener(c);
            }
        }
        public int CompareTo(ItemOperation other)
        {
            return priority - other.priority;
        }
    }
    public enum OperationOverTag { Default, Close_Info_After_Op, Close_Backpack_After_Op }
};