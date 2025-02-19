using UnityEngine;
using TMPro;
public enum DamageTextColors
{
    PlayerDamaged,
    PlayerCritDamaged,
    EnemyDamaged,
    EnemyCritDamaged,
}
public class DamageText : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    private void Destroy()
    {
        Destroy(gameObject);
    }
    public void SetText(float damage, DamageTextColors color)
    {
        text.text = ((int)damage).ToString();
        text.color = GetDamageTextColor(color);
    }
    public Color GetDamageTextColor(DamageTextColors color)
    {
        switch (color)
        {
            case DamageTextColors.PlayerDamaged:
                FMODUnity.RuntimeManager.PlayOneShot("event:/Weapons_Blade_Hit");
                return new Color(0.990566f, 0.359781f, 0.5709701f); //FD5C92
            case DamageTextColors.PlayerCritDamaged:
                return new Color(0.9528302f, 0.2651744f, 0.3845264f); //F34462
            case DamageTextColors.EnemyDamaged:
            FMODUnity.RuntimeManager.PlayOneShot("event:/Weapons_NoAmmo");
                return new Color(0.97169f, 0.6261627f, 0.3620951f); //F8A05C
            case DamageTextColors.EnemyCritDamaged:
                return new Color(0.9811321f, 0.4264505f, 0.3378426f); //FA6D56
            default:
                return Color.white;
        }
    }
}