#pragma warning disable CS0649

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeypadManager : MonoBehaviour
{
    public static KeypadManager instance = null;

    [SerializeField] TMP_InputField input;
    [SerializeField] TMP_Text textComponent;

    [Header("Text colors:")] // Standard colors
    public List<Color> colors = new List<Color> { new Color(0.1401745f, 0.6603774f, 0.1783226f, 1), new Color(0.8490566f, 0.1870883f, 0.1321645f, 1), new Color(0.1960784f, 0.1960784f, 0.1960784f, 1) };

    Keypad keypad;
    Coroutine errorRutine;
    int attempts = 0;

    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);

        ClosePad();
    }

    public void ClosePad() {
        if (errorRutine != null)
            StopErrorRutine();

        gameObject.SetActive(false);
        ClearInputfield();
        if(keypad != null)
            keypad.ReturnControl();
        keypad = null;

        foreach (Transform transform in transform.GetChild(2))
            transform.GetComponent<Button>().interactable = false;
    }

    public void CheckCode() {
        StopErrorRutine();

        if(!keypad.keycodeSolved) {
            if (keypad != null && keypad.autoComplete) {
                if (input.text.Length <= keypad.keycode.ToString().Length) {
                    int result = -1;
                    Int32.TryParse(input.text, out result);
                    if (keypad.keycode == result) GrantAccess();
                }
                else if(input.text != string.Empty && input.text != "Access Granted" && input.text != "Access Denied")
                    KeycodeError();
            }
        }
    }

    void GrantAccess() {
        if (errorRutine != null)
            StopErrorRutine();

        textComponent.color = colors[0];

        keypad.keycodeSolved = true;
        keypad.GrantAccess();
        ClosePad();
    }

    void StopErrorRutine() {
        if (errorRutine == null)
            return;

        StopCoroutine(errorRutine);
        errorRutine = null;
        textComponent.color = colors[2];
    }

    private void KeycodeError() {
        if(errorRutine != null)
            StopErrorRutine();

        errorRutine = StartCoroutine(ShowError());

        attempts++;

        if (keypad.limitTries)
            if (attempts >= keypad.triesAmount) {
                if (errorRutine != null)
                    StopErrorRutine();

                textComponent.color = colors[1];

                keypad.permanentlyLocked = true;
                keypad.DenyAccess();
                ClosePad();
            }
    }

    public void ClearInputfield() => input.text = "";

    public void ShowKeypad(Keypad _keypad) {
        ClearInputfield();
        keypad = _keypad;
        attempts = 0;

        gameObject.SetActive(true);

        if (keypad.keycodeSolved)
            SetButtonsInteractable(false);
        else
        {
            if(!keypad.permanentlyLocked)
                SetButtonsInteractable(true);
            else
                SetButtonsInteractable(false);
        }
    }

    private void SetButtonsInteractable(bool state) {
        foreach (Transform transform in transform.GetChild(2))
            transform.GetComponent<Button>().interactable = state;

        if (state) textComponent.color = colors[2];
        else
        {
            input.text = "Access Granted";
            textComponent.color = colors[0];
        }

        if(keypad.permanentlyLocked) {
            input.text = "Access Denied";
            textComponent.color = colors[1];
        }
    }

    public void KeyInput(int num) => input.text += num.ToString();

    public void SendInput() {
        if (input.text.Length < 1) return;

        if (input.text.Length > 0 && keypad.keycode == Int32.Parse(input.text)) GrantAccess();
        else KeycodeError();
    }

    public void Erase() { try { input.text = input.text.Substring(0, input.text.Length - 1); } catch (System.Exception) {  } }

    public Keypad RayCastMouseClickGetObject(Camera camera, float distance = 2f) {
        
        Keypad keypad = null;
        RaycastHit HitInfo;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out HitInfo, distance)) {
            Debug.DrawRay(camera.transform.position, camera.transform.forward * 100.0f, Color.yellow);
            if (HitInfo.collider.transform.GetComponent<Keypad>())
                keypad = HitInfo.collider.transform.GetComponent<Keypad>();
        }
        return keypad;
    }

    public bool IsPointerOverUI() {
        if (EventSystem.current.IsPointerOverGameObject()) return true;
        else {
            PointerEventData pe = new PointerEventData(EventSystem.current);
            pe.position = Input.mousePosition;
            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pe, hits);
            return hits.Count > 0;
        }
    }

    IEnumerator ShowError() {
        textComponent.color = colors[1];
        yield return new WaitForSeconds(.25f);
        textComponent.color = colors[2];
        yield return new WaitForSeconds(.25f);
        textComponent.color = colors[1];
        yield return new WaitForSeconds(.25f);
        textComponent.color = colors[2];
        yield return new WaitForSeconds(.25f);
        textComponent.color = colors[1];
        yield return new WaitForSeconds(.25f);
        textComponent.color = colors[2];
        input.text = "";
    }
}
