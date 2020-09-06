using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {
    public Image mask;
    float original_size;

    void Start(){
        original_size = mask.rectTransform.rect.width;
    }
    /// <summary>
    /// update health bar with percentage
    /// </summary>
    /// <param name="current_percentage"></param>
    public void SetBarPercentage(float current_percentage){
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, original_size * current_percentage);
    }
    public void UpdateHpBar(IDamageable self, Damager src) {
        SetBarPercentage(self.GetCurrentHP() /(float)self.GetMaxHP());
    }

}