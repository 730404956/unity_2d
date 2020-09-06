using UnityEngine;

public class UIBase : MonoBehaviour
{
    private void Awake()
    {
    }
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    public void ChangeShowState(bool visible)
    {
        if (visible)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    //***************************************rewrite*************************
}