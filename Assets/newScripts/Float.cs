using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

    public GameObject objects, objects1;
    public int tracker = 1;

    public static System.Random r = new System.Random ();
    public int x;
    public int y;
    public string name;
    public string name1;
    public string name2;
    public float speed = 250f;

    // Start is called before the first frame update
    void Start () {
        again : x = r.Next (-1, 1);
        y = r.Next (-1, 1);
        if (x == 0 || y == 0) {
            goto again;
        }

    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.name == name || other.gameObject.name == name1 || other.gameObject.name == name2) {
            x = -x;
            y = -y;

        }

    }

    void increment () {
        if (tracker == 2) {
            GameObject thePlayer = GameObject.Find ("Levelmanager");
            LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
            transform.position = playerScript.initialpos;
        } else {
            GameObject thePlayer = GameObject.Find ("Levelmanager1");
            LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
            transform.position = playerScript.initialpos;
        }

    }

    void Update () {

        if (tracker == 1) {
            if (transform.localPosition.x > 432) {

                x = -1;

            } else if (transform.localPosition.y > 107) {
                y = -1;

            } else if (transform.localPosition.x < -454) {
                x = 1;

            } else if (transform.localPosition.y < -238) {
                y = 1;

            }

            transform.position = new Vector2 (transform.position.x + (speed * x * Time.deltaTime), transform.position.y + (speed * y * Time.deltaTime));

        } else {
            increment ();
            GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
            transform.position = new Vector2 (transform.position.x + (0 * x * Time.deltaTime), transform.position.y + (0 * y * Time.deltaTime));
        }

    }
}