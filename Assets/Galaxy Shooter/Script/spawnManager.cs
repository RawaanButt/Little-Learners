using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
   
    [SerializeField]
    public Transform playerShip;
    public GameObject enemyShipPrefab;
    public GameObject bigEnemyShipPrefab;
    public GameObject astriodPrefab;
    [SerializeField]
    public GameObject[] powerups;
    private GameManager _gameManger;

    // Start is called before the first frame update
    void Start()
    {
        _gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemySpwanRoutine());
        StartCoroutine(PowerupSpwanRoutine());
    }


    // Update is called once per frame
    public void startSpawnRoutine() {
        StartCoroutine(EnemySpwanRoutine());
        StartCoroutine(PowerupSpwanRoutine());
    }
    IEnumerator EnemySpwanRoutine()
    {
        while(_gameManger.gameOver == false)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-15.0f, 15.0f), 11.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
    public void SpawnRandEnemyNow()
    {
        Debug.Log("New Enemy");
        Instantiate(bigEnemyShipPrefab, new Vector3(Random.Range(-15.0f, 15.0f), 11.5f, 0), Quaternion.identity);
    }
    public void SpawnAstriod()
    {
        Debug.Log("Astriod");
        Instantiate(astriodPrefab, new Vector3(Random.Range(-15.0f, 15.0f), 11.5f, 0), Quaternion.identity);
    }
    IEnumerator PowerupSpwanRoutine()
    {
        while (_gameManger.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-15.0f, 15.0f), 11.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(8.0f);
        }
    }
    public void SpawnPowerUpAtShipPos(int num)
    {
        playerShip = GameObject.FindWithTag("Player").transform;
        Instantiate(powerups[num], playerShip.position, Quaternion.identity);
    }
    public void SpawnPowerUpAtShipPos(GameObject obj)
    {
        playerShip = GameObject.FindWithTag("Player").transform;
        Instantiate(obj, playerShip.position, Quaternion.identity);
    }
}
