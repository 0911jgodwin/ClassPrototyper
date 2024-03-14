using System.Collections.Generic;
using UnityEngine;
public enum AbilityTargeting
{
    SELF,
    ENEMY,
    PLAYERORIGIN,
    ENEMYORIGIN
}

public enum EffectTarget
{
    SELF,
    ENEMY
}

public class AbilityBase : ScriptableObject
{
    //Executing stuff
    private PlayerCombat playerCombat;
    private Transform player;

    //Base stuff
    public string abilityName;
    public Sprite abilitySprite;

    //Targeting stuff
    public AbilityTargeting targeting;
    public float radius;
    public float degrees;
    public float range;

    //Cast stuff
    public bool castToggle;
    public float castTime;
    public bool channelToggle;
    public float channelTime;
    public float channelPulseRate;

    //CD stuff
    public bool onGCD;
    public float cooldown;

    //Effects
    public List<EffectData> effects;

    public void Initialise(GameObject obj)
    {
        playerCombat = obj.GetComponent<PlayerCombat>();
        player = obj.GetComponent<Transform>();
    }

    public void TriggerAbility()
    {
        playerCombat.abilityName = abilityName;
        playerCombat.onGCD = onGCD;
        switch (targeting)
        {
            case AbilityTargeting.SELF:
                break;
            case AbilityTargeting.ENEMY:
                if (!CheckInRange())
                {
                    return;
                }
                playerCombat.SetEnemyTargets();
                break;
            case AbilityTargeting.PLAYERORIGIN:
                playerCombat.SetEnemyTargets(player.transform, radius, degrees);
                break;
            case AbilityTargeting.ENEMYORIGIN:
                if (!CheckInRange())
                {
                    return;
                }
                playerCombat.SetEnemyTargets(playerCombat.GetTarget(), radius, degrees);
                break;
            default:
                break;
        }
        playerCombat.effects = effects;
        if (castToggle)
        {
            playerCombat.UseAbility(abilityName, abilitySprite, castTime);
        }
        else if (channelToggle) {
            playerCombat.UseAbility(abilityName, abilitySprite, channelTime, channelPulseRate);
        }
        else
        {
            playerCombat.UseAbility();
        }
    }

    public bool CheckInRange()
    {
        return Vector3.Distance(player.position, playerCombat.GetTarget().transform.position) < range;
    }
}


