using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Keypad : MonoBehaviour
{
    [Tooltip("Set keypad to solved or not solved.")]
    public bool keycodeSolved = false;

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

    [Header("Text colors:")]
    public List<Color> colors = new List<Color> { new Color(0.1401745f, 0.6603774f, 0.1783226f, 1), new Color(0.8490566f, 0.1870883f, 0.1321645f, 1), new Color(0.1960784f, 0.1960784f, 0.1960784f, 1) };

    [Header("Methods to run:")]
    public UnityEvent accessGranted;
    public UnityEvent accessDenied;
    public UnityEvent returnControl;

    public void GrantAccess() {
        accessGranted?.Invoke();
        returnControl?.Invoke();
    }
    public void GrantDenied() {
        accessDenied?.Invoke();
        returnControl?.Invoke();
    }
    public void ReturnControl() => returnControl?.Invoke();

    private void OnMouseDown()
    {
        if(activateByMouseclick)
        {
            //Make sure we dont interact with it through UI objects.
            if (!KeypadManager.instance.IsPointerOverUI())
                KeypadManager.instance.ShowKeypad(this);
        }
    }
}
