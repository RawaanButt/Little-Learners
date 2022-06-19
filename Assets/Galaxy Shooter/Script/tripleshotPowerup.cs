using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tripleshotPowerup : MonoBehaviour
{
    public float speed = 3.0f;
    public Rigidbody2D playar;
    public string trpleshoot = "Triple Boost";
    public string spedbost = "Speed Boost";
    public string shield = "Shield";
    public int id;
    [SerializeField]
   // private AudioClip _clip;
    private UIManager _uIManager;
    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D playar)
    {
        PlayerScript player = playar.GetComponent<PlayerScript>();
       // AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

        if (player != null)
        {
            if (id == 0)
            {
                player.tripleshoot = true;
                player.powerup();
                _uIManager.PowerGain(trpleshoot);
                Destroy(this.gameObject);
            }
            if (id == 1)
            {
                player.speedbost = true;
                player.powerup();
                _uIManager.PowerGain(spedbost);
                Destroy(this.gameObject);
            }
            if (id == 2)
            {
                player.sheild = true;
                _uIManager.PowerGain(shield);
                player.sheildEnable();
                Destroy(this.gameObject);
            }
        }
        
        
    }
}
