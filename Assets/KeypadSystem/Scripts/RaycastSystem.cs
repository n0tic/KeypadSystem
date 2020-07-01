using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Camera))]
public class RaycastSystem : MonoBehaviour
{
    [Tooltip("A method to run to deactivate and setup features before keypad is activated.")]
    public UnityEvent disableControl;
    [Tooltip("A method to restore features.")]
    public UnityEvent restoreControl;

    void Update() => KeypadChecker();

    void KeypadChecker () {
        //Make sure we dont interact with it through UI objects.
        if (!KeypadManager.instance.IsPointerOverUI()) {
            Keypad keypad = KeypadManager.instance.RayCastMouseClickGetObject(GetComponent<Camera>());
            if (keypad != null) {
                if (Input.GetKeyDown(KeyCode.Mouse0)) {
                    disableControl?.Invoke();
                    keypad.returnControl = restoreControl;
                    KeypadManager.instance.ShowKeypad(keypad);
                }
            }
        }
    }

    public void DisableControl()
    {
        transform.parent.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);
        transform.parent.GetComponent<FirstPersonController>().enabled = false;
    }

    public void EnableControl()
    {
        transform.parent.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(true);
        transform.parent.GetComponent<FirstPersonController>().enabled = true;
    }
}