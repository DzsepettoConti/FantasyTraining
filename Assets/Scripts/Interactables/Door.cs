using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customDoor : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }
}
