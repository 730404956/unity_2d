using UnityEngine;
using UnityEngine.UI;
namespace Acetering
{
    public interface Consumable
    {
        int GetConsumableMax();
        int GetConsumableNow();
    }
    public class Bar : MonoBehaviour
    {
        protected Image mask;
        protected float original_size;

        public void Init()
        {
            mask = transform.GetChild(0).Find("mask").GetComponent<Image>();
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
        public void SetBarPercentage(Consumable consumable)
        {
            SetBarPercentage(consumable.GetConsumableNow() / (float)consumable.GetConsumableMax());
        }
    }
}