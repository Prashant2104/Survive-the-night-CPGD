using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour 
{
	void Update ()
    {
        transform.Translate(new Vector3(0.0f, 0.0f, 0.2f)); 
    }
}