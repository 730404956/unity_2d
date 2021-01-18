using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
namespace Acetering{
public class ItemInfoPanel : UIBase
{
    private BackpackUI backpackUI;
    private Text item_name;
    private Text item_attr;
    private Text item_type;
    private Text item_desc;
    private Image item_img;
    private Transform buttons_group;
    public UIButton btn_prototype;
    protected List<UIButton> buttons = new List<UIButton>();
    public void Init(BackpackUI backpackUI)
    {
        this.backpackUI = backpackUI;
        item_name = transform.Find("name").GetComponent<Text>();
        item_attr = transform.Find("attr").GetComponent<Text>();
        item_desc = transform.Find("desc").GetComponent<Text>();
        item_type = transform.Find("type").GetComponent<Text>();
        item_img = transform.Find("img").GetComponent<Image>();
        buttons_group = transform.Find("buttons");
        print("ui panel init done");
    }
    public override void Show()
    {
        base.Show();
    }
    public void BindItem(IActorPart item_owner, Item item)
    {
        item_name.text = item.GetName();
        item_desc.text = item.GetDescription();
        item_attr.text = item.GetAttribute();
        item_type.text = item.GetItemType();
        item_img.sprite = item.GetImage().sprite;
        item_img.color = item.GetImage().color;
        int i = 0;
        IBackpack backpack = item_owner.GetBackpack();
        foreach (ItemOperation op in item.GetOperations())
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
}}