using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySwap : InstantEffect
{
    AbilityManager manager;
    Scriptable_AbilitySwap abilitySwap;
    public AbilitySwap(EffectBase _effect, GameObject _obj) : base(_effect, _obj)
    {
        abilitySwap = (Scriptable_AbilitySwap)effect;
        manager = GameObject.FindObjectOfType<AbilityManager>();
    }

    public override void PerformEffect()
    {
        manager.SwapAbility(abilitySwap.oldAbility, abilitySwap.newAbility, abilitySwap.retainCooldown);
    }
}
