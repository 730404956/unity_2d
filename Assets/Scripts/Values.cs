using UnityEngine;
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
}