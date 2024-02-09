using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressActiveFalse : MonoBehaviour
{
    [SerializeField]
    GameObject[] Objs;

    private void OnTriggerStay2D(Collider2D collision)
    {
        for (int i = 0; i < Objs.Length; i++)
        {
            Objs[i].SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < Objs.Length; i++)
        {
            Objs[i].SetActive(true);
        }
    }
}
