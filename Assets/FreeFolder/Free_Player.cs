using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Free_Player : MonoBehaviour
{
    List<UnitBody> unitBodyColliders = new List<UnitBody>();
    List<Rigidbody> rigidbodies = new List<Rigidbody>();


    Rigidbody rigdbody;
    Vector3 moveDir;
    void Start()
    {

        unitBodyColliders.AddRange(GetComponentsInChildren<UnitBody>());

        for (int i = 0; i < unitBodyColliders.Count; i++)
        {
            rigidbodies.Add(unitBodyColliders[i].GetComponent<Rigidbody>());
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(horizontalInput, 0, verticalInput);
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].velocity = moveDir;
        }
    }
}
