using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatformLever : MonoBehaviour
{
    float mid;
    [SerializeField]
    TwoDMovementWithBounds Platform;
    public bool IsLeftLower = false;

    // Start is called before the first frame update
    void Start()
    {
        mid = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLeftLower)
        {
            if (transform.rotation.z < mid)
            {
                Platform.isAtLower = true;
            }
            else
            {
                Platform.isAtLower = false;
            }
        }
        else
        {
            if (transform.rotation.z < mid)
            {
                Platform.isAtLower = false;
            }
            else
            {
                Platform.isAtLower = true;
            }
        }
    }
}
