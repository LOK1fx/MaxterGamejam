using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad()]
public class EditorToolbar
{
    static EditorToolbar()
    {
        ToolbarExtender.LeftToolbarGUI.Add(DrawScenesSelector);
    }

    static void DrawScenesSelector()
    {
        var path = "Assets/Recode/Scenes";

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("0"))
        {
            EditorSceneManager.OpenScene($"{path}/Intro.unity");
        }
        if (GUILayout.Button("1"))
        {
            EditorSceneManager.OpenScene($"{path}/Menu.unity");
        }
        if (GUILayout.Button("2"))
        {
            EditorSceneManager.OpenScene($"{path}/TestLevel.unity");
        }
        if (GUILayout.Button("3"))
        {
            EditorSceneManager.OpenScene($"{path}/Intro.unity");
        }
    }
}
