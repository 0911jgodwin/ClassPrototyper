using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Scriptable_DoT dotEffect;
    public Scriptable_Stat statEffect;
    public Scriptable_Damage damageEffect;
    private readonly Dictionary<string,TimedEffect> effects = new Dictionary<string, TimedEffect>();

    private void Update()
    {
        List<TimedEffect> effectList = new List<TimedEffect>(effects.Values);
        foreach (var effect in effectList)
        {
            effect.Tick(Time.deltaTime);
            if(effect.isFinished)
            {
                effects.Remove(effect.effect.effectName);
            }
        }
    }

    public void AddEffect(IEffect effect)
    {
        if (effect is TimedEffect)
        {
            TimedEffect thisEffect = (TimedEffect)effect;
            if (effects.ContainsKey(thisEffect.effect.effectName))
            {
                effects[thisEffect.effect.effectName].Activate();
            }
            else
            {
                effects.Add(thisEffect.effect.effectName, thisEffect);
                thisEffect.Activate();
            }
        }
        else if (effect is InstantEffect) {
            InstantEffect thisEffect = (InstantEffect)effect;
            thisEffect.PerformEffect();
        }
    }

    public void RemoveEffect(IEffect effect)
    {
        if (effect is TimedEffect)
        {
            TimedEffect thisEffect = (TimedEffect)effect;
            effects.Remove(thisEffect.effect.effectName);
        }
    }

    public bool HasEffect(EffectBase effect)
    {
        if (effects.ContainsKey(effect.effectName)) { return true; }
        return false;
    }

    public List<TimedEffect> GetDebuffs()
    {
        return new List<TimedEffect>(effects.Values);
    }
}
