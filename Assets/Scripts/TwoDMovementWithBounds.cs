using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDMovementWithBounds : MonoBehaviour
{
    //public float upperBound;
    //public float lowerBound;
    public Vector3 Upper;
    public Vector3 Lower;
    public bool isAtLower = false;
    [SerializeField][Range(0f, 1f)] float TransitionTime; 
    void Start()
    {
        transform.position = isAtLower ? Upper : Lower;
    }
    void FixedUpdate()
    {
        if (isAtLower && Vector3.Distance(transform.position,Lower) > 0.005f)
        {
            transform.position = Vector3.Lerp(transform.position, Lower, TransitionTime);
        }
        else if (!isAtLower && Vector3.Distance(transform.position,Upper) > 0.005f)
        {
            transform.position = Vector3.Lerp(transform.position, Upper, TransitionTime);
        }
    }
}
