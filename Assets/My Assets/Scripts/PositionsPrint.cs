using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsPrint : MonoBehaviour
{
    public UnityEngine.UI.Text distLabel;
    private OVRCameraRig rig;
    private void Start()
    {
        rig = GameObject.FindObjectOfType<OVRCameraRig>();
    }

    // Update is called once per frame
    void Update()
    {
        var posHead = rig.centerEyeAnchor.position;
        Vector3 posR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Vector3 posL = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 p = OVRManager.tracker.GetPose().position;

        var rotHead = rig.centerEyeAnchor.rotation;
        Quaternion rotR = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        Quaternion rotL = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        string rightpos = "Position of the RIGHT controller is: ";
        string leftpos = "Position of the LEFT controller is: ";
        string headpos = "Position of the HEADSET is: ";
        string rot = " And rotation is: ";
        string cl = "\n";
        string axisL = "AXIS LEFT: ";
        string axisR = "AXIS RIGHT: ";
        string accessoryPosition = rightpos + posR.ToString() + rot + rotR.eulerAngles.ToString() + "\n" + leftpos + posL.ToString() + rot + rotL.eulerAngles.ToString() + "\n" + headpos + posHead.ToString() + rot + rotHead.ToString() + "\n"; ;
        Vector2 inputAxisThumbL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        Vector2 inputAxisThumbR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        distLabel.text = accessoryPosition + cl + axisL + inputAxisThumbL.ToString() + cl + axisR + inputAxisThumbR.ToString();
    }
}
