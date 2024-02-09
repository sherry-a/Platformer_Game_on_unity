using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullets : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public bool isRight = true;
    public Vector3 offSet;
    public float Delay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnBullet");
    }

    IEnumerator SpawnBullet()
    {
        Debug.Log("Yes");
        while (true)
        {
            GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x + ((isRight) ? offSet.x : -1 * offSet.x), transform.position.y + offSet.y, transform.position.z + offSet.z), transform.rotation);
            newBullet.GetComponent<BulletScript>().isRight = isRight;
            yield return new WaitForSeconds(Delay);
        }
    }
}
