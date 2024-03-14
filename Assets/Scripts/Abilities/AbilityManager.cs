using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public AbilityCooldown[] abilities;
    private bool busy = false;
    void Start()
    {
        abilities = GetComponentsInChildren<AbilityCooldown>();
        PlayerCombat.TriggerCooldowns += TriggerCooldown;
    }

    public void ActivateGlobalCooldown()
    {
        foreach(AbilityCooldown ability in abilities)
        {
            ability.TriggerGlobalCooldown();
        }
    }

    void TriggerCooldown(string name, bool onGCD)
    {
        busy = false;
        foreach (AbilityCooldown ability in abilities)
        {
            if (ability.GetName() == name)
            {
                ability.TriggerCooldown();
            }
        }
        if (onGCD)
        {
            ActivateGlobalCooldown();
        }
    }

    public void SwapAbility(AbilityBase oldAbility, AbilityBase newAbility, bool retainCooldown)
    {
        foreach (AbilityCooldown ability in abilities)
        {
            if (ability.GetName() == oldAbility.abilityName)
            {
                ability.SwapAbility(newAbility, retainCooldown);
            }
        }
    }

    public void SetBusy()
    {
        busy = true;
    }

    public bool IsBusy()
    {
        return busy;
    }

    public void CancelBusy()
    {
        busy = false;
    }

}
