/*
 * File: Equipment.cs
 * Project/package: Equipment
 * File Created: Friday, 13th March 2020 1:24:02 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Monday, 18th January 2021 11:01:10 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
namespace Acetering
{
    [Serializable]
    public class EquipmentEvent : UnityEvent<Equipment> { };

    public class Equipment : MonoBehaviour
    {
        [SerializeField]
        protected EquipmentEvent onEquip, onTakeOff;
        public String.EquipmentType type;
        public Damager damager;
        public IEquipmentGear gear;
        public EquipmentItem item;

        /// <summary>
        /// trigger when take on equipment
        /// </summary>
        public virtual void OnEquip(IEquipmentGear gear)
        {
            gameObject.SetActive(true);
            damager = GetComponent<Damager>();
            if (damager != null)
            {
                damager.src = gear;
            }
            this.gear = gear;
            onEquip.Invoke(this);
        }
        /// <summary>
        /// trigger when take off equipment
        /// </summary>
        public virtual void OnTakeOff()
        {
            onTakeOff.Invoke(this);
            this.gear = null;
            gameObject.SetActive(false);
        }
        public bool BindModel(Transform tf)
        {
            if (tf)
            {
                transform.SetParent(tf);
                transform.localPosition = Vector2.zero;
                transform.localRotation = Quaternion.identity;
                print("bind model for [" + name + "] success.");
                return true;
            }
            print("bind model failed. No transform for [" + name + "] of type :" + type);
            return false;
        }
    }
}
