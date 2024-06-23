using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _door.UnlockDoor();
            _door.OpenDoor(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _door.OpenDoor(false);
    }
}
