using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsPrint : MonoBehaviour
{
   //public Transform headSet;
    //public Transform touchL;
    //public Transform touchR;
    public UnityEngine.UI.Text distLabel; // <-- Assign the Text element in the Inspector view.
    private OVRCameraRig rig;
    private void Start()
    {
        rig = GameObject.FindObjectOfType<OVRCameraRig>();
    }

    // Update is called once per frame
    void Update()
    {
        //OVRPose tracker = OVRManager.tracker.GetPose();
        //Vector3 posHead = tracker.position;
        //Quaternion rotHead = tracker.orientation;
        //Vector3 posHead = OVRManager.tracker.GetPose().position;
        
        var posHead = rig.centerEyeAnchor.position;
        Vector3 posR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Vector3 posL = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 p = OVRManager.tracker.GetPose().position;

        //Quaternion rotHead = OVRManager.tracker.GetPose().orientation;
        var rotHead = rig.centerEyeAnchor.rotation;
        //var eulerx = rig.centerEyeAnchor.rotation.eulerAngles.x;
        //var eulery = rig.centerEyeAnchor.rotation.eulerAngles.y;
        //var eulerz = rig.centerEyeAnchor.rotation.eulerAngles.z;
        Quaternion rotR = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        Quaternion rotL = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        string rightpos = "Position of the RIGHT controller is: ";
        string leftpos = "Position of the LEFT controller is: ";
        string headpos = "Position of the HEADSET is: ";
        string rot = " And rotation is: ";
        string cl = "\n";
        string axisL = "AXIS LEFT: ";
        string axisR = "AXIS RIGHT: ";
        //distLabel.text = rightpos + posR.ToString() + rightrot + rotR.ToString() + "\n" + leftpos + posL.ToString() + leftrot + rotL.ToString() + "\n" + headpos + posHead.ToString() + headrot + rotHead.ToString() + "\n";
        string accessoryPosition = rightpos + posR.ToString() + rot + rotR.eulerAngles.ToString() + "\n" + leftpos + posL.ToString() + rot + rotL.eulerAngles.ToString() + "\n" + headpos + posHead.ToString() + rot + rotHead.ToString() + "\n"; ;
        //distLabel.text = rightpos + posR.ToString() + rightrot + rotR.eulerAngles.ToString() + "\n" + leftpos + posL.ToString() + leftrot + rotL.eulerAngles.ToString() + "\n" + headpos + posHead.ToString() + headrot + rotHead.ToString() + "\n";
        Vector2 inputAxisThumbL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        Vector2 inputAxisThumbR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        distLabel.text = accessoryPosition + cl + axisL + inputAxisThumbL.ToString() + cl + axisR + inputAxisThumbR.ToString();
    }
    /*void Update()
    {
        if (OneTransform)
        {
            float dist = Vector3.Distance(OneTransform.position, AnotherTransform.position);
            print("Distance is: " + dist);
            distLabel.text = dist.ToString();
        }
    }*/
}
