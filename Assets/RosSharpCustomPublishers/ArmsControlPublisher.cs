using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class ArmsControlPublisher : UnityPublisher<MessageTypes.NaoArmsControl.ArmsControl>
    {
        public string FrameId = "Unity";
        private int positionLenght = 6; //lenght of the position array 6D
        private MessageTypes.NaoArmsControl.ArmsControl message;
        protected bool isEnabled { get; set; }
        int rate = 0;
        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void Update()
        {
            if (isEnabled)
            {
                if (OVRInput.Get(OVRInput.RawButton.B))
                {
                    rate++;
                    //update every frame and a half to not spam subscriber and ik solver
                    if (rate >= 135)
                    {
                        UpdateMessage();
                        rate = 0;
                    }

                }
            }
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.NaoArmsControl.ArmsControl
            {
                header = new MessageTypes.Std.Header { frame_id = FrameId },
                position_right_arm = new float[positionLenght],
                position_left_arm = new float[positionLenght],
            };
        }

        private void UpdateMessage()
        {
            //get controller position and orientation
            Vector3 posR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).Unity2Ros();
            Vector3 posL = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).Unity2Ros();
            Quaternion rotR = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch).Unity2Ros();
            Quaternion rotL = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch).Unity2Ros();

            //coefficents to calibrate arms movement
            float coefX = 0.35f;
            float coefY = 0.23f;
            float coefZ = 0.37f;

            //set position
            message.position_right_arm[0] = FitSafePosition(posR.x * coefX, 'x');
            float saveSign = FitSafePosition(-posR.y * coefY, 'y');
            message.position_right_arm[1] = -saveSign;
            message.position_right_arm[2] = FitSafePosition(posR.z* coefZ, 'z');
            message.position_left_arm[0] = FitSafePosition(posL.x * coefX, 'x');
            //left arm is always pointing too much to the right...
            message.position_left_arm[1] = FitSafePosition(((posL.y * coefY)) + 0.01f, 'y');
            message.position_left_arm[2] = FitSafePosition(posL.z* coefZ, 'z');
            //get radians from quaternion
            Vector3 eulerRotR = FixAngleGetRad(rotR.eulerAngles, 'r');
            Vector3 eulerRotL = FixAngleGetRad(rotL.eulerAngles, 'l');

            message.position_right_arm[3] = eulerRotR.x;
            message.position_right_arm[4] = eulerRotR.y;
            message.position_right_arm[5] = eulerRotR.z;
            message.position_left_arm[3] = eulerRotL.x;
            message.position_left_arm[4] = eulerRotL.y;
            message.position_left_arm[5] = eulerRotL.z;
            Publish(message);


        }

        private Vector3 FixAngleGetRad(Vector3 angles, char hand)
        {
            float maxAngleX = 2.90f;
            float minAngleX = 0.001f;
            //calibration coef
            float coef = 2.00f;
            int j = 0;
            Vector3 newAngles = new Vector3();
            for (int i = 0; i <= 2; i++)
            {
                if (j == 0)
                {
                    float angle = angles.x;
                    //switch rotation verse
                    if (angle < 270.0f && angle > 180.0f) { angle = 269.0f; }
                    if (angle < 180.0f && angle > 90.0f) { angle = 89.0f; }
                    if (angle <= 90.0f)
                    {
                        if (angle == 89.0f)
                        {
                            newAngles.x = minAngleX;
                        }
                        else
                        {
                            newAngles.x = (angle * Mathf.Deg2Rad) * coef;
                            if (hand == 'l') { newAngles.x = -newAngles.x; }
                            j++;

                        }
                    }
                    else
                    {
                        newAngles.x = ((360.0f - angle) * Mathf.Deg2Rad) * coef;
                        if (newAngles.x > maxAngleX)
                        {
                            print("CHANGED ANGLE X > MAX: " + newAngles.x + "\n");
                            newAngles.x = maxAngleX;
                        }
                        if (hand == 'l') { newAngles.x = -newAngles.x; }
                        j++;
                    }
                }
                else
                {
                    if (j == 1)
                    {
                        float angle = angles.y;
                        if (angle < 270.0f && angle > 180.0f) { angle = 271.0f; }
                        if (angle < 180.0f && angle > 90.0f) { angle = 89.0f; }
                        if (angle < 270.0f && angle > 180.0f) { angle = 271.0f; }
                        if (angle < 180.0f && angle > 90.0f) { angle = 89.0f; }
                        if (angle >= 270.0f)
                        {
                            newAngles.y = -(360.0f - angle) * Mathf.Deg2Rad;
                            j++;
                        }
                        else
                        {
                            newAngles.y = angle * Mathf.Deg2Rad;
                            j++;
                        }
                    }
                    else
                    {
                        float angle = angles.z;
                        if (angle < 270.0f && angle > 180.0f) { angle = 271.0f; }
                        if (angle < 180.0f && angle > 90.0f) { angle = 89.0f; }
                        if (angle < 270.0f && angle > 180.0f) { angle = 271.0f; }
                        if (angle < 180.0f && angle > 90.0f) { angle = 89.0f; }
                        if (angle >= 270.0f)
                        {
                            newAngles.z = -(360.0f - angle) * Mathf.Deg2Rad;
                            j++;
                        }
                        else
                        {
                            newAngles.z = angle * Mathf.Deg2Rad;
                            j++;
                        }
                    }
                }
            }

            return newAngles;
        }

        private float FitSafePosition(float val, char axis)
        {
            float maxX = 0.22f;
            float minX = 0.01f;
            float maxY = 0.85f;
            float minY = 0.03f;
            float maxZ = 0.65f;
            float minZ = 0.222f;
            if (axis == 'x')
            {
                //check if in range
                if (val <= maxX && val > minX)
                {
                    return val;
                }
                else
                {//if not in range, check wheter apply max or min values
                    if (val >= maxX)
                    {
                        return maxX;
                    }
                    else
                    {
                        return minX;
                    }
                }

            }
            else
            {
                if (axis == 'y')
                {
                    //check if in range
                    if (val <= maxY && val > minY)
                    {
                        return val;
                    }
                    else
                    {//if not in range, check wheter apply max or min values
                        if (val >= maxY)
                        {
                            return maxY;
                        }
                        else
                        {
                            return minY;
                        }
                    }
                }
                else
                {
                    //check if in range
                    if (val <= maxZ && val > minZ)
                    {
                        return val;
                    }
                    else
                    {//if not in range, check wheter apply max or min values
                        if (val >= maxZ)
                        {
                            return maxZ;
                        }
                        else
                        {
                            return minZ;
                        }
                    }
                }
            }
        }

        public void EnableDisable()
        {
            isEnabled = !isEnabled;
        }
    }
}