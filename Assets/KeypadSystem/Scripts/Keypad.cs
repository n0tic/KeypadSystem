using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class Keypad : MonoBehaviour
{
    [Header("States")]
    [Tooltip("Set keypad to solved or not solved.")]
    public bool keycodeSolved = false;
    public bool permanentlyLocked = false;

    [Header("Keypad Setup:")]
    [Tooltip("Should this be activated on mouse click event?")]
    public bool activateByMouseclick = false;
    [Tooltip("Enter a code for the keypad.")]
    public int keycode;
    [Tooltip("Do you want the keypad to autocomplete once the correct keycode is entered?")]
    public bool autoComplete = false;

    [Header("Keypad Extra:")]
    [Tooltip("Limit the amount of tries.")]
    public bool limitTries = false;
    public int triesAmount = 5;

    [Header("Methods to run:")]
    public UnityEvent accessGranted;
    public UnityEvent accessDenied;
    public UnityEvent returnControl;

    void Awake() { if (keycodeSolved) GrantAccess(); else if (permanentlyLocked) DenyAccess(); }

    public void GrantAccess() {
        accessGranted?.Invoke();
        returnControl?.Invoke();
    }
    public void DenyAccess() {
        accessDenied?.Invoke();
        returnControl?.Invoke();
    }
    public void ReturnControl() => returnControl?.Invoke();

    private void OnMouseDown() {
        if(activateByMouseclick) {
            if (!KeypadManager.instance.IsPointerOverUI())//Make sure we dont interact with it through UI objects.
                if(!permanentlyLocked)
                    KeypadManager.instance.ShowKeypad(this);
        }
    }
}
