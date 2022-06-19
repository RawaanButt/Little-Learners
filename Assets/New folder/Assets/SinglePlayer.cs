using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SinglePlayer : MonoBehaviour {


	public void Single_Player(){
	
		SceneManager.LoadScene("Game Selection");
	}

		public void Back_mainmeanu(){

			SceneManager.LoadScene("Mian Menu");
		}

		public void loading(){
			
			SceneManager.LoadScene("Loading Screen");
		}

public void Registere_page(){
	
		SceneManager.LoadScene("Registraion");
	}

	public void adminRegistration()
	{

		SceneManager.LoadScene("Admin Registration");
	}
	public void adminLogin()
	{

		SceneManager.LoadScene("Admin login 1");
	}



}
