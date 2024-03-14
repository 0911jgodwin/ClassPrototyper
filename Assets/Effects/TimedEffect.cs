using UnityEngine;

public abstract class TimedEffect : IEffect
{
    public float duration;
    public int effectStacks;
    public Sprite sprite;
    public EffectBase effect { get; }
    protected readonly GameObject obj;
    public bool isFinished;

    public TimedEffect(EffectBase _effect, GameObject _obj)
    {
        effect = _effect;
        obj = _obj;
    }

    public virtual void Tick(float delta)
    {
        duration -= delta;
        if (duration <= 0)
        {
            End();
            isFinished = true;
        }
    }

    public virtual void Activate()
    {
        if (effect.stackable && effectStacks < effect.stackCap || duration <= 0)
        {
             ApplyEffect();
             effectStacks++;
        }
        if (effect.refreshable || duration <= 0)
        {
            duration = effect.duration;
        }
    }

    protected abstract void ApplyEffect();
    public abstract void End();
}

public interface IEffect
{

}