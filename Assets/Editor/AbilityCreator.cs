using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;

public class AbilityCreator : EditorWindow
{
    [SerializeField] List<EffectBase> ListTest = new List<EffectBase>();

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

    //Effect Stuff
    [SerializeField]
    List<EffectData> effectList = new List<EffectData>();

    AbilityBase asset;

    [MenuItem("Ability Creation/Ability Wizard")]
    static void Initialise()
    {
        AbilityCreator abilityWindow = (AbilityCreator)CreateInstance(typeof(AbilityCreator));
        abilityWindow.Show();
    }

    private void OnEnable()
    {
        asset = ScriptableObject.CreateInstance<AbilityBase>();
    }

    private void OnGUI()
    {

        GUILayout.Label("Ability Details:", EditorStyles.boldLabel);
        abilityName = EditorGUILayout.TextField("Name: ", abilityName);
        abilitySprite = TextureField("Ability Sprite", abilitySprite);
        EditorGUILayout.Space();


        GUILayout.Label("Targeting Type:", EditorStyles.boldLabel);
        targeting = (AbilityTargeting)EditorGUILayout.EnumPopup(targeting);
        switch (targeting)
        {
            case AbilityTargeting.ENEMY:
                range = EditorGUILayout.FloatField("Range: ", range);
                break;
            case AbilityTargeting.PLAYERORIGIN:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Radius:");
                radius = EditorGUILayout.Slider(radius, 1, 20);
                GUILayout.Label("Degrees:");
                degrees = EditorGUILayout.Slider(degrees, 0, 360);
                EditorGUILayout.EndHorizontal();
                break;
            case AbilityTargeting.ENEMYORIGIN:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Radius:");
                radius = EditorGUILayout.Slider(radius, 1, 20);
                GUILayout.Label("Degrees:");
                degrees = EditorGUILayout.Slider(degrees, 0, 360);
                EditorGUILayout.EndHorizontal();
                range = EditorGUILayout.FloatField("Range: ", range);
                break;
        }
        EditorGUILayout.Space();
        
        
        GUILayout.Label("Casting Type:", EditorStyles.boldLabel);
        castToggle = EditorGUILayout.Toggle("Induction: ", castToggle);
        if (castToggle)
        {
            castTime = EditorGUILayout.FloatField("Induction Cast Time: ", castTime);
        }
        channelToggle = EditorGUILayout.Toggle("Channel: ", channelToggle);
        if (channelToggle)
        {
            EditorGUILayout.BeginHorizontal();
            channelTime = EditorGUILayout.FloatField("Number of Pulses: ", channelTime);
            channelPulseRate = EditorGUILayout.FloatField("Pulse Rate: ", channelPulseRate);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();

        GUILayout.Label("Cooldown:", EditorStyles.boldLabel);
        onGCD = EditorGUILayout.Toggle("GCD: ", onGCD);
        cooldown = EditorGUILayout.FloatField("Cooldown Time: ", cooldown);

        EditorGUILayout.Space();

        EditorGUILayout.Space();

        ScriptableObject target = asset;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("effects");

        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties();


        if (GUILayout.Button("Create Ability"))
        {
            MakeAbility();
            Debug.Log("Make ability");
        }

        

    }

    private void MakeAbility()
    {
        asset.abilityName = abilityName;
        asset.abilitySprite = abilitySprite;

        asset.targeting = targeting;

        switch (targeting)
        {
            case AbilityTargeting.SELF:
                break;
            case AbilityTargeting.ENEMY:
                asset.range = range;
                break;
            case AbilityTargeting.PLAYERORIGIN:
                asset.radius = radius;
                asset.degrees = degrees;
                break;
            case AbilityTargeting.ENEMYORIGIN:
                asset.range = range;
                asset.radius = radius;
                asset.degrees = degrees;
                break;
            default:
                break;
        }

        if (castToggle)
        {
            asset.castToggle = true;
            asset.castTime = castTime;
        }

        if (channelToggle)
        {
            asset.channelToggle = channelToggle;
            asset.channelTime = channelTime;
            asset.channelPulseRate = channelPulseRate;
        }

        asset.onGCD = onGCD;
        asset.cooldown = cooldown;

        AssetDatabase.CreateAsset(asset, "Assets/Abilities/" + abilityName + ".asset");
        AssetDatabase.SaveAssets();
    }

    private static Sprite TextureField(string name, Sprite texture)
    {
        GUILayout.BeginVertical();
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperCenter;
        style.fixedWidth = 70;
        GUILayout.Label(name, style);
        var result = (Sprite)EditorGUILayout.ObjectField(texture, typeof(Sprite), false, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.EndVertical();
        return result;
    }
}

public class ListDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        var list = serializedObject.FindProperty("effectList");
        EditorGUILayout.PropertyField(list, new GUIContent("Effects List"), true);
    }
}