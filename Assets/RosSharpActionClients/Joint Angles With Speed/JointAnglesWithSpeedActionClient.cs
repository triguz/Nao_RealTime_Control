using System;
using RosSharp.RosBridgeClient.MessageTypes.NaoqiBridge;
using UnityEngine;


namespace RosSharp.RosBridgeClient.Actionlib
{
    public class JointAnglesWithSpeedActionClient : ActionClient<JointAnglesWithSpeedAction, JointAnglesWithSpeedActionGoal, JointAnglesWithSpeedActionResult, JointAnglesWithSpeedActionFeedback, JointAnglesWithSpeedGoal, JointAnglesWithSpeedResult, JointAnglesWithSpeedFeedback>
    {
        public string[] joint_names;
        public float[] joint_angles;
        public float speed;
        public byte relative;
        public string status = "";
        public string feedback = "";
        public string result = "";

        public JointAnglesWithSpeedActionClient(string actionName, RosSocket rosSocket)
        {
            this.actionName = actionName;
            this.rosSocket = rosSocket;
            action = new JointAnglesWithSpeedAction();
            goalStatus = new MessageTypes.Actionlib.GoalStatus();
        }

        protected override JointAnglesWithSpeedActionGoal GetActionGoal()
        {
            action.action_goal.goal.joint_angles.joint_names = joint_names;
            action.action_goal.goal.joint_angles.joint_angles = joint_angles;
            action.action_goal.goal.joint_angles.speed = speed;
            action.action_goal.goal.joint_angles.relative = relative;
            return action.action_goal;
        }

        protected override void OnStatusUpdated()
        {
            // Not implemented for this particular application
        }

        protected override void OnFeedbackReceived()
        {
            // Not implemented for this particular application since get string directly returns stored feedback
        }

        protected override void OnResultReceived()
        {
            // Not implemented for this particular application since get string directly returns stored result action.action_feedback.feedback.sequence
        }

        public string GetStatusString()
        {
            if (goalStatus != null)
            {
                return ((ActionStatus)(goalStatus.status)).ToString();
            }
            return "";
        }

        public string GetFeedbackString()
        {
            if (action != null)
                return String.Join(",", action.action_feedback.feedback);
            return "";
        }

        public string GetResultString()
        {
            if (action != null)
                return String.Join(",", action.action_result.result);
            return "";
        }
    }
}