using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneTools
{
    [MenuItem("Tools/场景/打开烘焙")]
    public static void OpenBake()
    {
        Debug.Log("OpenBake");
    }

    [MenuItem("Tools/场景/关闭烘焙")]
    public static void CloseBake()
    {
        Debug.Log("CloseBake");
    }
}
