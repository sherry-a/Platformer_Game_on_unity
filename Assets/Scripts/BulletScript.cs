using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector]
    public bool isRight = false;

    [SerializeField][Range(0, 1)]
    public float speed = 0.0f;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + ((isRight) ? 1 : -1), transform.position.y, transform.position.z), speed); ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Turret") || collision.CompareTag("Fire") || collision.CompareTag("Water"))
        {
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
