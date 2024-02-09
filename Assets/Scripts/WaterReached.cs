using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReached : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 8) && collision.CompareTag("Water"))
        {
            GameManager.instance.Player2Reached = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 8) && collision.CompareTag("Water"))
        {
            GameManager.instance.Player2Reached = false;
        }
    }
}
