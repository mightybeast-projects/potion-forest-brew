using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TweenEngine;

public class BrewerBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> _potSlots;
    [SerializeField] private List<Recipe> _recipes;
    [SerializeField] private PotionSpawnerBehaviour _potionSpawner; 
 
    private List<RecipeEntry> _optionEntries;
    private Recipe _foundedRecipe;

    public void Brew()
    {
        GetOptionColors();
        FindRecipe();
        ShowResult();
        EmptySlots();
    }

    private void GetOptionColors()
    {
        _optionEntries = new List<RecipeEntry>();

        foreach (GameObject slot in _potSlots)
            AddNewRecipeEntry(slot);
    }

    private void AddNewRecipeEntry(GameObject slot)
    {
        Color newColor = Color.clear;
        if (slot.transform.childCount > 0)
        {
            Transform item = slot.transform.GetChild(0);
            newColor = item.GetComponent<ItemBehaviour>().color;
        }
        RecipeEntry re = new RecipeEntry(1, newColor);

        if (_optionEntries.Exists(x => x.color == re.color))
            _optionEntries.Find(x => x.color == re.color).amount++;
        else
            _optionEntries.Add(re);
    }

    private void FindRecipe()
    {
        foreach (Recipe r in _recipes)
        {
            _foundedRecipe = r;
            foreach (RecipeEntry re in _optionEntries)
            {
                if (!r.components.Contains(re))
                {
                    _foundedRecipe = null;
                    break;
                }
            }
            if (_foundedRecipe == r) break;
        }
    }

    private void ShowResult()
    {
        if (_foundedRecipe != null)
            _potionSpawner.SpawnNewPotion(_foundedRecipe.resultPotion);
    }

    private void EmptySlots()
    {
        foreach (GameObject slot in _potSlots)
            if (SlotIsNotEmpty(slot))
                foreach (Transform item in slot.transform)
                    Tween(this, 
                        Sequence(
                            For(0.05f).Move(item.GetComponent<ItemBehaviour>()).To(transform.position),
                            Callback(() => Destroy(item.gameObject))
                        )
                    );
    }

    private bool SlotIsNotEmpty(GameObject slot)
    {
        return slot.transform.childCount > 0;
    }
}