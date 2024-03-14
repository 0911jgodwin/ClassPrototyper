using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private readonly List<StatModifier> modifiers = new List<StatModifier>();

    private float baseValue = 0f;
    private bool isDirty = true;

    private float value;

    public float Value
    {
        get
        {
            if(isDirty)
            {
                value = CalculateValue();
                isDirty = false;
            }
            return value;
        }
    }

    public Stat(float initialValue) => baseValue = initialValue;
    public Stat(StatType statType) => baseValue = statType.DefaultValue;

    public void UpdateBaseValue(float newBase)
    {
        isDirty = true;

        baseValue = newBase;
    }

    public void AddModifier(StatModifier modifier)
    {
        isDirty = true;

        int index = modifiers.BinarySearch(modifier, new ByPriority());
        if (index <0) { index = ~index; }
        modifiers.Insert(index, modifier);
    }

    public void RemoveModifier(StatModifier modifier)
    {
        isDirty = true;

        modifiers.Remove(modifier);
    }

    private float CalculateValue()
    {
        float finalValue = baseValue;
        float sumPercentAdditive = 0f;

        for (int i = 0; i < modifiers.Count; i++)
        {
            var modifier = modifiers[i];

            switch (modifier.ModType)
            {
                case StatModType.Flat:
                    finalValue += modifier.Value;
                    break;
                case StatModType.PercentAdditive:
                    sumPercentAdditive += modifier.Value;
                    if (i+1 >= modifiers.Count || modifiers[i+1].ModType != StatModType.PercentAdditive)
                    {
                        finalValue *= 1 + sumPercentAdditive;
                    }
                    break;
                case StatModType.PercentMultiplicative:
                    finalValue *= 1 + modifier.Value;
                    break;
            }
        }
        return finalValue;
    }

    private class ByPriority : IComparer<StatModifier>
    {
        public int Compare(StatModifier a, StatModifier b)
        {
            if (a.ModType > b.ModType) { return 1; }
            if (a.ModType < b.ModType) { return -1; }
            return 0;
        }
    }
}
