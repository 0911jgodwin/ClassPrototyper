using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Utility/AbilitySwap")]
public class Scriptable_AbilitySwap : EffectBase
{
    public AbilityBase oldAbility;
    public AbilityBase newAbility;
    public bool retainCooldown;

    public override IEffect InitialiseEffect(GameObject obj)
    {
        AbilitySwap effect = new AbilitySwap(this, obj);
        return effect;
    }
}
