using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityHeadControlActionClient : MonoBehaviour
    {
        OVRCameraRig headSet;
        private Quaternion rotation;
        float prevAngleX = 0.0f;
        float prevAngleY = 0.0f;
        float tolerance = 0.0f;
        float pi = 3.14159265359f;
        float maxRotationX = 2.0857f;
        float minRotationX = -2.0857f;
        float maxRotationY = -0.6720f;
        float minRotationY = 0.5149f;
        public float XAngle { get; set; }
        public float YAngle { get; set; }
        private RosConnector rosConnector;
        public JointAnglesWithSpeedActionClient jointAnglesWithSpeedActionClient;
        // Start is called before the first frame update

        public string actionName = "joint_angles_action";
        public string[] joint_names = { "HeadYaw", "HeadPitch" };
        public float[] joint_angles;
        public float speed = 0.3f;
        public byte relative = 0;
        public string status = "";
        public string feedback = "";
        public string result = "";

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
            eulerRotation.Unity2Ros();
            XAngle = eulerRotation.x;
            YAngle = eulerRotation.y;
            //get radians
            XAngle = (XAngle * pi) / 180;
            YAngle = (XAngle * pi) / 180;
            //float XAngle = (headSet.centerEyeAnchor.rotation.x)*(-2.0f);
            //float YAngle = headSet.centerEyeAnchor.rotation.y*(2.0f);
            //print(XAngle);
            //print(YAngle);
            //XAngle = 1.0f;
            //YAngle = 2.0f;
                float[] new_joint_angles = { YAngle, XAngle };
                //ExecuteAngle(joint_names, new_joint_angles);
                if (OVRInput.Get(OVRInput.RawButton.B))
                {
                    print("ENTERED IN CYCLE 1, THE ANGLES ARE X: " + XAngle + " PREV X: " + prevAngleX + " Y: " + YAngle + "PREV Y: " + prevAngleY + "\n");
                    //control tolerance to prevent jittering from involontary movement of the head
                    if (((XAngle - prevAngleX) > tolerance) || ((YAngle - prevAngleY) > tolerance))
                    {
                        print("ENTERED IN CYCLE 2, THE ANGLES ARE X: " + XAngle + " PREV X: " + prevAngleX + " Y: " + YAngle + "PREV Y: " + prevAngleY + "\n");
                        ExecuteAngle(joint_names, new_joint_angles);
                        prevAngleX = XAngle;
                        prevAngleY = YAngle;
                    }
                    else
                    {
                        print("ELSE" + "\n");
                    }

                }
            /*if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            //control tolerance to prevent jittering from involontary movement of the head
            if (((XAngle - prevAngleX) > tolerance) || ((YAngle - prevAngleY) > tolerance))
            {
                ExecuteAngle(joint_names, new_joint_angles);
            }
            else
            {

            }
        }*/
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