#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class KeypadExtra : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] FirstPersonController character;
    [SerializeField] Light deniedIndicator;
    [SerializeField] Light accessGrantedIndicator;

    public void OpenDoor() {
        door.GetComponent<Animator>().enabled = true;
        door.GetComponent<AudioSource>().Play();
        accessGrantedIndicator.enabled = true;
    }

    public void DisableControl() {
        character.m_MouseLook.SetCursorLock(false);
        character.enabled = false;
    }

    public void EnableControl() {
        character.m_MouseLook.SetCursorLock(true);
        character.enabled = true;
    }

    public void DeniedAccess() => deniedIndicator.enabled = true;
}
