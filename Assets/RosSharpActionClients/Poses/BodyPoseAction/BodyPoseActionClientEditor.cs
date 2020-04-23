using UnityEditor;
using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
	[CustomEditor(typeof(UnityBodyPoseActionClient))]
public class BodyPoseActionClientEditor : Editor
{
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Send Goal"))
            {
                ((UnityBodyPoseActionClient)target).RegisterGoal();
                ((UnityBodyPoseActionClient)target).bodyPoseActionClient.SendGoal();
            }

            if (GUILayout.Button("Cancel Goal"))
            {
                ((UnityBodyPoseActionClient)target).bodyPoseActionClient.CancelGoal();
            }
        }
    }
}