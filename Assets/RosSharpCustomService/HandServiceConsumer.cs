using RosSharp.RosBridgeClient.MessageTypes.NaoHandsControl;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class HandServiceConsumer : MonoBehaviour
    {
        protected bool isEnabled { get; set; }
        private RosConnector rosConnector;
        public void Start()
        {
            rosConnector = GetComponent<RosConnector>();
        }
        public void Update()
        {
            if (isEnabled)
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

        public void EnableDisable()
        {
            isEnabled = !isEnabled;
        }
    }
}