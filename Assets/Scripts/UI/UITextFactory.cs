using UnityEngine;
using System.Collections.Generic;
public class UITextFactory
{
    public UIText uIText_prototype;
    protected Transform default_pos;
    private void Init(Transform default_pos)
    {
        GameManager.instance.objectPool.AddPrototype(uIText_prototype);
    }
    public void ShowText(string text, Transform aim_pos)
    {
        UIText uIText = GameManager.instance.objectPool.GetRecycleObject(uIText_prototype);
        uIText.ShowText(text, aim_pos);
    }
    public void OnDamageShown(IDamageable dst, Damager src)
    {
        ShowText("" + src.damage, dst.GetTransform());
    }
}