using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floata: MonoBehaviour
{
     private float latestDirectionChangeTime;
 private readonly float directionChangeTime = 3f;
 private float characterVelocity = 30f;
 private Vector2 movementDirection;
 private Vector2 movementPerSecond;
 public GameObject two;
 public Vector2 currentpos;
 
 void Start(){
     latestDirectionChangeTime = 0f;
     calcuateNewMovementVector();
 }
 
 void calcuateNewMovementVector(){
    //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
     movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
     movementPerSecond = movementDirection * characterVelocity;
 }
 
 void Update(){
     currentpos=two.transform.position;
     //if the changeTime was reached, calculate a new movement vector
    //  if (Time.time - latestDirectionChangeTime > directionChangeTime){
    //      latestDirectionChangeTime = Time.time;
    //      calcuateNewMovementVector();
    //  }
     //current position of gameObject
        //  Debug.Log(currentpos.y);
        //   Debug.Log(currentpos.x);

         if( currentpos.x > 1000||currentpos.y <= 140||currentpos.y >= 400||currentpos.x < 195)
         {
           calcuateNewMovementVector();
       
         }
        //     else if(currentpos.y > -300 || currentpos.x > -1000){
        //    calcuateNewMovementVector();
       
        //     }
     
     //move enemy: 
     transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), 
     transform.position.y + (movementPerSecond.y * Time.deltaTime));
 
 }
}
