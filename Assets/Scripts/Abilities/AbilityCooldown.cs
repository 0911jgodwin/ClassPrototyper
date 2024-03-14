using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    public KeyCode abilityButtonName;
    public Image darkMask;
    public Text cooldownTextDisplay;

    //[SerializeField] private Ability ability;
    [SerializeField] public AbilityBase ability;
    [SerializeField] GameObject combatObject;
    private Image myButtonImage;
    private float cooldownDuration;
    private float nextReadyTime;
    private float cooldownTimeLeft;
    private bool cooldownComplete;
    private AbilityManager abilityManager;
    private float currentCooldownDuration;


    void Start()
    {
        abilityManager = GetComponentInParent<AbilityManager>();
        Initialise(ability, combatObject);
    }

    public void Initialise(AbilityBase selectedAbility, GameObject combatObject)
    {
        ability = selectedAbility;
        myButtonImage = GetComponent<Image>();
        myButtonImage.sprite = ability.abilitySprite;
        darkMask.sprite = ability.abilitySprite;
        cooldownDuration = ability.cooldown;
        ability.Initialise(combatObject);
        AbilityReady();
    }

    public void SwapAbility(AbilityBase newAbility, bool retainCooldown)
    {
        ability = newAbility;
        myButtonImage = GetComponent<Image>();
        myButtonImage.sprite = ability.abilitySprite;
        darkMask.sprite = ability.abilitySprite;
        cooldownDuration = ability.cooldown;
        ability.Initialise(combatObject);
        if (retainCooldown)
        {
            RetainCooldown(cooldownTimeLeft);
        }
        else
        {
            ResetCooldown();
        }
    }

    void Update()
    {
        cooldownComplete = (Time.time > nextReadyTime);
        if (cooldownComplete)
        {
            AbilityReady();
            if (Input.GetKeyDown(abilityButtonName) && !abilityManager.IsBusy())
            {
                ButtonTriggered();
            }
        }
        else
        {
            Cooldown();
        }
    }

    public void TriggerGlobalCooldown()
    {
        if (cooldownComplete || cooldownTimeLeft < 1f)
        {
            currentCooldownDuration = 1f;
            nextReadyTime = 1f + Time.time;
            cooldownTimeLeft = 1f;
            darkMask.enabled = true;
            cooldownTextDisplay.enabled = false;
        }
    }

    private void AbilityReady()
    {
        cooldownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void Cooldown()
    {
        cooldownTimeLeft -= Time.deltaTime;
        float roundedCD = Mathf.Round(cooldownTimeLeft);
        cooldownTextDisplay.text = roundedCD.ToString();
        darkMask.fillAmount = (cooldownTimeLeft / currentCooldownDuration);
    }

    private void ButtonTriggered()
    {
        abilityManager.SetBusy();
        ability.TriggerAbility();
    }

    public string GetName()
    {
        return ability.abilityName;
    }

    public void TriggerCooldown()
    {
        currentCooldownDuration = cooldownDuration;
        nextReadyTime = cooldownDuration + Time.time;
        cooldownTimeLeft = cooldownDuration;
        darkMask.enabled = true;
        cooldownTextDisplay.enabled = true;
        cooldownComplete = false;
    }

    public void RetainCooldown(float _cooldown)
    {
        currentCooldownDuration = _cooldown;
        nextReadyTime = _cooldown + Time.time;
        cooldownTimeLeft = _cooldown;
        darkMask.enabled = true;
        cooldownTextDisplay.enabled = true;
        cooldownComplete = false;
    }

    public void ResetCooldown()
    {
        nextReadyTime = Time.time;
        darkMask.enabled = false;
        cooldownTextDisplay.enabled = false;
        cooldownComplete = false;
    }
}
