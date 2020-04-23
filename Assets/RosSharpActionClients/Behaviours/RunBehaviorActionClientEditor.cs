using UnityEditor;
using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [CustomEditor(typeof(UnityRunBehaviorActionClient))]
    public class RunBehaviorActionClientEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Send Goal"))
            {
                ((UnityRunBehaviorActionClient)target).RegisterGoal();
                ((UnityRunBehaviorActionClient)target).runBehaviorActionClient.SendGoal();
            }

            if (GUILayout.Button("Cancel Goal"))
            {
                ((UnityRunBehaviorActionClient)target).runBehaviorActionClient.CancelGoal();
            }
        }
    }
}