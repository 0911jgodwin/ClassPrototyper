using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Stat Type")]
public class StatType : ScriptableObject
{
    [SerializeField] private new string name = "New Stat Type Name";
    [SerializeField] private float defaultValue = 0f;

    public string Name => name;
    public float DefaultValue => defaultValue;
}
