using RosSharp.RosBridgeClient.MessageTypes.NaoHandsControl;
using RosSharp.RosBridgeClient.MessageTypes.Rosapi;
using System;
using System.Collections.Generic;
using UnityEngine;
using rosapi = RosSharp.RosBridgeClient.MessageTypes.Rosapi;

namespace RosSharp.RosBridgeClient
{
    public class HandServiceConsumer : MonoBehaviour
    {
        private RosConnector rosConnector;
        public void Start()
        {
            rosConnector = GetComponent<RosConnector>();
        }
        public void Update()
        {
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) || OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            {
                if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                {
                    rosConnector.CallServiceCloseHand(true);
                }
                else
                {
                    rosConnector.CallServiceCloseHand(false);
                }

            }
            else
            {
                if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) || OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
                {
                    if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
                    {
                        rosConnector.CallServiceOpenHand(true);
                    }
                    else
                    {
                        rosConnector.CallServiceOpenHand(false);
                    }
                }
            }
        }
    }
}