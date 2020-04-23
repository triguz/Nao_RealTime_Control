using UnityEditor;
using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [CustomEditor(typeof(UnityBodyPoseWithSpeedActionClient))]
    public class BodyPoseWithSpeedActionClientEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Send Goal"))
            {
                ((UnityBodyPoseWithSpeedActionClient)target).RegisterGoal();
                ((UnityBodyPoseWithSpeedActionClient)target).bodyPoseWithSpeedActionClient.SendGoal();
            }

            if (GUILayout.Button("Cancel Goal"))
            {
                ((UnityBodyPoseWithSpeedActionClient)target).bodyPoseWithSpeedActionClient.CancelGoal();
            }
        }
    }
}