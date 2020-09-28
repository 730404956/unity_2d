/*
 * File: Equipment.cs
 * Project/package: Equipment
 * File Created: Friday, 13th March 2020 1:24:02 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Sunday, 6th September 2020 2:14:01 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
using UnityEngine.Events;
using System;
[Serializable]
public class EquipmentEvent : UnityEvent<Equipment> { };

public abstract class Equipment : MonoBehaviour
{
    [SerializeField]
    protected EquipmentEvent onEquip, onTakeOff;
    public EquipmentType type;
    public IEquipmentGear gear;
    public EquipmentItem item;

    /// <summary>
    /// trigger when take on equipment
    /// </summary>
    public virtual void OnEquip(IEquipmentGear gear)
    {
        gameObject.SetActive(true);
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
            print("bind model failed.");
            return true;
        }
        print("bind model success.");
        return false;
    }
}
public enum EquipmentType
{
    Weapon, Body
}