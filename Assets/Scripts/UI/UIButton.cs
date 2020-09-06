using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButton : UIBase
{
    protected Button btn;
    private void Awake()
    {
        if (btn == null)
            btn = GetComponent<Button>();
    }
    public void SetText(string text)
    {
        Awake();
        btn.gameObject.transform.GetComponentInChildren<Text>().text = text;
    }
    public void SetOnclick(UnityAction call)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(call);
    }

}