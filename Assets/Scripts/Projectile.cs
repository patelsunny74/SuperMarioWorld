using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    

    // Start is called before the first frame update
    void Start()
    {
        if(lifeTime <= 0)
        {
            lifeTime = 1.0f;
        }
        if(speed <= 0)
        {
            speed = 2.0f;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0);

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
