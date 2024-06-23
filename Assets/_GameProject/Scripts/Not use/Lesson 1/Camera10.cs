using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera10 : MonoBehaviour
{
    [SerializeField] PlayerMovement _player;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.gameObject.transform.position, 1f);
    }
}
