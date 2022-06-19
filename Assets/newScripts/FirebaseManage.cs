// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using Firebase;
// using Firebase.Database;
// using Firebase.Unity.Editor;


// public class FirebaseManage : MonoBehaviour
// {
//     //   void Start() {
//     //     // Set up the Editor before calling into the realtime database.
//     //     FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://game-9bfc3.firebaseio.com/");

//     //     // Get the root reference location of the database.
//     //     DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

//     //     Debug.Log("dddd");
//     //   }
//     // }

//     void Start()
//     {
//         // Set this before calling into the realtime database.

//         writeNewUser("1", "SDSA", "fgf");
//     }

//     void HandleValueChanged(object sender, ValueChangedEventArgs args)
//     {


//         if (args.DatabaseError != null)
//         {
//             Debug.LogError(args.DatabaseError.Message);
//             return;
//         }
//         Debug.Log("helloooooo");
//         Debug.Log(args.Snapshot);
//         // Do something with the data in args.Snapshot
//     }

//     private void writeNewUser(string userId, string name, string email)
//     {


//         FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://game-9bfc3.firebaseio.com/");
//         DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
//         User user = new User(name, email);
//         string json = JsonUtility.ToJson(user);

//         reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
//         // FirebaseDatabase.DefaultInstance
//         // .GetReference("users")
//         // .ValueChanged += HandleValueChanged;

//         return;
//     }



// }

// class User
// {
//     public string username;
//     public string email;

//     public User()
//     {
//     }

//     public User(string username, string email)
//     {
//         this.username = username;
//         this.email = email;
//     }
// }