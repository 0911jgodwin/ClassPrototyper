using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEffect : TimedEffect
{
    EffectManager effectManager;
    Scriptable_Custom customEffect;
    public CustomEffect(EffectBase _effect, GameObject _obj) : base(_effect, _obj)
    {
        customEffect = (Scriptable_Custom)effect;
        effectManager = obj.GetComponent<EffectManager>();
    }

    public override void End()
    {
        effectManager.AddEffect(customEffect.endEffect.InitialiseEffect(obj));
    }

    protected override void ApplyEffect()
    {
        effectManager.AddEffect(customEffect.startEffect.InitialiseEffect(obj));
    }
}
