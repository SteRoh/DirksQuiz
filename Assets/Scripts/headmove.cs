using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headmove : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(new Vector2(Random.Range(10000f, 5000f), Random.Range(-10000f, -5000f)));
        rb.AddTorque(Random.Range(-5000f, 5000f), ForceMode2D.Impulse);
    }
    void Update()
    {

    }

    void FixedUpdate()
    {
        //rb.AddForce(movement * maxSpeed);
    }
}
