
using UnityEngine;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Camera mainCam = Camera.allCameras[0];
        LevelController lvmap = target as LevelController;
        mainCam.fieldOfView = lvmap.CamSize;

    }
}
