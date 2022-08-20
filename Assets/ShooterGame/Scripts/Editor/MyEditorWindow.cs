using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyEditorWindow : EditorWindow
{

    bool groupEnabled;
    bool myBool;
    int myInt;
    float yourFloat;


    [MenuItem("Bootcamp/My Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyEditorWindow));
    }

    private void OnGUI()
    {
        GUILayout.Label("Settings Window", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("This is a help box over here!", MessageType.Info);


        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle Me", myBool);
        yourFloat = EditorGUILayout.Slider("Slide me", yourFloat, -1f, 1f);
        myInt = EditorGUILayout.IntField("A number please: ", myInt);

        EditorGUILayout.EndToggleGroup();
    }

}
