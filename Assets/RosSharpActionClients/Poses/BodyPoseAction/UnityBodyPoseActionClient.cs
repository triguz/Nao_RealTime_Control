using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityBodyPoseActionClient : MonoBehaviour
    {
        private RosConnector rosConnector;
        public BodyPoseActionClient bodyPoseActionClient;
        // Start is called before the first frame update

        public string actionName = "body_pose";
        public string pose_name = "crouch";
        public string status = "";
        public string feedback = "";
        public string result = "";

        void Start()
        {
            rosConnector = GetComponent<RosConnector>();
            bodyPoseActionClient = new BodyPoseActionClient(actionName,rosConnector.RosSocket);
            bodyPoseActionClient.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            status = bodyPoseActionClient.GetStatusString();
            feedback = bodyPoseActionClient.GetFeedbackString();
            result = bodyPoseActionClient.GetResultString();
        }

        public void RegisterGoal()
        {
            bodyPoseActionClient.pose_name = pose_name;
        }

        public void ExecutePose(string new_pose_name)
        {
            bodyPoseActionClient.pose_name = new_pose_name;
            bodyPoseActionClient.SendGoal();
        }

    }
}
