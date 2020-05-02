using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class WalkerPublisher : UnityPublisher<MessageTypes.Geometry.Twist>
    {
        private MessageTypes.Geometry.Twist message;
        protected bool isEnabled { get; set; }
        private int rate = 0;
        public OVRPlayerController playerController;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();

        }

        private void Update()
        {
            if (isEnabled)
            {
                rate++;
                //update every half frame
                if (rate >= 45)
                {
                    UpdateMessage();
                    rate = 0;
                }

            }
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.Twist();
            message.linear = new MessageTypes.Geometry.Vector3();
            message.angular = new MessageTypes.Geometry.Vector3();
        }
        private void UpdateMessage()
        {

            Vector2 linearVelocity = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
            Vector2 angularVelocity = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);

            message.linear = GetGeometryVector3Linear(linearVelocity); ;
            message.angular = GetGeometryVector3Angular(angularVelocity);

            Publish(message);
        }

        private static MessageTypes.Geometry.Vector3 GetGeometryVector3Linear(Vector2 vector2)
        {
            MessageTypes.Geometry.Vector3 geometryVector3Linear = new MessageTypes.Geometry.Vector3();
            geometryVector3Linear.x = vector2.y;
            geometryVector3Linear.y = -vector2.x;
            geometryVector3Linear.z = 0.0f;
            return geometryVector3Linear;
        }

        private static MessageTypes.Geometry.Vector3 GetGeometryVector3Angular(Vector2 vector2)
        {
            float maxAngularZ = 0.5f;
            MessageTypes.Geometry.Vector3 geometryVector3Angular = new MessageTypes.Geometry.Vector3();
            geometryVector3Angular.x = 0.0f;
            geometryVector3Angular.y = 0.0f;
            geometryVector3Angular.z = -(vector2.x * maxAngularZ);
            return geometryVector3Angular;
        }

        public void EnableDisable()
        {
            isEnabled = !isEnabled;
            playerController.EnableLinearMovement = !playerController.EnableLinearMovement;
            playerController.EnableRotation = !playerController.EnableRotation;
        }
    }
}