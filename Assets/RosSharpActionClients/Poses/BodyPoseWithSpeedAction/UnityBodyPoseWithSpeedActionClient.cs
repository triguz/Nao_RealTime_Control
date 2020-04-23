using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityBodyPoseWithSpeedActionClient : MonoBehaviour
    {
        private RosConnector rosConnector;
        public BodyPoseWithSpeedActionClient bodyPoseWithSpeedActionClient;
        // Start is called before the first frame update

        public string actionName = "body_pose_naoqi";
        public string posture_name = "Sit";
        public float speed = 1.0f;
        public string status = "";
        public string feedback = "";
        public string result = "";

        void Start()
        {
            rosConnector = GetComponent<RosConnector>();
            bodyPoseWithSpeedActionClient = new BodyPoseWithSpeedActionClient(actionName, rosConnector.RosSocket);
            bodyPoseWithSpeedActionClient.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            status = bodyPoseWithSpeedActionClient.GetStatusString();
            feedback = bodyPoseWithSpeedActionClient.GetFeedbackString();
            result = bodyPoseWithSpeedActionClient.GetResultString();
        }

        /*public void RegisterGoal()
        {
            bodyPoseWithSpeedActionClient.posture_name = posture_name;
            bodyPoseWithSpeedActionClient.speed = speed;
        }

        public void ExecutePosture(string new_posture_name, float new_speed_value)
        {
            bodyPoseWithSpeedActionClient.posture_name = new_posture_name;
            bodyPoseWithSpeedActionClient.speed = new_speed_value;
            bodyPoseWithSpeedActionClient.SendGoal();
        }*/

        public void RegisterGoal()
        {
            bodyPoseWithSpeedActionClient.posture_name = posture_name;
        }

        public void ExecutePosture(string new_posture_name)
        {
            bodyPoseWithSpeedActionClient.posture_name = new_posture_name;
            bodyPoseWithSpeedActionClient.SendGoal();
        }

    }
}
