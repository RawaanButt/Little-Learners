using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.Networking;

public class LevelManager : MonoBehaviour {
    public GameObject[] objects;

    public GameObject[] objects2;
    public string[] objects3;
    public GameObject[] saveob;
    public int speed;

    public int x;
    public int y;
    public int indicator = 0;
    public Vector2[] inipos;
    public Vector3 initialpos;
    public int dragingObj = -1;
    public int number;
    public string alphabet;
    public int tracker;
    public int q = 0;

    public GameObject self;
    public int scorevar2 = 0;
    public GameObject[] stars;

    public int scorevar_1 = 0;
    public int scorevar_2 = 0;
    public int scorevar_3 = 0;
    public int scorevar_4 = 0;
    public int scorevar_5 = 0;
    public int scorevar_6 = 0;
    public GameObject leftbutton;
    public GameObject up, up1, up2;
    public GameObject down;
    public GameObject down1;
    public GameObject down2;
    public GameObject finaltext;
    public int scorevar;
    public Text newText;
    public int flag;
    public GameObject levelcomanim;
    public Text Stagecomplete;
    public string Stagetext;
    public GameObject ltext;
    public int flaagg;
    public GameObject Gameover;

    //change
    public Stopwatch timer;
    public bool dragLocker;
    private string reposnseTime;
    private string username;
    private int session;
    private int attemptNo;
    private int qh;
    /////
    private string dragDistance;
    public string[] sources;
    public string[] destinations;

    // public Score other;

    public string game = "matching";
    public string gameMode;

    string sSolved = "";
    private string solved = "";
    public GameObject resumePop;

    async void Start () {

        timer = new Stopwatch ();

        if (number == 123) {
            gameMode = "3";
        } else if (alphabet == "abc") {
            gameMode = "4";
        }
        // initialpos = objects2[0].transform.position;
        //change
        qh = 8;
        if (alphabet == "abc") {
            qh = 19;
        }

        if (number == 123) {
            sources[0] = "orange-1";
            sources[1] = "blue-2";
            sources[2] = "purple-3";
            sources[3] = "green-4";
            sources[4] = "seagreen-5";
            sources[5] = "green-6";

            destinations[0] = "orange-1";
            destinations[1] = "blue-1";
            destinations[2] = "purple-3";
            destinations[3] = "grey-3";
            destinations[4] = "seagreen-5";
            destinations[5] = "grey-5";

        } else if (alphabet == "abc") {
            destinations[0] = "purple-a";
            destinations[1] = "grey-a";
            destinations[2] = "blue-c";
            destinations[3] = "grey-c";
            destinations[4] = "yellow-e";
            destinations[5] = "grey-e";

            sources[0] = "purple-a";
            sources[1] = "seagreen-b";
            sources[2] = "blue-c";
            sources[3] = "green-d";
            sources[4] = "yellow-e";
            sources[5] = "red-f";
        }

        dragLocker = true;
        username = File.ReadAllText (Application.dataPath + "/user.txt");
        /////

        dragingObj = -1;
        for (int i = 0; i < 6; i++) {
            inipos[i] = objects[i].transform.position;
        }
        changeobjects ();
        speed = 0;
        // tracker = 100;
        // rb = GetComponent<Rigidbody2D>();

        self.SetActive (false);
        q = 0;

        flag = 0;
        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive (false);
        }

        // //change
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference ("users/" + username + "/matching")
        //     .GetValueAsync ().ContinueWith (task => {
        //         if (task.IsFaulted) {
        //             // Handle the error...
        //         } else if (task.IsCompleted) {

        //             DataSnapshot snapshot = task.Result;
        //             int sess = 0;
        //             if (snapshot.Exists && snapshot.ChildrenCount > 0) {
        //                 // Do something with snapshot...
        //                 foreach (var childSnapshot in snapshot.Children) {
        //                     if (Int32.Parse (childSnapshot.Key.Substring (1)) > sess)
        //                         sess = Int32.Parse (childSnapshot.Key.Substring (1));
        //                 }
        //             }
        //             sess++;
        //             session = sess;

        //         }
        //     });
        // // Debug.Log(session);

        // await FirebaseDatabase.DefaultInstance
        //     .GetReference ("users/" + username + "/matching/s" + session + "/q" + (indicator + 1 + qh))
        //     .GetValueAsync ().ContinueWith (task => {
        //         if (task.IsFaulted) {
        //             // Handle the error...
        //         } else if (task.IsCompleted) {

        //             DataSnapshot snapshot = task.Result;
        //             int att = 0;
        //             if (snapshot.Exists && snapshot.ChildrenCount > 0) {
        //                 // Do something with snapshot...
        //                 foreach (var childSnapshot in snapshot.Children) {
        //                     if (Int32.Parse (childSnapshot.Key.Substring (1)) > att)
        //                         att = Int32.Parse (childSnapshot.Key.Substring (1));
        //                 }
        //             }
        //             att++;
        //             attemptNo = att;

        //         }
        //     });
        // // Debug.Log(attemptNo);

        // timer = new Stopwatch ();
        // timer.Start ();

        /////

    }

    IEnumerator Upload (String user, String game, String gameMode) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("game", game);
        form.AddField ("gameMode", gameMode);

        using (UnityWebRequest www = UnityWebRequest.Post ("https://evening-fortress-14821.herokuapp.com/select", form)) {
            yield return www.SendWebRequest ();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log (www.error);
            } else {
                Debug.Log ("Form upload complete!");
            }
        }
    }
    IEnumerator updateScore (String user, String game, String gameMode, int score, int score1, int score2, int score3, int score4, int score5, int score6, int score7, String solved) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("game", game);
        form.AddField ("gameMode", gameMode);
        form.AddField ("score", score);
        form.AddField ("score1", score1);
        form.AddField ("score2", score2);
        form.AddField ("score3", score3);
        form.AddField ("score4", score4);
        form.AddField ("score5", score5);
        form.AddField ("score6", score6);
        form.AddField ("score7", score7);
        form.AddField ("solved", solved);

        using (UnityWebRequest www = UnityWebRequest.Post ("https://evening-fortress-14821.herokuapp.com/select/score", form)) {
            yield return www.SendWebRequest ();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log (www.error);
            } else {
                Debug.Log ("score upload complete!");
            }
        }
    }
    IEnumerator GetRequest (string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get (uri)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest ();

            string[] pages = uri.Split ('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError) {
                Debug.Log (pages[page] + ": Error: " + webRequest.error);
            } else {
                Resume myObject = JsonUtility.FromJson<Resume> (webRequest.downloadHandler.text);

                if (myObject.solved != null) {
                    sSolved = myObject.solved.Length > 0 ? myObject.solved : sSolved;
                }
                if (myObject.score != null) {
                    scorevar = myObject.score;
                }
                if (myObject.score1 != null) {
                    scorevar2 = myObject.score1;
                    scorevar_1 = myObject.score2;
                    scorevar_2 = myObject.score3;
                    scorevar_3 = myObject.score4;
                    scorevar_4 = myObject.score5;
                    scorevar_5 = myObject.score6;
                    scorevar_6 = myObject.score7;
                }

                if (sSolved.Length > 0) {
                    solved = sSolved;
                    string[] sols = sSolved.Split (',');
                    foreach (string sol in sols) {
                        // record[Int32.Parse(sol)] = true;
                        // dropped[(int)Decimal.Truncate(Int32.Parse(sol) / 5)]++;
                        objects[Int32.Parse (sol)].transform.position = objects2[Int32.Parse (sol)].transform.position;
                        objects2[Int32.Parse (sol)].SetActive (false);

                    }
                }
                GameObject thePlayer4 = GameObject.Find ("LevelManager");
                ObjectRenderer playerScript4 = thePlayer4.GetComponent<ObjectRenderer> ();
                if (alphabet == "abc") {
                    playerScript4.scorevar_4 = scorevar2;

                } else {
                    playerScript4.scorevar_1 = scorevar2;
                }
                playerScript4.datacheck++;
                // initialpos = objects2[0].transform.position;
                // increment ();
                self.SetActive (false);

                // timer = new Stopwatch();
                timer.Start ();
                resumePop.SetActive (false);
                Debug.Log ("five");

            }
        }
    }

    public void onStarting (bool resume, int sess) {
        session = sess;
        if (number == 123) {
            gameMode = "3";
        } else if (alphabet == "abc") {
            gameMode = "4";
        }
        if (resume) {
            StartCoroutine (GetRequest ("https://evening-fortress-14821.herokuapp.com/select/" + username + "/" + game + "/" + gameMode));
        } else {
            Debug.Log ("Float");

            Debug.Log (gameMode);

            StartCoroutine (Upload (username, game, gameMode));

            self.SetActive (false);

            timer = new Stopwatch ();
            timer.Start ();
            resumePop.SetActive (false);
        }
    }

    IEnumerator ShowMessage (float delay) {
        Stagecomplete.text = "" + Stagetext;
        yield return new WaitForSeconds (delay);
        ltext.SetActive (true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds (2f);
        ltext.SetActive (false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }
    void flaag () {
        score ();
        flag = 5;
    }
    void changeobjects () {
        for (int j = 0; j <= 5; j++) {
            objects[j].SetActive (false);
            objects2[j].SetActive (false);

        }
        if (indicator == 0) {
            for (int i = 0; i <= 1; i++) {

                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            }
        } else if (indicator == 1) {
            for (int i = 2; i <= 3; i++) {

                objects[i].SetActive (true);
                objects2[i].SetActive (true);

            }
        } else if (indicator == 2) {
            for (int i = 4; i <= 5; i++) {

                objects[i].SetActive (true);
                objects2[i].SetActive (true);

            }
        }
    }

    async public void leftarrow () {
        //change
        // timer.Restart();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        // .GetReference("users/" + username + "/matching/s" + session + "/q" + (indicator + qh))
        // .GetValueAsync().ContinueWith(task =>
        // {
        //     if (task.IsFaulted)
        //     {
        //         // Handle the error...
        //     }
        //     else if (task.IsCompleted)
        //     {

        //         DataSnapshot snapshot = task.Result;
        //         int att = 0;
        //         if (snapshot.Exists && snapshot.ChildrenCount > 0)
        //         {
        //             // Do something with snapshot...
        //             foreach (var childSnapshot in snapshot.Children)
        //             {
        //                 if (Int32.Parse(childSnapshot.Key.Substring(1)) > att)
        //                     att = Int32.Parse(childSnapshot.Key.Substring(1));
        //             }
        //         }

        //         att++;
        //         attemptNo = att;

        //     }
        // });
        ////
        if (indicator == 0 || scorevar2 == 6) {

            if (number == 123) {
                up.SetActive (true);
                GameObject thePlayer = GameObject.Find ("LevelManager");
                ObjectRenderer playerScript = thePlayer.GetComponent<ObjectRenderer> ();
                if (scorevar_6 == 24) {
                    playerScript.indicator = 4;
                    playerScript.onclickleft ();

                }
                if (scorevar_6 != 24) {
                    playerScript.flaagg++;
                }
                playerScript.scorevar = scorevar;
                playerScript.scorevar_1 = scorevar2;
                playerScript.scorevar_2 = scorevar_1;
                playerScript.scorevar_3 = scorevar_2;
                playerScript.scorevar_4 = scorevar_3;
                playerScript.scorevar_5 = scorevar_4;
                playerScript.restartStopwatch ();
                self.SetActive (false);
                return;
            } else if (alphabet == "abc") {
                if (scorevar_6 == 24 && scorevar_5 == 40 && scorevar_4 < 6) {
                    up1.SetActive (true);
                    GameObject thePlayer3 = GameObject.Find ("Levelmanager");
                    LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
                    playerScript3.scorevar = scorevar;
                    playerScript3.scorevar_1 = scorevar_5;
                    playerScript3.scorevar_2 = scorevar_6;
                    playerScript3.scorevar_3 = scorevar2;
                    playerScript3.scorevar_4 = scorevar_1;
                    playerScript3.scorevar_5 = scorevar_2;
                    playerScript3.scorevar_6 = scorevar_3;
                    playerScript3.flaagg++;
                    playerScript3.restartStopwatch ();
                    self.SetActive (false);
                    // play2.SetActive(false);
                    return;
                } else if (scorevar_6 == 24 && scorevar_5 == 40 && scorevar_4 == 6 && scorevar_3 < 24) {
                    up2.SetActive (true);
                    GameObject thePlayer4 = GameObject.Find ("LevelManager");
                    ObjectRenderer playerScript4 = thePlayer4.GetComponent<ObjectRenderer> ();
                    playerScript4.scorevar = scorevar;
                    playerScript4.scorevar_1 = scorevar_4;
                    playerScript4.scorevar_2 = scorevar_5;
                    playerScript4.scorevar_3 = scorevar_6;
                    playerScript4.scorevar_4 = scorevar2;
                    playerScript4.scorevar_5 = scorevar_1;
                    playerScript4.flaagg++;
                    playerScript4.restartStopwatch ();
                    self.SetActive (false);
                    // play2.SetActive(false);
                    return;
                } else if (scorevar_6 == 24 && scorevar_5 == 40 && scorevar_4 == 6 && scorevar_3 == 24 && scorevar_2 < 40) {
                    up2.SetActive (true);
                    GameObject thePlayer5 = GameObject.Find ("LevelManager");
                    ObjectRenderer playerScript5 = thePlayer5.GetComponent<ObjectRenderer> ();
                    playerScript5.indicator = 4;
                    playerScript5.onclickleft ();
                    playerScript5.scorevar = scorevar;
                    playerScript5.scorevar_1 = scorevar_4;
                    playerScript5.scorevar_2 = scorevar_5;
                    playerScript5.scorevar_3 = scorevar_6;
                    playerScript5.scorevar_4 = scorevar2;
                    playerScript5.scorevar_5 = scorevar_1;
                    playerScript5.restartStopwatch ();
                    self.SetActive (false);
                    // play2.SetActive(false);
                    return;
                }
                up.SetActive (true);
                GameObject thePlayer1 = GameObject.Find ("levelManager");
                ObjectRenderer playerScript1 = thePlayer1.GetComponent<ObjectRenderer> ();
                if (scorevar_6 == 24) {
                    playerScript1.indicator = 4;
                    playerScript1.onclickleft ();
                }
                if (scorevar_6 != 24) {
                    playerScript1.flaagg = 2;
                }
                playerScript1.scorevar = scorevar;
                playerScript1.scorevar_1 = scorevar2;
                playerScript1.scorevar_2 = scorevar_1;
                playerScript1.scorevar_3 = scorevar_2;
                playerScript1.scorevar_4 = scorevar_3;
                playerScript1.scorevar_5 = scorevar_4;
                playerScript1.restartStopwatch ();
                self.SetActive (false);
                return;
            }

        } else {
            if (number == 123) {
                if (scorevar_5 == 40 && scorevar_6 == 24 && indicator == 1) {
                    leftbutton.SetActive (false);
                }
            }
            if (alphabet == "abc") {
                if (scorevar_2 == 40 && scorevar_3 == 24 & scorevar_4 == 6 && scorevar_5 == 40 && scorevar_6 == 24 && indicator == 1) {
                    leftbutton.SetActive (false);
                }
            }
            indicator--;
            changeobjects ();
            q--;
            for (int i = 0; i < objects.Length; i++) {
                if (objects[i].transform.position == objects2[i].transform.position) {
                    objects2[i].SetActive (false);

                }
            }

        }

    }
    public async void restartStopwatch () {
        try {
            timer.Restart ();
        } catch {
            Debug.Log ("re");
        }
        // timer.Restart ();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference ("users/" + username + "/sorting/s" + session + "/q" + (indicator + qh + 1))
        //     .GetValueAsync ().ContinueWith (task => {
        //         if (task.IsFaulted) {
        //             // Handle the error...
        //         } else if (task.IsCompleted) {

        //             DataSnapshot snapshot = task.Result;
        //             int att = 0;
        //             if (snapshot.Exists && snapshot.ChildrenCount > 0) {
        //                 foreach (var childSnapshot in snapshot.Children) {
        //                     if (Int32.Parse (childSnapshot.Key.Substring (1)) > att)
        //                         att = Int32.Parse (childSnapshot.Key.Substring (1));
        //                 }
        //             }
        //             att++;
        //             attemptNo = att;

        //         }
        //     });
    }
    /////////////////////////////////////////
    public void endanim () {
        levelcomanim.SetActive (false);
    }
    public void ddown () {
        if (scorevar_1 == 40 && scorevar_2 == 24 && scorevar_3 < 6) {
            down1.SetActive (true);
            GameObject thePlayer3 = GameObject.Find ("Levelmanager1");
            LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
            if (scorevar_1 == 40 && scorevar_2 == 24 && scorevar_5 == 40 && scorevar_6 == 24) {
                playerScript3.leftbutton.SetActive (false);
            }

            playerScript3.scorevar_1 = scorevar_4;
            playerScript3.scorevar_2 = scorevar_5;
            playerScript3.scorevar_3 = scorevar_6;
            playerScript3.scorevar_4 = scorevar2;
            playerScript3.scorevar_6 = scorevar_2;
            playerScript3.scorevar_5 = scorevar_1;
            playerScript3.flaagg++;
            playerScript3.restartStopwatch ();
            self.SetActive (false);
            return;
        } else if (scorevar_1 == 40 && scorevar_2 == 24 && scorevar_3 == 6 && scorevar_4 < 20) {
            down2.SetActive (true);
            GameObject thePlayer4 = GameObject.Find ("LevelManager1");
            ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
            if (scorevar_1 == 40 && scorevar_2 == 24) {
                playerScript4.leftarrow.SetActive (false);
            }
            playerScript4.scorevar_1 = scorevar_5;
            playerScript4.scorevar_2 = scorevar_6;
            playerScript4.scorevar_3 = scorevar2;
            playerScript4.scorevar_4 = scorevar_1;
            playerScript4.scorevar_5 = scorevar_2;
            playerScript4.scorevar_6 = scorevar_3;
            playerScript4.flaagg++;
            playerScript4.restartStopwatch ();
            self.SetActive (false);
        } else if (scorevar_1 == 40 && scorevar_2 == 24 && scorevar_3 == 6 && scorevar_4 == 20) {
            StartCoroutine (ShowMessage1 (0.5f));
            return;
        }
        down.SetActive (true);
        GameObject thePlayer = GameObject.Find ("levelManager");
        ObjectRenderer playerScript = thePlayer.GetComponent<ObjectRenderer> ();
        if (scorevar_5 == 40 && scorevar_6 == 24) {
            playerScript.leftarrow.SetActive (false);
        } else if (scorevar_1 == 40 && scorevar_2 < 24) {
            playerScript.indicator = 3;
            playerScript.onclickright ();
            if (scorevar_5 == 40 && scorevar_6 == 24) {
                playerScript.leftarrow.SetActive (false);
            }
        }
        playerScript.flag = 5;
        playerScript.flaagg++;
        playerScript.restartStopwatch ();
        self.SetActive (false);
    }
    public void ddown1 () {
        if (scorevar_1 == 20) {
            StartCoroutine (ShowMessage1 (0.5f));
            return;
        }
        down.SetActive (true);
        GameObject thePlayer = GameObject.Find ("LevelManager1");
        ObjectRenderer1 playerScript = thePlayer.GetComponent<ObjectRenderer1> ();
        if (scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 == 6 && scorevar_5 == 40 && scorevar_6 == 24) {
            playerScript.leftarrow.SetActive (false);
        }
        playerScript.flag = 5;
        playerScript.flaagg++;
        playerScript.restartStopwatch ();
        self.SetActive (false);
    }

    async public void rightarrow () {

        //change
        // timer.Restart ();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference ("users/" + username + "/matching/s" + session + "/q" + (indicator + 2 + qh))
        //     .GetValueAsync ().ContinueWith (task => {
        //         if (task.IsFaulted) {
        //             // Handle the error...
        //         } else if (task.IsCompleted) {

        //             DataSnapshot snapshot = task.Result;
        //             int att = 0;
        //             if (snapshot.Exists && snapshot.ChildrenCount > 0) {
        //                 // Do something with snapshot...
        //                 foreach (var childSnapshot in snapshot.Children) {
        //                     if (Int32.Parse (childSnapshot.Key.Substring (1)) > att)
        //                         att = Int32.Parse (childSnapshot.Key.Substring (1));
        //                 }
        //             }

        //             att++;
        //             attemptNo = att;

        //         }
        //     });
        // Debug.Log(attemptNo);

        /////
        // Debug.Log ("i am called");
        // if (objects[indicator * 2].transform.position == initialpos) {
        //     if (indicator * 2 == 2) {
        //         q = 1;
        //         increment ();
        //         Debug.Log ("i am inside you if");

        //     } else if (indicator * 2 == 4) {
        //         q = 2;
        //         increment ();
        //         Debug.Log ("i am inside you if");

        //     } else {
        //         increment ();

        //     }
        //     q = 0;
        // }
        leftbutton.SetActive (true);
        if (indicator == 2) {
            if (number == 123) {
                if (scorevar >= 168) {
                    SettingsPenal.gameover ();
                    Gameover.SetActive (true);
                    Invoke ("endanimation", 6.5f);
                    return;
                }
                if (scorevar2 == 6) {
                    levelcomanim.SetActive (true);
                    SettingsPenal.playanimation ();
                    Invoke ("endanim", 1.5f);
                    Invoke ("ddown", 1.8f);
                    return;
                }
                if (scorevar_1 == 40 && scorevar_2 < 24) {
                    down1.SetActive (true);
                    GameObject thePlayer3 = GameObject.Find ("Levelmanager1");
                    LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
                    playerScript3.scorevar_1 = scorevar_4;
                    playerScript3.scorevar_2 = scorevar_5;
                    playerScript3.scorevar_3 = scorevar_6;
                    playerScript3.scorevar_4 = scorevar2;
                    playerScript3.scorevar_6 = scorevar_2;
                    playerScript3.scorevar_5 = scorevar_1;
                    playerScript3.flaagg++;
                    playerScript3.restartStopwatch ();
                    self.SetActive (false);
                    return;
                } else if (scorevar_1 == 40 && scorevar_2 == 24 && scorevar_3 < 6) {
                    down1.SetActive (true);
                    GameObject thePlayer3 = GameObject.Find ("Levelmanager1");
                    LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
                    playerScript3.scorevar_1 = scorevar_4;
                    playerScript3.scorevar_2 = scorevar_5;
                    playerScript3.scorevar_3 = scorevar_6;
                    playerScript3.scorevar_4 = scorevar2;
                    playerScript3.scorevar_6 = scorevar_2;
                    playerScript3.scorevar_5 = scorevar_1;
                    playerScript3.flaagg++;
                    playerScript3.restartStopwatch ();
                    self.SetActive (false);
                    return;
                } else if (scorevar_1 == 40 && scorevar_2 == 24 && scorevar_3 == 6 && scorevar_4 < 20) {
                    down2.SetActive (true);
                    GameObject thePlayer4 = GameObject.Find ("LevelManager1");
                    ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
                    playerScript4.scorevar_1 = scorevar_5;
                    playerScript4.scorevar_2 = scorevar_6;
                    playerScript4.scorevar_3 = scorevar2;
                    playerScript4.scorevar_4 = scorevar_1;
                    playerScript4.scorevar_5 = scorevar_2;
                    playerScript4.scorevar_6 = scorevar_3;
                    playerScript4.flaagg++;
                    playerScript4.restartStopwatch ();
                    self.SetActive (false);
                } else if (scorevar_1 == 40 && scorevar_2 == 24 && scorevar_3 == 6 && scorevar_4 == 20) {
                    StartCoroutine (ShowMessage1 (0.5f));
                    return;
                }

                down.SetActive (true);
                GameObject thePlayer = GameObject.Find ("levelManager");
                ObjectRenderer playerScript = thePlayer.GetComponent<ObjectRenderer> ();
                if (scorevar_1 == 40 && scorevar_2 < 24) {
                    playerScript.indicator = 3;
                    playerScript.onclickright ();
                }
                playerScript.flag = 5;
                playerScript.flaagg++;
                playerScript.restartStopwatch ();

                self.SetActive (false);
                return;
            }
            if (alphabet == "abc") {
                if (scorevar >= 168) {
                    SettingsPenal.gameover ();
                    Gameover.SetActive (true);
                    Invoke ("endanimation", 6.5f);
                    return;
                }
                if (scorevar2 == 6) {
                    levelcomanim.SetActive (true);
                    SettingsPenal.playanimation ();
                    Invoke ("endanim", 1.5f);
                    Invoke ("ddown1", 1.8f);
                    return;
                }
                if (scorevar_1 == 20) {
                    StartCoroutine (ShowMessage1 (0.5f));
                    return;
                }
                down.SetActive (true);
                GameObject thePlayer = GameObject.Find ("LevelManager1");
                ObjectRenderer1 playerScript = thePlayer.GetComponent<ObjectRenderer1> ();
                playerScript.flag = 5;
                playerScript.flaagg++;
                playerScript.restartStopwatch ();
                self.SetActive (false);
                return;
            }

        } else {
            indicator++;
            changeobjects ();
            q++;
            // rb.velocity = Vector3.zero;
        }
        for (int i = 0; i < objects.Length; i++) {
            if (objects[i].transform.position == objects2[i].transform.position) {
                objects2[i].SetActive (false);

            }
        }
    }
    IEnumerator ShowMessage1 (float delay) {
        yield return new WaitForSeconds (delay);
        finaltext.SetActive (true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds (2f);
        finaltext.SetActive (false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }

    public void Drag (int ob) {
        //change
        if (dragLocker) {
            Vector2 objPos;
            Vector2 mousePos;
            float mousePosY, mousePosX;

            objPos = objects[ob].transform.position; //gets player position
            mousePos = Input.mousePosition; //gets mouse postion
            mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            mousePosX = mousePos.x - objPos.x; //gets the distance between object and mouse position for x
            mousePosY = mousePos.y - objPos.y; //gets the distance between object and mouse position for y  if you want this.
            mousePosX = mousePosX * mousePosX;
            mousePosY = mousePosY * mousePosY;
            var c = mousePosX + mousePosY;
            dragDistance = Mathf.Sqrt (c).ToString ("0.##");

            ////////////////////////////////
            reposnseTime = timer.Elapsed.TotalSeconds.ToString ("0.##");
            timer.Restart ();
            dragLocker = false;
        }
        objects[ob].transform.position = Input.mousePosition;

        dragingObj = ob;
    }
    void increment () {

        // Debug.Log("q:" + q);

        GameObject thePlayer = GameObject.Find (objects3[q]);
        Float playerScript = thePlayer.GetComponent<Float> ();
        if (number == 123) {
            playerScript.tracker = 2;
        } else if (alphabet == "abc") {
            playerScript.tracker = 3;
        }

    }
    void score () {

        up.SetActive (true);
        if (number == 123) {
            GameObject thePlayer = GameObject.Find ("LevelManager");
            ObjectRenderer playerScript = thePlayer.GetComponent<ObjectRenderer> ();
            scorevar = playerScript.scorevar;

            scorevar_1 = playerScript.scorevar_2;
            scorevar_2 = playerScript.scorevar_3;
            scorevar_3 = playerScript.scorevar_4;
            scorevar_4 = playerScript.scorevar_5;
            scorevar_5 = playerScript.scorevar1;
            scorevar_6 = playerScript.scorevar2;

            playerScript.restartStopwatch ();
        } else {
            GameObject thePlayer = GameObject.Find ("levelManager");
            ObjectRenderer playerScript = thePlayer.GetComponent<ObjectRenderer> ();
            scorevar = playerScript.scorevar;

            scorevar_1 = playerScript.scorevar_2;
            scorevar_2 = playerScript.scorevar_3;
            scorevar_3 = playerScript.scorevar_4;
            scorevar_4 = playerScript.scorevar_5;
            scorevar_5 = playerScript.scorevar1;
            scorevar_6 = playerScript.scorevar2;

            playerScript.restartStopwatch ();
        }

        up.SetActive (false);

    }

    public void drop (int ob) {
        // Vector2 objPos;
        // Vector2 mousePos;
        // float mousePosY, mousePosX;

        // objPos = objects2[ob].transform.position; //gets player position
        // mousePos = Input.mousePosition; //gets mouse postion
        // mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        // mousePosX = mousePos.x - objPos.x; //gets the distance between object and mouse position for x
        // mousePosY = mousePos.y - objPos.y; //gets the distance between object and mouse position for y  if you want this.
        // mousePosX = mousePosX * mousePosX;
        // mousePosY = mousePosY * mousePosY;
        // var c = mousePosX + mousePosY;
        // string dropDistance = Mathf.Sqrt(c).ToString("0.##");

        //change
        string dragTime = timer.Elapsed.TotalSeconds.ToString ("0.##");
        dragLocker = true;
        timer.Restart ();
        string status = "wrong";
        /////
        string destination = "-";
        for (int p = indicator * 2; p < ((indicator * 2) + 2); p++) {
            if ((Vector3.Distance (objects[ob].transform.position, objects2[p].transform.position) < 80) && (objects2[p].activeInHierarchy)) {
                destination = destinations[p];
            }
        }

        dragingObj = -1;
        if (ob == 0 || ob == 2 || ob == 4) {
            if (objects[ob].transform.position == objects2[ob].transform.position) {
                Debug.Log ("i am here");
                return;
                // goto abc;
            }

            float Distance = Vector3.Distance (objects[ob].transform.position, objects2[ob].transform.position);
            if (Distance < 80) {
                destination = sources[ob];

                objects[ob].transform.position = objects2[ob].transform.position;
                objects2[ob].SetActive (false);
                // saveob[indicator] = objects[ob];
                initialpos = objects2[ob].transform.position;

                // indicator++;

                scorevar = scorevar + 2;
                scorevar2 = scorevar2 + 2;
                SettingsPenal.playeffect (0);

                // if (indicator == 2)
                // {
                increment ();

                rightarrow ();
                // return;
                // }
                // other.increment();
                // q++;

                changeobjects ();
                //change
                status = "right";
                //////
                if (solved.Length > 0) {
                    solved = solved + "," + ob.ToString ();
                } else {
                    solved = ob.ToString ();
                }
            } else {
                SettingsPenal.playeffect (1);

            }
        }
        //change
        saveAttempt (reposnseTime, dragTime, status, sources[ob], destination);
        /////

        if (objects[ob].transform.localPosition.x > 432 || objects[ob].transform.localPosition.y > 107 || objects[ob].transform.localPosition.x < -454 || objects[ob].transform.localPosition.y < -238) {
            objects[ob].transform.position = inipos[ob];
        }
    }
    public void restartTimer () {
        timer.Restart ();
    }

    ///change
    IEnumerator updateAnalytics (String user, String game, int session, int questionNo, string reposnseTime, string dragTime, string status, string source, string destination) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("game", game);
        form.AddField ("session", session);
        form.AddField ("questionNo", questionNo);
        form.AddField ("reposnseTime", reposnseTime);
        form.AddField ("dragTime", dragTime);
        form.AddField ("status", status);
        form.AddField ("source", source);
        form.AddField ("destination", destination);

        using (UnityWebRequest www = UnityWebRequest.Post ("https://evening-fortress-14821.herokuapp.com/analytics", form)) {
            yield return www.SendWebRequest ();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log (www.error);
            } else {
                Debug.Log ("attemmpt upload complete!");
            }
        }
    }
    async void saveAttempt (string reposnseTime, string dragTime, string status, string source, string destination) {

        // Debug.Log("in funtionn");
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // AtemptMatchingFloat atempt = new AtemptMatchingFloat(reposnseTime, dragTime, status, source, destination);
        // string json = JsonUtility.ToJson(atempt);
        // Debug.Log("qh");
        // Debug.Log(qh);

        // reference.Child("users").Child(username).Child("matching").Child("s" + session).Child("q" + (indicator + 1 + qh)).Child("a" + attemptNo).SetRawJsonValueAsync(json);
        // attemptNo++;
        int questionNo = indicator + 1 + qh;
        StartCoroutine (updateAnalytics (username, game, session, questionNo, reposnseTime, dragTime, status, source, destination));

        if (status == "right") {
            StartCoroutine (updateScore (username, game, gameMode, scorevar, scorevar2, scorevar_1, scorevar_2, scorevar_3, scorevar_4, scorevar_5, scorevar_6, solved));
        }
    }
    /////

    void Update () {
        if (number == 123) {
            if (flag == 2) {
                flaag ();
            }

        }
        if (flaagg == 1) {

            StartCoroutine (ShowMessage (0.5f));
            flaagg--;
        }
        if (alphabet == "abc") {
            if (flag == 4) {
                flaag ();
            }
        }

        if (dragingObj != -1) {
            objects[dragingObj].transform.position = Input.mousePosition;
        }
        newText.text = "" + scorevar;

        if (scorevar >= 42) {
            stars[0].SetActive (true);
        }
        if (scorevar >= 84) {
            stars[1].SetActive (true);
        }
        if (scorevar >= 126) {
            stars[2].SetActive (true);
        }
        if (scorevar >= 168) {
            stars[3].SetActive (true);
        }

    }
}

class AtemptMatchingFloat {
    public string reposnseTime;
    public string dragTime;
    public string status;
    // public string dragDistance;
    // public string dropDistance;
    public string source;
    public string destination;
    public string timeStamp;

    public AtemptMatchingFloat () { }

    public AtemptMatchingFloat (string reposnseTime, string dragTime, string status, string source, string destination) {
        this.reposnseTime = reposnseTime;
        this.dragTime = dragTime;
        this.status = status;
        // this.dragDistance = dragDistance;
        // this.dropDistance = dropDistance;
        this.source = source;
        this.destination = destination;
        this.timeStamp = DateTime.Now.ToString ("MM/dd/yyyy HH:mm:ss");

    }

    // public AtemptMatchingFloat(string reposnseTime, string dragTime, string status)
    // {
    //     this.reposnseTime = reposnseTime;
    //     this.dragTime = dragTime;
    //     this.status = status;
    // }
}