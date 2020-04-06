using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform _camera;

    void Start()
    {
        _camera = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + _camera.rotation * Vector3.forward, _camera.rotation * Vector3.up);
    }
}
