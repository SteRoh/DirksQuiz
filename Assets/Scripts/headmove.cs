using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headmove : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(50, 50));
    }
    void Update()
    {

    }

    void FixedUpdate()
    {
        //rb.AddForce(movement * maxSpeed);
    }
}
