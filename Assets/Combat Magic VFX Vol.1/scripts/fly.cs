using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{ 
    void Update ()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime);
    }
}