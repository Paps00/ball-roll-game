using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
      public float rotationSpeed = 30f; // Adjust the rotation speed as needed

    void Update()
    {
        // Rotate the cube around its center in all three axes
        transform.Rotate(new Vector3(1, 1, 1) * rotationSpeed * Time.deltaTime);
    }
}
