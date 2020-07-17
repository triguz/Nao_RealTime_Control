using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinRobot : MonoBehaviour
{
    public GameObject pinnedParent;
    public GameObject standardPosition;
    protected bool pinStatus { get; set; }


    public void SetPin()
    {
        if (!pinStatus)
        {
            gameObject.transform.SetParent(pinnedParent.transform, false);
            gameObject.transform.position.Set(pinnedParent.transform.position.x, 0.0f, pinnedParent.transform.position.z);
            ChangePinMode();
        }
        else
        {
            gameObject.transform.SetParent(standardPosition.transform, false);
            //gameObject.transform.parent = standardPosition.transform;
            ChangePinMode();
        }
    }

    protected void ChangePinMode()
    {
        pinStatus = !pinStatus;
    }
}
