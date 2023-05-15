using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "PotionData", menuName = "ScriptableObject/PotionData")]
public class PotionData : ScriptableObject 
{
    public new string name;
    public int damageAmount;
    [ShowAssetPreview] public Sprite sprite;
}