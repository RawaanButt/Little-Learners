using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisiondetection : MonoBehaviour
{

    public int x;
    public int y;
    public string name;
    // Start is called before the first frame update
    void Start()
    {

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == name)
        {
            Debug.Log("collion detected");
            x = -x;
            y = -y;
        }
        if (other.gameObject.name == "one (1)")
        {
            Debug.Log("collion detected 1");
        }
    }
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "one (1)")
    //         Destroy(gameObject);
    // }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + (80 * x * Time.deltaTime), transform.position.y + (80 * y * Time.deltaTime));

    }
}
