using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityHeadControlActionClient : MonoBehaviour
    {
        OVRCameraRig headSet;
        private Quaternion rotation;
        private float XAngle;
        private float YAngle;
        private RosConnector rosConnector;
        public JointAnglesWithSpeedActionClient jointAnglesWithSpeedActionClient;

        public string actionName = "joint_angles_action";
        public string[] joint_names = { "HeadYaw", "HeadPitch" };
        public float[] joint_angles;
        public float speed = 0.3f;
        public byte relative = 0;
        public string status = "";
        public string feedback = "";
        public string result = "";

        // Start is called before the first frame update
        void Start()
        {
            headSet = GameObject.FindObjectOfType<OVRCameraRig>();
            rosConnector = GetComponent<RosConnector>();
            jointAnglesWithSpeedActionClient = new JointAnglesWithSpeedActionClient(actionName, rosConnector.RosSocket);
            jointAnglesWithSpeedActionClient.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            //get 2d head orientation
            rotation = headSet.centerEyeAnchor.rotation;
            //get euler angles from quaternion
            Vector3 eulerRotation = rotation.eulerAngles;
            if (eulerRotation.y < 270.0f && eulerRotation.y > 180.0f) { eulerRotation.y = 271.0f; }
            if (eulerRotation.y < 180.0f && eulerRotation.y > 90.0f) { eulerRotation.y = 89.0f; }
            if (eulerRotation.x < 270.0f && eulerRotation.x > 180.0f) { eulerRotation.x = 271.0f; }
            if (eulerRotation.x < 180.0f && eulerRotation.x > 900.0f) { eulerRotation.x = 89.0f; }
            if (OVRInput.Get(OVRInput.RawButton.B))
            {
                if (eulerRotation.y <= 90.0f && eulerRotation.x <= 90.0f)
                {
                    XAngle = eulerRotation.x * Mathf.Deg2Rad;
                    YAngle = -eulerRotation.y * Mathf.Deg2Rad;
                    float[] new_joint_angles = { YAngle, XAngle };
                    ExecuteAngle(joint_names, new_joint_angles);
                }
                if (eulerRotation.y >= 270.0f && eulerRotation.x >= 270.0f)
                {
                    XAngle = -(360.0f - eulerRotation.x) * Mathf.Deg2Rad;
                    YAngle = (360.0f - eulerRotation.y) * Mathf.Deg2Rad;
                    float[] new_joint_angles = { YAngle, XAngle };
                    ExecuteAngle(joint_names, new_joint_angles);
                }
                if (eulerRotation.y <= 90.0f && eulerRotation.x >= 270.0f)
                {
                    XAngle = -(360.0f - eulerRotation.x) * Mathf.Deg2Rad;
                    YAngle = -eulerRotation.y * Mathf.Deg2Rad;
                    float[] new_joint_angles = { YAngle, XAngle };
                    ExecuteAngle(joint_names, new_joint_angles);
                }
                if (eulerRotation.y >= 270.0f && eulerRotation.x <= 90.0f)
                {
                    XAngle = eulerRotation.x * Mathf.Deg2Rad;
                    YAngle = (360.0f - eulerRotation.y) * Mathf.Deg2Rad;
                    float[] new_joint_angles = { YAngle, XAngle };
                    ExecuteAngle(joint_names, new_joint_angles);
                }
                else
                {
                    print("ENTERED IN ELSE, THE ANGLES ARE X: " + XAngle + " EULER X: " + eulerRotation.x + " Y: " + YAngle + " EULER Y: " + eulerRotation.y + "\n");
                }
            }
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