using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressMotorChanger : MonoBehaviour
{
    public HingeJoint2D Platform;
    float sign;
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sign = Platform.motor.motorSpeed / Mathf.Abs(Platform.motor.motorSpeed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Platform.motor.motorSpeed > 0)
        {
            Platform.motor = new JointMotor2D { motorSpeed = -1 * Platform.motor.motorSpeed, maxMotorTorque = Platform.motor.maxMotorTorque };
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Platform.motor.motorSpeed < 0)
        {
            Platform.motor = new JointMotor2D { motorSpeed = Mathf.Abs(Platform.motor.motorSpeed), maxMotorTorque = Platform.motor.maxMotorTorque };
        }
    }
}
