using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorHelpers
{

    [MenuItem("CONTEXT/Hello World %#HOME")]
    public static void HelloWorld()
    {
        Debug.Log("Hallo Earth");
    }

    [MenuItem("Help/Hello World")]
    public static void HelloWorld2()
    {
        Debug.Log("Hello Earthlings");
    }
}
