using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class HeadControlPublisher : UnityPublisher<MessageTypes.NaoHeadControl.HeadControl>
    {
        public string FrameId = "Unity";
        public int headJoints = 2; //HeadYaw and HeadPitch
        public float standardSpeed = 0.3f;
        OVRCameraRig headSet;
        public Quaternion rotation;
        protected bool isEnabled { get; set; }

        private MessageTypes.NaoHeadControl.HeadControl message;

        protected override void Start()
        {
            base.Start();
            headSet = GameObject.FindObjectOfType<OVRCameraRig>();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            if (isEnabled)
            {
                if (OVRInput.Get(OVRInput.RawButton.Y))
                {
                    UpdateMessage();
                }
            }
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.NaoHeadControl.HeadControl
            {
                header = new MessageTypes.Std.Header { frame_id = FrameId },
                joint_names = new string[headJoints],
                joint_angles = new float[headJoints],
                speed = standardSpeed
            };
        }

        private void UpdateMessage()
        {
            //get 2d head orientation
            rotation = headSet.centerEyeAnchor.rotation;
            //get euler angles from quaternion
            Vector3 eulerRotation = rotation.eulerAngles;
            if (eulerRotation.y < 270.0f && eulerRotation.y > 180.0f) { eulerRotation.y = 271.0f; }
            if (eulerRotation.y < 180.0f && eulerRotation.y > 90.0f) { eulerRotation.y = 89.0f; }
            if (eulerRotation.x < 270.0f && eulerRotation.x > 180.0f) { eulerRotation.x = 271.0f; }
            if (eulerRotation.x < 180.0f && eulerRotation.x > 900.0f) { eulerRotation.x = 89.0f; }
            message.header.Update();
            message.joint_names[0] = "HeadYaw";
            message.joint_names[1] = "HeadPitch";
            //print("ENTERED IN CICLE, THE ANGLES ARE X: " + XAngle + " EULER X: " + eulerRotation.x + " Y: " + YAngle + " EULER Y: " + eulerRotation.y + " EULER ROTATION UNITY: " + eulerRotation2 + " EULER ROTATION ROS: " + eulerRotation + "\n");
            if (eulerRotation.y <= 90.0f && eulerRotation.x <= 90.0f)
            {
                message.joint_angles[0] = -eulerRotation.y * Mathf.Deg2Rad;
                message.joint_angles[1] = eulerRotation.x * Mathf.Deg2Rad;
                //print("ENTERED IN CYCLE <90, HEADYAW IS: " + YAngle + " HEADPITCH: " + XAngle + "\n");
                Publish(message);
            }
            if (eulerRotation.y >= 270.0f && eulerRotation.x >= 270.0f)
            {
                message.joint_angles[0] = (360.0f - eulerRotation.y) * Mathf.Deg2Rad;
                message.joint_angles[1] = -(360.0f - eulerRotation.x) * Mathf.Deg2Rad;
                //print("ENTERED IN CYCLE >270, HEADYAW IS: " + YAngle + " HEADPITCH: " + XAngle + "\n");
                Publish(message);
            }
            if (eulerRotation.y <= 90.0f && eulerRotation.x >= 270.0f)
            {
                message.joint_angles[0] = -eulerRotation.y * Mathf.Deg2Rad;
                message.joint_angles[1] = -(360.0f - eulerRotation.x) * Mathf.Deg2Rad;
                //print("ENTERED IN CYCLE <90 e >270, HEADYAW IS: " + YAngle + " HEADPITCH: " + XAngle + "\n");
                Publish(message);
            }
            if (eulerRotation.y >= 270.0f && eulerRotation.x <= 90.0f)
            {
                message.joint_angles[0] = (360.0f - eulerRotation.y) * Mathf.Deg2Rad;
                message.joint_angles[1] = eulerRotation.x * Mathf.Deg2Rad;
                //print("ENTERED IN CYCLE >270 e <90, HEADYAW IS: " + YAngle + " HEADPITCH: " + XAngle + "\n");
                Publish(message);
            }
            /*else
            {
                print("ENTERED IN ELSE, THE ANGLES ARE X: " + eulerRotation.x + " Y: " + eulerRotation.y + "\n");
            }*/
        }

        public void EnableDisable()
        {
            isEnabled = !isEnabled;
        }
    }
}