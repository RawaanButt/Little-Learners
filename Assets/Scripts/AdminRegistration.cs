using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdminRegistration : MonoBehaviour
{

    public GameObject username;
    public GameObject age;
    public GameObject autismlevel;

    public GameObject textDisplay;
    public GameObject textDisplay1;

    private string Username;
    private string Password;
    private string AutismLevel;

    private string form;

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
    }

    void Start()
    {
        // writeNewUser("1", "shayan", "12");
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWith(task =>
        // {
        //     if (task.IsFaulted)
        //     {
        //         // Handle the error...
        //     }
        //     else if (task.IsCompleted)
        //     {
        //         DataSnapshot snapshot = task.Result;
        //         // Do something with snapshot...

        //         foreach (var childSnapshot in snapshot.Children)
        //         {
        //             Debug.Log("BEGIN");
        //             Debug.Log(childSnapshot.Key);
        //             // LevelList.Add(childSnapshot.Key);
        //             Debug.Log("END");
        //         }
        //         // Debug.Log(snapshot.key);
        //         if (snapshot.Child("sami").Exists)
        //         {
        //             Debug.Log("sami bhai");
        //         }
        //         if (snapshot.Child("bakr").Exists)
        //         {
        //             Debug.Log("bakr bhai");
        //         }
        //         Debug.Log(snapshot.Child("sami"));
        //         Debug.Log(snapshot.Child("bakr/age").Value);
        //     }
        // });
    }

    public void wait()
    {
        textDisplay.SetActive(false);
        textDisplay1.SetActive(false);

    }

    //////for displayin text

    async public void RegisterButtton()
    {
        bool UN = false;

        if (Username != "" && Password != "")
        {
            /*int age1 = Int32.Parse(Age);
            Debug.Log(age1);
            char c = Username[0];
            int x = c;*/

            /*if ((x >= 65 && x <= 90 && age1 != 0) || (x >= 97 && x <= 122 && age1 != 0))
            {
                Debug.Log("1");*/
                await FirebaseDatabase.DefaultInstance.GetReference("admin").GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        // Handle the error...
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;

                        if (!snapshot.Child(Username).Exists)
                        {
                            UN = true;
                        }

                    }
                });

            /*}
            else
            {
                textDisplay1.SetActive(true);
                textDisplay1.GetComponent<Text>().text = "Invalid User Name or Age";
                Invoke("wait", 0.5f);
                return;
            }*/
        }
        else
        {
            Debug.LogWarning("Username field Empty");
            textDisplay1.SetActive(true);
            textDisplay1.GetComponent<Text>().text = "Username field Empty";
            Invoke("wait", 0.5f);
            return;
        }
        if (!UN)
        {
            Debug.LogWarning("Username Taken");
            textDisplay.SetActive(true);
            textDisplay.GetComponent<Text>().text = "Username Taken";
            Invoke("wait", 0.5f);
            return;
        }
        // form = (Username + Environment.NewLine + Age);
        // System.IO.File.WriteAllText(@"D:\New folder\TinyHands\Assets\Scripts" + Username + ".txt", form);

        writeNewUser(Username, Int32.Parse(Password));
        username.GetComponent<InputField>().text = "";
        age.GetComponent<InputField>().text = "";
        print("Registration successful");
        SceneManager.LoadScene("Admin login 1");

    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {

        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Debug.Log("helloooooo");
        Debug.Log(args.Snapshot);
        // Do something with the data in args.Snapshot
    }
    private void writeNewUser(string username, int password)
    {

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        Admin admin= new Admin(password);
        string json = JsonUtility.ToJson(admin);

        reference.Child("admin").Child(username).SetRawJsonValueAsync(json);
        // FirebaseDatabase.DefaultInstance
        // .GetReference("users")
        // .ValueChanged += HandleValueChanged;

        return;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                age.GetComponent<InputField>().Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {

            if (Username != "" && Password != "")
            {
                RegisterButtton();
            }

        }

        Username = username.GetComponent<InputField>().text;
        Password = age.GetComponent<InputField>().text;

    }
}

class Admin
{
    public int password;

    public Admin() { }

    public Admin(int password)
    {
        this.password = password;
    }
}
