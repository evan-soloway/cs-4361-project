using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class camera : MonoBehaviour
{
    public grannyscript granny;
    public float smoothSpeed = 0.125f;  // Smoothing speed
    public Vector3 offset;  // Offset from the target

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial rotation to (0,0,0)
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = new Vector3(granny.transform.position.x, granny.transform.position.y, -2.582901f) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
