using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SuperGrid))]
public class SuperGridEditor : Editor
{
    private SuperGrid _superGrid;

    private Vector3 _center;

    private void OnEnable()
    {
        _superGrid = target as SuperGrid;
    }

    private void OnDisable()
    {
        
    }

    private void OnSceneGUI()
    {
        
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Init/Reset SuperGrid"))
        {
            if (EditorUtility.DisplayDialog("Safety Check!", "Do you want to reset the SuperGrid?", "Yes", "No"))
            {
                _superGrid.ClearGrid();

                _superGrid.Initialize();

                MarkSceneAsDirty();
            }
        }
    }

    private void MarkSceneAsDirty()
    {
        UnityEngine.SceneManagement.Scene activeScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();

        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(activeScene);
    }
}
