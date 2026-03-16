using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePLayer : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed=10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new(0,rb.velocity.y,speed);
    }
}
