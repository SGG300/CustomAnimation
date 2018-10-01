using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(AnimationApi))]
public class AnimationApiEditor : Editor
{
    SerializedProperty samples;
    private void Awake()
    {
    }

    public override void OnInspectorGUI()
    {
        AnimationApi myTarget = (AnimationApi)target;
        myTarget.KeyFrameSamplesSize = Mathf.Max(0, EditorGUILayout.IntField("Samples", myTarget.KeyFrameSamplesSize));
        if (myTarget.KeyFrameSamplesSize > 0)
        {
            myTarget.selectedFrameKey = EditorGUILayout.IntSlider("SelectedFrameKey", myTarget.selectedFrameKey, 0, myTarget.KeyFrameSamplesSize - 1);

            myTarget.SetKeyFrameTimeFrame(myTarget.selectedFrameKey, EditorGUILayout.IntField("TimeFrame", myTarget.GetKeyFrame(myTarget.selectedFrameKey).timeFrame));
            myTarget.SetKeyFramePos(myTarget.selectedFrameKey, EditorGUILayout.Vector3Field("Pos", myTarget.GetKeyFrame(myTarget.selectedFrameKey).pos));

            if (GUILayout.Button("Sort"))
            {
                myTarget.Sort();
                Repaint();
            }

            if (GUILayout.Button("Remove"))
            {
                myTarget.Remove(myTarget.selectedFrameKey);
                Repaint();
            }
        }

        myTarget.animMode = (AnimMode)EditorGUILayout.EnumPopup("Animation Type", myTarget.animMode);

        EditorUtility.SetDirty(myTarget);
        if (!EditorApplication.isPlaying)
        {
            EditorSceneManager.MarkSceneDirty(myTarget.gameObject.scene);
        }


    }
}
