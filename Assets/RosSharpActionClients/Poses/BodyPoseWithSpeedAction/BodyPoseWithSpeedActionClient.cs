using System;
using RosSharp.RosBridgeClient.MessageTypes.NaoqiBridge;

namespace RosSharp.RosBridgeClient.Actionlib
{
    public class BodyPoseWithSpeedActionClient : ActionClient<BodyPoseWithSpeedAction, BodyPoseWithSpeedActionGoal, BodyPoseWithSpeedActionResult, BodyPoseWithSpeedActionFeedback, BodyPoseWithSpeedGoal, BodyPoseWithSpeedResult, BodyPoseWithSpeedFeedback>
    {
        public string posture_name;
        public float speed;
        public string status = "";
        public string feedback = "";
        public string result = "";

        public BodyPoseWithSpeedActionClient(string actionName, RosSocket rosSocket)
        {
            this.actionName = actionName;
            this.rosSocket = rosSocket;
            action = new BodyPoseWithSpeedAction();
            goalStatus = new MessageTypes.Actionlib.GoalStatus();
        }

        protected override BodyPoseWithSpeedActionGoal GetActionGoal()
        {
            action.action_goal.goal.posture_name = posture_name;
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