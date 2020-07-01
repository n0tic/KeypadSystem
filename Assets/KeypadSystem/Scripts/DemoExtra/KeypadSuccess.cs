#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadSuccess : MonoBehaviour
{
    [SerializeField] GameObject door;

    public void OpenDoor()
    {
        door.GetComponent<Animator>().enabled = true;
        door.GetComponent<AudioSource>().Play();
    }
}
