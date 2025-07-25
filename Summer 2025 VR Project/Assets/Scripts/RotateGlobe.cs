using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGlobe : MonoBehaviour
{
    public Vector3 rotation;
    public float speed = 0;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotation * speed * Time.deltaTime);
    }
}
