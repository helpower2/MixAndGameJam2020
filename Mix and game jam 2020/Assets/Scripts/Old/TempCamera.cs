using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Camera))]
public class TempCamera : MonoBehaviour
{
    private Camera _camera;

    private void Reset()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.allCameras.Except(new Camera[1]{this._camera}).Any())
        {
         Destroy(this.gameObject);   
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(TempCamera))]
public class TempCameraDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Will remove the GameObject once other camera found!", MessageType.Warning, true);
        base.OnInspectorGUI();
    }
}
#endif

