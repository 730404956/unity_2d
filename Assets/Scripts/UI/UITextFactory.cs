using UnityEngine;
using System.Collections.Generic;
public class UITextFactory : MonoBehaviour
{
    public int initCapacity = 2;
    public UIText protype;
    protected List<UIText> texts;
    private void Start()
    {
        texts = new List<UIText>();
        for (int i = 0; i < initCapacity; i++) {
            createNewText();
        }
    }
    public void ShowText(string text)
    {
        bool handle = false;
        foreach (UIText t in texts)
        {
            if (!t.gameObject.activeSelf)
            {
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
        UIText ui_text = Instantiate(protype);
        ui_text.transform.SetParent(transform);
        texts.Add(ui_text);
        return ui_text;
    }
    public void OnDamageShown(IDamageable dst, Damager src)
    {
        ShowText(""+src.damage);
    }
}