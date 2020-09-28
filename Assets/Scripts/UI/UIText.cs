using UnityEngine;
using UnityEngine.UI;
public class UIText : MonoBehaviour
{
    Text mText;
    Animator anim;
    public static string animName = "default";
    private void Awake()
    {
        mText = GetComponent<Text>();
        anim = GetComponent<Animator>();
    }
    public void ShowText(string text)
    {
        if (mText == null)
        {
            mText = GetComponent<Text>();
            anim = GetComponent<Animator>();
        }
        mText.text = text;
        gameObject.SetActive(true);
        anim.Play(animName, 0, 0);
    }
    public void OnPlayFinish()
    {
        gameObject.SetActive(false);

    }

}