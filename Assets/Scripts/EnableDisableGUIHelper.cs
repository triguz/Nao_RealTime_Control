using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableGUIHelper : MonoBehaviour
{
    public GameObject target;
    public void EnableOrDisable()
    {
        target.SetActive(!target.activeSelf);
    }
}
