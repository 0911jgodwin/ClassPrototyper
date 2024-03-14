using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

[Serializable]
public class CharacterStat
{
    /*public float baseValue;
    protected float lastBaseValue;
    protected bool isDirty = true;
    protected float value;

    protected readonly List<StatModifier> statModifiers;

    public readonly IReadOnlyCollection<StatModifier> StatModifiers;


    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public CharacterStat(float _baseValue) : this()
    {
        baseValue = _baseValue;
    }

    public float Value { 
        get
        {
            if(isDirty || lastBaseValue != baseValue)
            {
                lastBaseValue = baseValue;
                value = CalculateFinalValue();
                isDirty = false;
            }
            return value;
        }
    }

    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
        {
            return -1;
        }
        else if (a.order > b.order)
        {
            return 1;
        }
        return 0;
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        
        return false;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float percentSum = 0;

        for (int i=0; i<statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.type == StatModType.Flat)
            {
                finalValue += mod.value;
            }
            else if (mod.type == StatModType.PercentAdditive) 
            {
                percentSum += mod.value;

                if ( i + 1 >= statModifiers.Count || statModifiers[i+1].type != StatModType.PercentAdditive)
                {
                    finalValue *= 1 + percentSum;
                    percentSum = 0;
                }
            }
            else if (mod.type == StatModType.PercentMultiplicative)
            {
                finalValue *= 1 + mod.value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }*/
}
