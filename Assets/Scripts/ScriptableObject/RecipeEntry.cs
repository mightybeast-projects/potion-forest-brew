using System;
using UnityEngine;

[Serializable]
public class RecipeEntry
{
    public int amount;
    public Color color;

    public RecipeEntry(int amount, Color color)
    {
        this.amount = amount;
        this.color = color;
    }

    public override bool Equals(object obj)
    {
        RecipeEntry rp = obj as RecipeEntry;
        return this.color == rp.color && this.amount == rp.amount;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return amount + " " + color;
    }
}