/*
 * File: Values.cs
 * Project/package: Scripts
 * File Created: Friday, 16th October 2020 12:34:03 pm
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
public class Values
{
    public static float float_zero { get; } = 0.01f;
    public static float float_angle_zero { get; } = 0.02f;

    public const float MAX_ACCURACY = 100;
    public const float MAX_ACCURACY_BIAS = 1;
}
public class LayerManager
{
    public static int GetOpenetLayer(int layer)
    {
        string name = LayerMask.LayerToName(layer);
        MonoBehaviour.print("check layer " + name + " ");
        if (name.Equals("Player"))
        {
            MonoBehaviour.print("opponent layer: Enemy");
            return LayerMask.GetMask("Enemy");
        }
        else if (name.Equals("Enemy"))
        {
            MonoBehaviour.print("opponent layer: Player");
            return LayerMask.GetMask("Player");
        }
        else
        {
            MonoBehaviour.print("opponent layer: Default");
            return 0;
        }
    }
}
public class String
{
    public enum EquipSlotName
    {
        None,
        Head,
        Body,
        Hand1,
        Hand2,
        Ring,
        Shoes
    };
    public enum EquipmentType
    {
        Weapon, Body
    }
}}