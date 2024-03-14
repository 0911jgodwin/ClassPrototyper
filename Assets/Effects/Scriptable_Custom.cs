using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Custom/CustomEffect")]
public class Scriptable_Custom : EffectBase
{
    public EffectBase startEffect;
    public EffectBase endEffect;

    public override IEffect InitialiseEffect(GameObject obj)
    {
        CustomEffect effect = new CustomEffect(this, obj);
        return effect;
    }
}