using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
  //  private PlayerInput _input;

    [SerializeField] private float _turnSpeed;


    // Start is called before the first frame update
    void Start()
    {
       // _input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * PlayerInput.GetInstance()._mouseX);
    }

}
