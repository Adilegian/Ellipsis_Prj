using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(AdvancedInteractionSystem))]

public class AIS_Inspector : Editor
{
    SerializedProperty StartPositonAndRotation,
    MainPosition,
    MainRotation,

    SecondaryPosition,
    SecondaryRotation,
    
    OpenAudio,
    CloseAudio,
    LockedAudio,
    
    OpenSpeed,
    CloseSpeed,
    
    OpenRotationSpeed,
    CloseRotationSpeed,
    
    locked,
    ConnectedScripts;

    void OnEnable()
    {
        StartPositonAndRotation = serializedObject.FindProperty("StartPositionAndRotation");
        MainPosition = serializedObject.FindProperty("MainPosition");
        MainRotation = serializedObject.FindProperty("MainRotation");

        SecondaryPosition = serializedObject.FindProperty("SecondaryPosition");
        SecondaryRotation = serializedObject.FindProperty("SecondaryRotation");

        OpenAudio = serializedObject.FindProperty("OpenAudio");
        CloseAudio = serializedObject.FindProperty("CloseAudio");
        LockedAudio = serializedObject.FindProperty("LockedAudio");

        OpenSpeed = serializedObject.FindProperty("OpenSpeed");
        CloseSpeed = serializedObject.FindProperty("CloseSpeed");

        OpenRotationSpeed = serializedObject.FindProperty("OpenRotationSpeed");
        CloseRotationSpeed = serializedObject.FindProperty("CloseRotationSpeed");
    
        locked = serializedObject.FindProperty("locked");
        ConnectedScripts = serializedObject.FindProperty("ConnectedScripts");
    }

    public override void OnInspectorGUI(){
        GUILayout.Box(Resources.Load("AIS_background") as Texture, GUILayout.Width(400), GUILayout.Height(215));
        AdvancedInteractionSystem script = (AdvancedInteractionSystem)target;

        EditorGUILayout.HelpBox("If you like this asset please don't forget to leave a nice review. It helps alot.", MessageType.Info);
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Main State", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(StartPositonAndRotation, new GUIContent("Start Position And Rotation", "Automatically get the main position and rotation of the object on start"));

        EditorGUI.BeginDisabledGroup(script.StartPositionAndRotation == true);
            EditorGUILayout.PropertyField(MainPosition, new GUIContent("Main Position", "The main transform position of the object. If the object isn't moving on game start then set it exactly the same as it's current transform position."));
            EditorGUILayout.PropertyField(MainRotation, new GUIContent("Main Rotation", "The main rotation of the object. If the object isn't rotating on game start then set it exactly the same as it's current transform rotation"));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Secondary State", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(SecondaryPosition, new GUIContent("Secondary Position", "The secondary position of the object when it's interacted with"));
        EditorGUILayout.PropertyField(SecondaryRotation, new GUIContent("Secondary Rotation", "The secondary rotation of the object when it's interacted with"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Audios", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(OpenAudio, new GUIContent("Open Audio", "The audio played when the object is moving to the secondary state"));
        EditorGUILayout.PropertyField(CloseAudio, new GUIContent("Close Audio", "The audio played when the object is moving back to the main state"));
        EditorGUILayout.PropertyField(LockedAudio, new GUIContent("Locked Audio", "The audio played when the object is locked but interacted with"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Speeds", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(OpenSpeed, new GUIContent("Open Speed", "Speed when moving from main to secondary state"));
        EditorGUILayout.PropertyField(CloseSpeed, new GUIContent("Close Speed", "Speed when moving from secondary to main state"));
        
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(OpenRotationSpeed, new GUIContent("Open Rotation Speed", "Speed of rotation when moving from main to secondary state"));
        EditorGUILayout.PropertyField(CloseRotationSpeed, new GUIContent("Close Rotation Speed", "Speed of rotation when moving from secondary to main state"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Miscellaneous", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(locked, new GUIContent("Locked", "Can the object be opened or is it locked?"));
        EditorGUILayout.PropertyField(ConnectedScripts, new GUIContent("Connected Scripts", "Attach the A.I.S. script of other parts to move different parts together. Like two push doors."), true);


        serializedObject.ApplyModifiedProperties();
    }
}
