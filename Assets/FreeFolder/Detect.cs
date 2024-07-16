using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{

    Rigidbody rigidbody;
    [SerializeField] float moveSpd;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
    }


    void Move()
    {

        Vector3 moveDir;
        float inputHor = Input.GetAxisRaw("Horizontal");
        float inputVer = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(inputHor, 0, inputVer).normalized;

        rigidbody.velocity = moveDir * moveSpd;

    }

}
