/*
 * File: Weapon.cs
 * Project/package: Equipment
 * File Created: Friday, 13th March 2020 1:29:30 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Sunday, 6th September 2020 2:16:57 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;

[RequireComponent(typeof(Moveable))]
public class Weapon : Equipment
{
    public EquipmentEvent OnWeaponUse, OnWeaponUseFinish;
    public virtual void Use()
    {
        OnWeaponUse?.Invoke(this, gear);
    }
    public virtual void FinishUsing()
    {
        OnWeaponUseFinish?.Invoke(this, gear);
    }
    /// <summary>
    /// call when remove from weapon library
    /// unbind energy_bar
    /// </summary>
    /// <param name="gear"></param>
    public override void OnTakeOff(IEquipmentGear gear)
    {
        FinishUsing();
        base.OnTakeOff(gear);
    }
}