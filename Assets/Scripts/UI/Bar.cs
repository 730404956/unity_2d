using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConsumEvent : UnityEvent<Consumable> { }
public interface Consumable
{
    int GetConsumableMax();
    int GetConsumableNow();
}
public class Bar : MonoBehaviour
{
    public Image mask;
    float original_size;

    void Start()
    {
        original_size = mask.rectTransform.rect.width;
    }
    /// <summary>
    /// update health bar with percentage
    /// </summary>
    /// <param name="current_percentage"></param>
    public void SetBarPercentage(float current_percentage)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, original_size * current_percentage);
    }
    public void UpdateBar(Consumable consumable)
    {
        SetBarPercentage(consumable.GetConsumableNow() / (float)consumable.GetConsumableMax());
    }
    public void UpdateBar(MonoBehaviour consumable)
    {
        if (consumable is Consumable)
        {
            UpdateBar(consumable as Consumable);
        }
    }
    public void UpdateBar(MonoBehaviour consumable, MonoBehaviour other)
    {
        UpdateBar(consumable);
    }

}