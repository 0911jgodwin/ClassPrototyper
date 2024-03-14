using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Utility/Dispel")]
public class Scriptable_Dispel : EffectBase
{
    public EffectBase effect;

    public override IEffect InitialiseEffect(GameObject obj)
    {
        DispelEffect effect = new DispelEffect(this, obj);
        return effect;
    }
}
