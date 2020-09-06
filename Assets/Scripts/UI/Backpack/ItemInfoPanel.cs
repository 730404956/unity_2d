using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class ItemInfoPanel : UIBase
{
    public BackpackUI backpackUI;
    public Text item_name;
    public Text item_attr;
    public Text item_desc;
    public Image item_img;
    public UIButton btn_prototype;
    public Transform buttons_group;
    protected List<UIButton> buttons = new List<UIButton>();
    public override void Show()
    {
        base.Show();
    }
    public void Init(IActor item_owner, Item item)
    {
        item_name.text = item.item_name;
        item_desc.text = item.description;
        item_attr.text = item.attr;
        item_img.sprite = item.image.sprite;
        item_img.color = item.image.color;
        int i = 0;
        IBackpack backpack = item_owner.GetBackpack();
        foreach (ItemOperation op in item.operations)
        {
            UIButton btn;
            if (i < buttons.Count)
            {
                btn = buttons[i];
            }
            else
            {
                btn = Instantiate(btn_prototype, buttons_group.transform);
                buttons.Add(btn);
            }
            btn.SetText(op.operation_name);
            switch (op.tag)
            {
                default:
                case OperationOverTag.Default:
                    btn.SetOnclick(() => op.callBack.Invoke(item, backpack));
                    break;
                case OperationOverTag.Close_Info_After_Op:
                    btn.SetOnclick(() => { op.callBack.Invoke(item, backpack); Hide(); });
                    break;
                case OperationOverTag.Close_Backpack_After_Op:
                    btn.SetOnclick(() => { op.callBack.Invoke(item, backpack); Hide(); backpackUI.Hide(); });
                    break;
            }
            btn.Show();
            i++;
        }
        for (; i < buttons.Count; i++)
        {
            buttons[i].Hide();
        }
    }
}