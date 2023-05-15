using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObject/Recipe")]
public class Recipe : ScriptableObject 
{
    public List<RecipeEntry> components;
    [Expandable] public PotionData resultPotion;
}