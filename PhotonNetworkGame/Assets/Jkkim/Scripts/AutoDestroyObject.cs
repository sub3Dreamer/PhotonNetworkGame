using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyObject : MonoBehaviour
{
    void Start()
    {
        DestroyImmediate(this.gameObject);
    }
}
