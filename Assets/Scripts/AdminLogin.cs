using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.IO;
public class AdminLogin : MonoBehaviour
{
    private const bool Value = true;
    public GameObject username;
    //public GameObject Panel;

    public GameObject Background;

    public GameObject Empty;
    public GameObject textDisplay;
    private string Username;
    private String[] Lines;


    public float time = 1.5f;


    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.Log("Failed to resolve firebase dependencies. Error: " + task.Result);
            }
        });
    }

    private void InitializeFirebase()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://little-learners-2022-default-rtdb.firebaseio.com/");
        Debug.Log("Initialized Firebase Successfully");
        //(https://test-b8c15-default-rtdb.firebaseio.com/)
    }

    void Waiting()
    {
        textDisplay.SetActive(false);
        Empty.SetActive(false);

    }

    async public void LoginButton()
    {
        bool UN = false;
        if (Username != "")
        {
            await FirebaseDatabase.DefaultInstance.GetReference("admin").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.Child(Username).Exists)
                    {
                        UN = true;
                    }

                }
            });
            // if (System.IO.File.Exists(@"D:\New folder\TinyHands\Assets\Scripts" + Username + ".txt"))
            // {
            //     UN = true;
            //     Lines = System.IO.File.ReadAllLines(@"D:\New folder\TinyHands\Assets\Scripts" + Username + ".txt");

            // }
            // else
            if (!UN)
            {
                Debug.LogWarning("Admin Username Invalid ");
                textDisplay.GetComponent<Text>().text = "Invalid Admin Username";
                textDisplay.SetActive(true);
                Invoke("Waiting", 2f);

            }

        }
        else
        {
            Debug.LogWarning("Admin Username Field Empty");
            Empty.GetComponent<Text>().text = "Enter Admin Username First";
            Empty.SetActive(true);
            Invoke("Waiting", 2f);

        }

        if (UN == true)
        {

            username.GetComponent<InputField>().text = "";
            print("Login Successful");
            File.WriteAllText(Application.dataPath + "/user.txt", Username);
            SceneManager.LoadScene("Admin Record Search");


            /*if (Panel != null)
			{
				Panel.SetActive(true);
			}

				if (Background != null)
			{
				bool isActive = Background.activeSelf;
				Background.SetActive(! isActive);
			}
*/
        }
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            username.GetComponent<InputField>().Select();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {

            if (Username != "")
            {
                LoginButton();
            }

        }
        Username = username.GetComponent<InputField>().text;



    }

}


