using UnityEditor;
using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [CustomEditor(typeof(UnityJointAnglesWithSpeedActionClient))]
    public class JointAnglesWithSpeedActionClientEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Send Goal"))
            {
                ((UnityJointAnglesWithSpeedActionClient)target).RegisterGoal();
                ((UnityJointAnglesWithSpeedActionClient)target).jointAnglesWithSpeedActionClient.SendGoal();
            }

            if (GUILayout.Button("Cancel Goal"))
            {
                ((UnityJointAnglesWithSpeedActionClient)target).jointAnglesWithSpeedActionClient.CancelGoal();
            }
        }
    }
}