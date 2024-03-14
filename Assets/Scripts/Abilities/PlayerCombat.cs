using System.Collections.Generic;
using UnityEngine;

public enum CastStatus {
    SUCCESS,
    FAILURE,
    TICK
}
public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private CastBar castBar;

    [SerializeField]
    private Sprite image;

    [SerializeField]
    private StatsManager statsManager;

    [SerializeField]
    private EffectManager playerEffectManager;

    [SerializeField] 
    private StatType inductionMod;

    public AbilityBase currentSkill;

    public Combat combat;

    private List<GameObject> enemyTargets;
    public string abilityName;
    public bool onGCD;

    public List<EffectData> effects;

    public delegate void TriggerCooldown(string abilityName, bool onGCD);
    public static event TriggerCooldown TriggerCooldowns;

    public AbilityManager abilityManager;

    private void Start()
    {
        statsManager = this.GetComponent<StatsManager>();
        enemyTargets = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            CastBar.OnTick += ApplyEffects;
            castBar.CastSpell("Blabla", image, 3.0f * statsManager.StatsSystem.GetStatValue(inductionMod));
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            CastBar.OnTick += ApplyEffects;
            castBar.ChannelSpell("Blah blah", image, 3.0f, 1.0f);
        }
    }

    void ApplyEffects(CastStatus castStatus)
    {
        if (castStatus == CastStatus.TICK)
        {
            foreach(EffectData effect in effects)
            {
                if (Random.Range(0, 100) <= effect.applicationChance)
                {
                    if (effect.target == EffectTarget.SELF)
                    {
                        playerEffectManager.AddEffect(effect.effect.InitialiseEffect(this.gameObject));
                    }
                    else if (effect.target == EffectTarget.ENEMY)
                    {
                        if (enemyTargets.Count != 0)
                        {
                            foreach (GameObject enemy in enemyTargets)
                            {
                                Debug.Log(enemy.name);
                                enemy.GetComponent<EffectManager>().AddEffect(effect.effect.InitialiseEffect(enemy));
                            }
                        }
                    }
                }
            }
        }
        else if (castStatus == CastStatus.SUCCESS)
        {
            // Trigger Cooldown
            if (TriggerCooldowns != null)
            {
                TriggerCooldowns(abilityName, onGCD);
            }
        }
        else if (castStatus == CastStatus.FAILURE)
        {
            // No cooldown
            abilityManager.CancelBusy();
        }
    }

    public void UseAbility(string name, Sprite icon, float duration)
    {
        CastBar.OnTick += ApplyEffects;
        castBar.CastSpell(name, icon, duration * statsManager.StatsSystem.GetStatValue(inductionMod));
    }

    public void UseAbility(string name, Sprite icon, float duration, float pulseRate)
    {
        CastBar.OnTick += ApplyEffects;
        castBar.ChannelSpell(name, icon, duration, pulseRate);
    }

    public void UseAbility()
    {
        ApplyEffects(CastStatus.TICK);
        ApplyEffects(CastStatus.SUCCESS);
    }

    public void SetEnemyTargets()
    {
        enemyTargets.Clear();
        enemyTargets.Add(combat.GetTarget().gameObject);
    }

    public void SetEnemyTargets(Transform origin, float radius, float degrees)
    {
        enemyTargets.Clear();
        float maxAngle = Mathf.Cos(degrees * Mathf.Deg2Rad / 2.0f);

        Collider[] colliders = Physics.OverlapSphere(origin.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Enemy"))
            {
                Vector3 enemyLocation = colliders[i].transform.position;
                Vector3 vectorToEnemy = (enemyLocation - transform.position);

                if (Vector3.Dot(vectorToEnemy.normalized, this.transform.forward) > maxAngle)
                {
                    enemyTargets.Add(colliders[i].transform.gameObject);
                }

            }
        }
    }

    public Transform GetTarget()
    {
        return combat.GetTarget().transform;
    }
}
