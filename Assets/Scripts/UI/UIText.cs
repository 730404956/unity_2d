using UnityEngine;
using UnityEngine.UI;
namespace Acetering{
public class UIText : RecycleObject
{
    Text mText;
    Animator anim;
    public static string animName = "default";

    public override void OnObjectCreate(IRecycleObjectFactory factory)
    {
        mText = GetComponent<Text>();
        anim = GetComponent<Animator>();
    }
    public void ShowText(string text,Transform t)
    {
        transform.SetParent(t);
        transform.localPosition = Vector2.zero;
        mText.text = text;
        ObjectInit();
    }
    public override void OnObjectInit()
    {
        anim.Play(animName, 0, 0);
    }
    public override void OnObjectDestroy()
    {
        
    }
}}