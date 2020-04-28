using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityReactiveBodyPoseActionClient : MonoBehaviour
    {
        private RosConnector rosConnector;
        public BodyPoseActionClient bodyPoseActionClient;

        OVRCameraRig headSet;
        private Vector3 position;
        public float minSquat = 1.2f;
        public bool isActive = false;
        public bool playerIsSquatted = false;

        public string actionName = "body_pose";
        public string pose_name = "crouch";
        public string status = "";
        public string feedback = "";
        public string result = "";

        // Start is called before the first frame update
        void Start()
        {
            headSet = GameObject.FindObjectOfType<OVRCameraRig>();
            rosConnector = GetComponent<RosConnector>();
            bodyPoseActionClient = new BodyPoseActionClient(actionName,rosConnector.RosSocket);
            bodyPoseActionClient.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            position = headSet.centerEyeAnchor.position;
            if (position.y < minSquat && isActive && playerIsSquatted != true )
            {
                ExecutePose("crouch");
                playerIsSquatted = true;
            }
            else
            {
                if (position.y > minSquat && isActive && playerIsSquatted == true)
                {
                    ExecutePose("init");
                    playerIsSquatted = false;
                }
            }
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

        public void EnableDisable()
        {
             isActive = !isActive;
        }

    }
}
