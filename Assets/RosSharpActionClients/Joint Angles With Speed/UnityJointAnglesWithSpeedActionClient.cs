using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityJointAnglesWithSpeedActionClient : MonoBehaviour
    {
        private RosConnector rosConnector;
        public JointAnglesWithSpeedActionClient jointAnglesWithSpeedActionClient;


        public string actionName = "joint_angles_action";
        public string[] joint_names = {"HeadYaw", "HeadPitch"};
        public float[] joint_angles = {1.0f, 0.0f};
        public float speed = 0.1f;
        public byte relative = 0;
        public string status = "";
        public string feedback = "";
        public string result = "";

        // Start is called before the first frame update
        void Start()
        {
            rosConnector = GetComponent<RosConnector>();
            jointAnglesWithSpeedActionClient = new JointAnglesWithSpeedActionClient(actionName, rosConnector.RosSocket);
            jointAnglesWithSpeedActionClient.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            status = jointAnglesWithSpeedActionClient.GetStatusString();
            feedback = jointAnglesWithSpeedActionClient.GetFeedbackString();
            result = jointAnglesWithSpeedActionClient.GetResultString();

        }

        public void RegisterGoal()
        {
            jointAnglesWithSpeedActionClient.joint_names = joint_names;
            jointAnglesWithSpeedActionClient.joint_angles = joint_angles;
            jointAnglesWithSpeedActionClient.speed = speed;
            jointAnglesWithSpeedActionClient.relative = relative;

        }

        public void ExecuteAngle(string[] new_joint_names, float[] new_joint_angles)
        {
            jointAnglesWithSpeedActionClient.speed = speed;
            jointAnglesWithSpeedActionClient.relative = relative;
            jointAnglesWithSpeedActionClient.joint_names = new_joint_names;
            jointAnglesWithSpeedActionClient.joint_angles = new_joint_angles;
            jointAnglesWithSpeedActionClient.SendGoal();
        }

    }
}