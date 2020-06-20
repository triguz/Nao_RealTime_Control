using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    public class QuickActionsManager : MonoBehaviour
    {
        public bool headEnabled;
        public bool ArmsEnabled;
        public bool HandsEnabled;
        public bool WalkerEnabled;
        //public Image headBackground;
        public GameObject left_panel;
        public GameObject right_panel;
        public Transform target;

        public void Start()
        {
            headEnabled = false;
            ArmsEnabled = false;
            HandsEnabled = false;
            WalkerEnabled = false;
        }

        public void Update()
        {
            float ui_offset = 1.2f;
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z + ui_offset);

            if (OVRInput.GetDown(OVRInput.RawButton.Start))
            {
                left_panel.SetActive(!left_panel.activeSelf);
                right_panel.SetActive(!right_panel.activeSelf);
            }
        }

        public void EnableDisableHead(Text label)
        {
            if (!headEnabled)
            {
                label.text = "Dsiable" + "\n" + "Head";
                //ChangeColorInactive(headBackground);
            }
            else
            {
                label.text = "Enable" + "\n" + "Head";
                //ChangeColorActive(headBackground);
            }
            headEnabled = !headEnabled;
        }

        public void EnableDisablArms(Text label)
        {
            if (!ArmsEnabled)
            {
                label.text = "Disable" + "\n" + "Arms";
            }
            else
            {
                label.text = "Enable" + "\n" + "Arms";
            }
            ArmsEnabled = !ArmsEnabled;
        }

        public void EnableDisableHands(Text label)
        {
            if (!HandsEnabled)
            {
                label.text = "Disable" + "\n" + "Hands";
            }
            else
            {
                label.text = "Enable" + "\n" + "Hands";
            }
            HandsEnabled = !HandsEnabled;
        }

        public void EnableDisableWalker(Text label)
        {
            if (!WalkerEnabled)
            {
                label.text = "Disable" + "\n" + "Walker";
            }
            else
            {
                label.text = "Enable" + "\n" + "Walker";
            }
            WalkerEnabled = !WalkerEnabled;
        }

        public void ChangeColorActive(Image background)
        {
            background.color = new Color(0, 200, 25, 255);
        }

        public void ChangeColorInactive(Image background)
        {
            background.color = new Color(0, 140, 255, 255);
        }
    }
}
