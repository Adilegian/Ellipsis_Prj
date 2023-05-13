using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRyu : MonoBehaviour
{
  public Vector3 dir;
  public float speed=1f;
  
    void Update()
    {
        this.transform.Rotate(dir * speed);
    }
}
