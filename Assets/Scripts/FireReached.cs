using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireReached : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 6) && collision.CompareTag("Fire"))
        {
            GameManager.instance.Player1Reached = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 6) && collision.CompareTag("Fire"))
        {
            GameManager.instance.Player1Reached = false;
        }
    }
}
