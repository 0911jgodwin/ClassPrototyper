using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispelEffect : InstantEffect
{
    EffectManager manager;
    Scriptable_Dispel dispelEffect;
    GameObject _obj;
    public DispelEffect(EffectBase _effect, GameObject _obj) : base(_effect, _obj)
    {
        dispelEffect = (Scriptable_Dispel)effect;
        manager = obj.GetComponent<EffectManager>();
    }

    public override void PerformEffect()
    {
        manager.RemoveEffect(dispelEffect.effect.InitialiseEffect(obj));
    }
}