using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationPathEditor : EditorWindow
{
    private AnimationClip[] animationClips;
    private string oldPath = "Onion";
    public string newPath = "Tomato";

    [MenuItem("Tools/Animation Path Editor")]
    public static void ShowWindow()
    {
        GetWindow<AnimationPathEditor>("Animation Path Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Animation Path Editor", EditorStyles.boldLabel);
        oldPath = EditorGUILayout.TextField("Old Path", oldPath);
        newPath = EditorGUILayout.TextField("New Path", newPath);

        if (GUILayout.Button("Load Selected Animation Clips"))
        {
            LoadSelectedAnimationClips();
        }

        if (animationClips != null && animationClips.Length > 0)
        {
            if (GUILayout.Button("Update Paths"))
            {
                UpdatePaths();
            }
        }
        EditorGUILayout.TextField("Change Into", newPath);

    }

    private void LoadSelectedAnimationClips()
    {
        var selectedObjects = Selection.objects;
        var clipsList = new List<AnimationClip>();

        foreach (var obj in selectedObjects)
        {
            if (obj is AnimationClip clip)
            {
                clipsList.Add(clip);
            }
        }

        animationClips = clipsList.ToArray();
        Debug.Log($"Loaded {animationClips.Length} animation clips.");
    }

    private void UpdatePaths()
    {
        foreach (var clip in animationClips)
        {
            var bindings = AnimationUtility.GetCurveBindings(clip);
            foreach (var binding in bindings)
            {
                if (binding.path.StartsWith(oldPath))
                {
                    var newBinding = binding;
                    newBinding.path = newPath + binding.path.Substring(oldPath.Length);

                    var curve = AnimationUtility.GetEditorCurve(clip, binding);
                    AnimationUtility.SetEditorCurve(clip, binding, null); // 기존 경로의 커브 삭제
                    AnimationUtility.SetEditorCurve(clip, newBinding, curve); // 새로운 경로에 커브 추가
                }
            }
        }

        Debug.Log("Paths updated successfully.");
    }
}