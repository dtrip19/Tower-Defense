using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] float force;
    Transform _transform;
    Rigidbody rb;


    private void Awake()
    {
        _transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        _transform.position += new Vector3(Mathf.Cos(Time.time), Mathf.Sin(Time.time), 0)*Time.deltaTime;
    }

    private void Jump()
    {
        rb.AddForce(new Vector3(0, force, 0));
    }
}
