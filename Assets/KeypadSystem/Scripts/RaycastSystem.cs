using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Camera))]
public class RaycastSystem : MonoBehaviour
{
    [InfoBox("This \"RaycastSystem\" will raycast from the camera untill it hits an object with the component \"Keypad\" attached.", EInfoBoxType.Normal)]

    [Header("Setup")]
    [Tooltip("Add a key for keypad interaction. Default: Left Mouse Click")]
    [SerializeField] KeyCode interactKey= KeyCode.Mouse0;
    [Tooltip("The camera from which the raycast should be used. Null value uses the current object the system is attached to.")]
    [SerializeField] Camera characterCamera = null;
    [Tooltip("This is the distance of which you can interact with the keypad. Greater number equals further away.")]
    [SerializeField] float distance = 1.5f;

    [Tooltip("A method to run to deactivate and setup features before keypad is activated.")]
    public UnityEvent disableControl;
    [Tooltip("A method to restore features.")]
    public UnityEvent restoreControl;

    void Update() => KeypadChecker();

    void KeypadChecker () {
        //Make sure we dont interact with it through UI objects.
        if (!KeypadManager.instance.IsPointerOverUI()) {

            Keypad keypad;

            if (characterCamera == null)
            {
                if (GetComponent<Camera>())
                    keypad = KeypadManager.instance.RayCastMouseClickGetObject(GetComponent<Camera>());
                else
                {
                    Debug.LogError("No camera has been set and \"RaycastSystem\" is attached to an object that doesnt have a camera either. Please reference a camera object.");
                    return; // Failsafe if component doesnt have a camera.
                }
            }
            else
                keypad = KeypadManager.instance.RayCastMouseClickGetObject(characterCamera, distance);

            if (keypad != null) {
                if (Input.GetKeyDown(interactKey)) {
                    disableControl?.Invoke();
                    keypad.returnControl = restoreControl;
                    KeypadManager.instance.ShowKeypad(keypad);
                }
            }
        }
    }
}