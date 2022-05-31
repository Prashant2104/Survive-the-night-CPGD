using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour 
{
    Rigidbody rb;
    [SerializeField] float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update ()
    {
        rb.velocity = transform.forward * speed;
    }
}