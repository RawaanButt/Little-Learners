using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    [SerializeField]
    private float _speed = 5.0f;
    private float _health = 3.0f;
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
        /*transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            float randomX = Random.Range(-7f, 7f);
            tranform.position = new Vector3(randomX, 7, 0);
        }*/
        /* time += Time.fixedDeltaTime;
         if(time>1f)
         {
             phase++;
             phase %= 6;
             time = 0f;
         }

         switch(phase){
         case 0:
                 transform.Rotate(0f, 0f, velocity * (1 - time));
                 break;
             case 1:
                 transform.Rotate(0f, 0f, -velocity * time);     
                 break;
             case 2:
                 transform.Rotate(0f, 0f, -velocity * (1 - time));
                 break;
             case 3:
                 transform.Rotate(0f, 0f, velocity * time); 
                 break;
         }*/
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
            if (player !=null)
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
