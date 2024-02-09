using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector3 Right;
    public Vector3 Left;
    [SerializeField][Range(0, 1)] float Speed;
    [HideInInspector] public bool ShouldMove = true;
    bool MoveRight = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ShouldMove)
        {
            if (MoveRight && Vector3.Distance(transform.position, Right) > 0.005f)
            {
                transform.position = Vector3.MoveTowards(transform.position, Right, Speed);
            }
            else if (!MoveRight && Vector3.Distance(transform.position, Left) > 0.005f)
            {
                transform.position = Vector3.MoveTowards(transform.position, Left, Speed);
            }
        }
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, Right) < 0.005f)
        {
            MoveRight = false;
        }
        else if (Vector3.Distance(transform.position, Left) < 0.005f)
        {
            MoveRight = true;
        }
    }
}
