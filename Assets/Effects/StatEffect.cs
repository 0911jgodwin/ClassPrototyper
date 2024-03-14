using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEffect : TimedEffect
{
    private StatsSystem statComponent;
    private StatModifier statMod;
    private Scriptable_Stat effectStat;
    public StatEffect(EffectBase _effect, GameObject _obj) : base(_effect, _obj)
    {
        statComponent = obj.GetComponent<StatsManager>().StatsSystem;
    }

    protected override void ApplyEffect()
    {
        effectStat = (Scriptable_Stat)effect;
        statMod = CalculateModifier();
        statComponent.AddModifier(effectStat.stat, statMod);
    }

    private StatModifier CalculateModifier()
    {
        switch (effectStat.statModifier.ModType)
        {
            case StatModType.Flat:
                return new StatModifier(effectStat.statModifier.ModType, effectStat.statModifier.Value * effectStacks);
            case StatModType.PercentAdditive:
                return new StatModifier(effectStat.statModifier.ModType, effectStat.statModifier.Value * effectStacks);
            case StatModType.PercentMultiplicative:
                float modValue = 1f;
                for (int i = 0; i < effectStacks; i++)
                {
                    modValue *= 1 + effectStat.statModifier.Value;
                }
                return new StatModifier(effectStat.statModifier.ModType, modValue);
        }
        return effectStat.statModifier;
    }

    public override void Activate()
    {
        if (effect.stackable && effectStacks < effect.stackCap || duration <= 0)
        {
            Stack();
            effectStacks++;
            ApplyEffect();
        }
        if (effect.refreshable || duration <= 0)
        {
            duration = effect.duration;
        }
    }

    public void Stack()
    {
        if (statMod != null)
        {
            statComponent.RemoveModifier(effectStat.stat, statMod);
        }
    }

    public override void End()
    {
        for (int i = 0; i < effectStacks; i++)
        {
            statComponent.RemoveModifier(effectStat.stat, statMod);
        }
        effectStacks = 0;
    }
}
