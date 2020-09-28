using UnityEngine;
using System.Collections.Generic;
public class UITextFactory 
{
    public int initCapacity = 2;
    public UIText protype;
    protected List<UIText> texts;
    protected Transform default_pos;
    private void Init(Transform default_pos)
    {
        this.default_pos = default_pos;
        texts = new List<UIText>();
        for (int i = 0; i < initCapacity; i++) {
            createNewText();
        }
    }
    public void ShowText(string text,Transform aim_pos)
    {
        bool handle = false;
        foreach (UIText t in texts)
        {
            if (!t.gameObject.activeSelf)
            {
                t.transform.SetParent(aim_pos);
                t.transform.localPosition = Vector2.zero;
                t.ShowText(text);
                handle = true;
                break;
            }
        }
        if (!handle)
        {
            createNewText().ShowText(text);
        }
    }
    protected UIText createNewText()
    {
        UIText ui_text = GameObject.Instantiate(protype);
        ui_text.transform.SetParent(default_pos);
        texts.Add(ui_text);
        return ui_text;
    }
    public void OnDamageShown(IDamageable dst, Damager src)
    {
        ShowText(""+src.damage,dst.GetTransform());
    }
}