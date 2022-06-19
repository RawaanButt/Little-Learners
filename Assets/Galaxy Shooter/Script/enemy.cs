using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    public GameObject exPrefab;
    private float _speed = 5.0f;
    private UIManager _uIManager;
    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y < -8)
        {
            float randomX = Random.Range(-15f, 15f);
            transform.position = new Vector3(randomX, 11.5f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);
            exPrefab = Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uIManager.updateScore();
            Destroy(this.gameObject);
            Destroy(exPrefab.gameObject, 4f);
        }
        else if (other.tag == "Player")
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player!= null)
            {
                player.damage();
            }
            exPrefab = Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
            Destroy(exPrefab.gameObject, 4f);
        }
       

    } 
}
