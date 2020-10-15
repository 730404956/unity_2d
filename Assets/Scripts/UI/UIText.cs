using UnityEngine;
using UnityEngine.UI;
public class UIText : RecycleObject
{
    Text mText;
    Animator anim;
    public static string animName = "default";

    protected override void OnObjectCreate(IRecycleObjectFactory factory)
    {
        mText = GetComponent<Text>();
        anim = GetComponent<Animator>();
    }
    public void ShowText(string text,Transform t)
    {
        transform.parent = t;
        transform.localPosition = Vector2.zero;
        mText.text = text;
        ObjectInit();
    }
    protected override void OnObjectInit()
    {
        anim.Play(animName, 0, 0);
    }
    protected override void OnObjectDestroy()
    {
        
    }
}