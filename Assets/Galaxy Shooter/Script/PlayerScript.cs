using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public bool tripleshoot= false, speedbost=false, sheild=false;
    [SerializeField]
    public float canfire = 0.0f;
    public float firerate=0.25f;
    public float speed = 10; 
        public int lifes=3;
    
    [SerializeField]
    public GameObject laser, tripleshot, explosion, sheildActive, exPrefab;

    private UIManager _uIManager;
    private GameManager _gameManger;
    private spawnManager _spawnManager;
    //public Rigidbody player;
    // Start is called before the first frame update
    void Start()

    {
        
         transform.position = new Vector3(0, 4, 0);
        //player.AddForce(new Vector2(-1.0f, 0.5f), ForceMode.Impulse);

        _gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uIManager != null)
        {
            _uIManager.updateLives(lifes);
        }
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<spawnManager>();
        if (_spawnManager!= null)
        {
            _spawnManager.startSpawnRoutine();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }

    }
    public void movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        // transform.Translate(0,x*speed,y*speed, Space.World);
        /* transform.Translate(Vector3.right * x * speed * Time.deltaTime);
          transform.Translate(Vector3.up * y * speed * Time.deltaTime);
         if (transform.position.y>0.5)
         {
             transform.position = new Vector3(transform.position.x, 0.5f);
         }
         else if (transform.position.y < -4.15f)
         {
             transform.position = new Vector3(transform.position.x, -4.15f);
         }
         if (transform.position.x>16f)
         {
             transform.position = new Vector3(-15.5f, transform.position.y);
         }
         else if (transform.position.x <-16f)
         {
             transform.position = new Vector3(15.5f, transform.position.y);
         }*/


        var position = transform.position;
        if (speedbost==true)
        {
            speed = 18;
            position += new Vector3(x, y) * Time.deltaTime * speed;
        }
        position += new Vector3(x, y) * Time.deltaTime * speed;
        position.x = Mathf.Clamp(position.x, -17.5f, 20.5f);
        position.y = Mathf.Clamp(position.y, -5.15f, 1.1f);
        transform.position = position;
    }
    private void shoot()
    {
       if (Time.time > canfire )
        {
            if (tripleshoot==true)
            {
                //left Instantiate(laser, transform.position + new Vector3(-1.55f, -1.86f, 0), Quaternion.identity);
                //right Instantiate(laser, transform.position + new Vector3(1.55f, -1.86f, 0), Quaternion.identity);
                //center
                Instantiate(tripleshot, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laser, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
        
        canfire = Time.time + firerate;
        }
    }
    public void powerup()
    {
        StartCoroutine(tonormal());
    }
    public void sheildEnable()
    {
        sheildActive.SetActive(true);
    }
    public IEnumerator tonormal()
    {
        if (tripleshoot==true)
        {
            yield return new WaitForSeconds(10.0f);
            _uIManager.powerEmpty();
            tripleshoot = false;
            
        }
        if (speedbost == true)
        {
            yield return new WaitForSeconds(10.0f);
            _uIManager.powerEmpty();
            speedbost = false;
        }
        if (sheild == true)
        {
            yield return new WaitForSeconds(15.0f);
            _uIManager.powerEmpty();
            sheild = false;
            sheildActive.SetActive(false);
        }
    }
    public void damage()
    {
        if(sheild==true)
        {
            sheild = false;
            sheildActive.SetActive(false);
            return;
        }
        lifes--;
        _uIManager.updateLives(lifes);
        if (lifes<1)
        {
                exPrefab = Instantiate(explosion, transform.position, Quaternion.identity);
            _gameManger.gameOver = true;
            _uIManager.showTitleScreen();
            Destroy(this.gameObject);
           Destroy(exPrefab.gameObject, 4f);

        }
    }
}
