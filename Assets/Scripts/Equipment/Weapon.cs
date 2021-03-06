/*
 * File: Weapon.cs
 * Project/package: Equipment
 * File Created: Friday, 13th March 2020 1:29:30 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Sunday, 17th January 2021 1:57:46 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
namespace Acetering{

[RequireComponent(typeof(Moveable))]
public class Weapon : Equipment
{
    protected EquipmentEvent OnWeaponUse, OnWeaponUseFinish;
    public virtual void Use()
    {
        OnWeaponUse?.Invoke(this);
    }
    public virtual void FinishUsing()
    {
        OnWeaponUseFinish?.Invoke(this);
    }
    //*****************************override*************************
    /// <summary>
    /// call when remove from weapon library
    /// unbind energy_bar
    /// </summary>
    /// <param name="gear"></param>
    public override void OnTakeOff()
    {
        FinishUsing();
        base.OnTakeOff();
    }
}}