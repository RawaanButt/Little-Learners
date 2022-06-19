using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astriod : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    [SerializeField]
    private float _speed = 8.0f;
    private float _health = 2.0f;
    private UIManager _uIManager;
    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }
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
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            _health--;
            Debug.Log(_health);
            if (_health < 1)
            {
                Destroy(other.gameObject);
                Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);

            }
            _uIManager.updateScore();
        }
        else if (other.tag == "Player")
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.damage();
                _health--;
                Debug.Log(_health);
                if (_health < 1)
                {
                    Destroy(this.gameObject);
                    Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

                }
            }
        }
    }
}
