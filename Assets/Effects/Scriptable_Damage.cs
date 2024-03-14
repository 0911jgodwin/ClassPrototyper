using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Damage/DirectDamage")]
public class Scriptable_Damage : EffectBase
{
    public float minDamage;
    public float maxDamage;

    public override IEffect InitialiseEffect(GameObject obj)
    {
        InstantEffect effect = new InstantEffect(this, obj);
        return effect;
    }
}
