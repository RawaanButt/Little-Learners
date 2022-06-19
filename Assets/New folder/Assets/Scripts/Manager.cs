using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	public GameObject one,two,three,four,five,d1,d2,d3,d4,d5;
	Vector2 oneinitialpos,twoinitialpos,threeinitialpos,fourinitialpos,fiveinitialpos;
	void Start() {

		oneinitialpos=one.transform.position;
		twoinitialpos=two.transform.position;
		threeinitialpos=three.transform.position;
		fourinitialpos=four.transform.position;
		fiveinitialpos=five.transform.position;

	}
	public void Dragone(){
		one.transform.position=Input.mousePosition;
	}
	public void Dragtwo(){
		two.transform.position=Input.mousePosition;
	}
	public void Dragthree(){
		three.transform.position=Input.mousePosition;
	}
	public void Dragfour(){
		four.transform.position=Input.mousePosition;
	}
	public void Dragfive(){
		five.transform.position=Input.mousePosition;
	}

	public void dropone(){
		float Distance=Vector3.Distance(one.transform.position, d1.transform.position);
		if(Distance<80){


			one.transform.position=d1.transform.position;

		}
		else{
			one.transform.position=oneinitialpos;
		}
	}
	public void droptwo(){
		float Distance=Vector3.Distance(two.transform.position, d2.transform.position);
		if(Distance<50){
			two.transform.position=d2.transform.position;

		}
		else{
			two.transform.position=twoinitialpos;
		}
	}
	public void dropthree(){
		float Distance=Vector3.Distance(three.transform.position, d3.transform.position);
		if(Distance<50){
			three.transform.position=d3.transform.position;
		}
		else{
			three.transform.position=threeinitialpos;
		}
	}
	public void dropfour(){
		float Distance=Vector3.Distance(four.transform.position, d4.transform.position);
		if(Distance<50){
			four.transform.position=d4.transform.position;
		}
		else{
			four.transform.position=fourinitialpos;
		}
	}
	public void dropfive(){
		float Distance=Vector3.Distance(five.transform.position, d5.transform.position);
		if(Distance<50){
			five.transform.position=d5.transform.position;
		}
		else{
			five.transform.position=fiveinitialpos;
		}
	}


}
