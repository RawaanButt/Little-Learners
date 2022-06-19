using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.Networking;

public class ObjectRenderer : MonoBehaviour {
    public GameObject float1;
    public GameObject float2;

    public GameObject[] cards;
    public GameObject[] cards2;
    public GameObject[] objects;
    public GameObject[] objects2;
    public Vector2[] inipos;
    public int indicator;
    public static System.Random r = new System.Random ();
    public int var;
    public GameObject[] stars;
    public int number;
    public string alphabet;
    public int[] dropped;

    public GameObject self;
    public GameObject finaltext;
    public GameObject up, up1;
    public GameObject down;
    public GameObject down1;
    public GameObject down2;
    public GameObject down3;

    public GameObject rightarrow;
    public GameObject leftarrow;

    public int scorevar = 0;
    public int scorevar2 = 0;
    public int scorevar1 = 0;

    public int scorevar_1 = 0;
    public int scorevar_2 = 0;
    public int scorevar_3 = 0;
    public int scorevar_4 = 0;
    public int scorevar_5 = 0;

    public Text newText;
    public int flag;
    public int flaagg;
    public int anf;
    public int a;
    public GameObject levelcomanim;
    public GameObject Gameover;
    public Text Stagecomplete;
    public string[] Stagetext;
    public GameObject ltext;

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

    public string game = "matching";
    public string gameMode;

    string sPostions = "";
    string sPostions1 = "";
    string sSolved = "";
    private string solved = "";
    private string postions = "";
    private string postions1 = "";
    public GameObject resumePop;

    async void Start () {
        timer = new Stopwatch ();

        if (number == 123) {
            gameMode = "1";
        } else if (alphabet == "abc") {
            gameMode = "2";
        }

        qh = 0;
        if (alphabet == "abc") {
            qh = 11;
        }

        if (number == 123) {
            sources[0] = "1";
            sources[1] = "2";
            sources[2] = "3";
            sources[3] = "4";
            sources[4] = "5";
            sources[5] = "6";
            sources[6] = "7";
            sources[7] = "8";
            sources[8] = "9";
            sources[9] = "10";
            sources[10] = "1";
            sources[11] = "2";
            sources[12] = "3";
            sources[13] = "4";
            sources[14] = "5";
            sources[15] = "6";
            sources[16] = "7";
            sources[17] = "8";
            sources[18] = "9";
            sources[19] = "10";
            sources[20] = "1";
            sources[21] = "2";
            sources[22] = "3";
            sources[23] = "4";
            sources[24] = "5";
            sources[25] = "6";
            sources[26] = "7";
            sources[27] = "8";
            sources[28] = "9";
            sources[29] = "10";
            sources[30] = "1";
            sources[31] = "2";
            sources[32] = "3";
            sources[33] = "4";
            sources[34] = "5";
            sources[35] = "6";
            sources[36] = "7";
            sources[37] = "8";
            sources[38] = "9";
            sources[39] = "10";
        } else if (alphabet == "abc") {
            sources[0] = "a";
            sources[1] = "b";
            sources[2] = "c";
            sources[3] = "d";
            sources[4] = "e";
            sources[5] = "f";
            sources[6] = "g";
            sources[7] = "h";
            sources[8] = "i";
            sources[9] = "j";
            sources[10] = "a";
            sources[11] = "b";
            sources[12] = "c";
            sources[13] = "d";
            sources[14] = "e";
            sources[15] = "f";
            sources[16] = "g";
            sources[17] = "h";
            sources[18] = "i";
            sources[19] = "j";
            sources[20] = "a";
            sources[21] = "b";
            sources[22] = "c";
            sources[23] = "d";
            sources[24] = "e";
            sources[25] = "f";
            sources[26] = "g";
            sources[27] = "h";
            sources[28] = "i";
            sources[29] = "j";
            sources[30] = "a";
            sources[31] = "b";
            sources[32] = "c";
            sources[33] = "d";
            sources[34] = "e";
            sources[35] = "f";
            sources[36] = "g";
            sources[37] = "h";
            sources[38] = "i";
            sources[39] = "j";
        }

        dragLocker = true;
        username = File.ReadAllText (Application.dataPath + "/user.txt");
        /////

        indicator = 0;
        render ();
        randomize ();

        setInitialPosition ();
        var = -5;
        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive (false);
        }

        // other = new Score();
        // if (alphabet == "abc") {
        //     self.SetActive (false);
        // }
        scorevar = 0;
        //change
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
    public void jump () {
        if (scorevar1 < 40) {
            indicator = 0;
        } else if (scorevar1 == 40 && scorevar2 < 24) {
            indicator = 4;
            setInitialPosition ();
            render ();
            leftarrow.SetActive (false);
        } else if (scorevar1 == 40 && scorevar2 == 24 && scorevar_1 < 6) {
            down.SetActive (true);
            GameObject thePlayer = GameObject.Find ("Levelmanager");
            LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
            playerScript.leftbutton.SetActive (false);
            playerScript.flag = 2;
            playerScript.flaagg++;
            playerScript.restartStopwatch ();
            self.SetActive (false);

        } else if (scorevar1 == 40 && scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 < 40) {
            down1.SetActive (true);
            GameObject thePlayer1 = GameObject.Find ("levelManager");
            ObjectRenderer playerScript1 = thePlayer1.GetComponent<ObjectRenderer> ();
            playerScript1.leftarrow.SetActive (false);
            playerScript1.scorevar_3 = scorevar1;
            playerScript1.scorevar_4 = scorevar2;
            playerScript1.scorevar_5 = scorevar_1;
            playerScript1.scorevar_1 = scorevar_4;
            playerScript1.scorevar_2 = scorevar_5;
            playerScript1.flaagg++;
            playerScript1.restartStopwatch ();
            self.SetActive (false);
        } else if (scorevar1 == 40 && scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 < 24) {
            down1.SetActive (true);
            GameObject thePlayer2 = GameObject.Find ("levelManager");
            ObjectRenderer playerScript2 = thePlayer2.GetComponent<ObjectRenderer> ();
            playerScript2.leftarrow.SetActive (false);
            playerScript2.indicator = 4;
            playerScript2.setInitialPosition ();
            playerScript2.render ();
            playerScript2.scorevar_3 = scorevar1;
            playerScript2.scorevar_4 = scorevar2;
            playerScript2.scorevar_5 = scorevar_1;
            playerScript2.scorevar_1 = scorevar_4;
            playerScript2.scorevar_2 = scorevar_5;
            playerScript2.flaagg++;
            playerScript2.restartStopwatch ();
            self.SetActive (false);
        } else if (scorevar1 == 40 && scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 < 6) {
            down2.SetActive (true);
            GameObject thePlayer3 = GameObject.Find ("LevelManager");
            LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
            playerScript3.leftbutton.SetActive (false);
            playerScript3.scorevar_1 = scorevar_5;
            playerScript3.scorevar_2 = scorevar1;
            playerScript3.scorevar_3 = scorevar2;
            playerScript3.scorevar_4 = scorevar_1;
            playerScript3.scorevar_6 = scorevar_3;
            playerScript3.scorevar_5 = scorevar_2;
            playerScript3.flaagg++;
            playerScript3.restartStopwatch ();
            self.SetActive (false);
        } else if (scorevar1 == 40 && scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 == 6 && scorevar_5 < 20) {
            down3.SetActive (true);
            GameObject thePlayer4 = GameObject.Find ("LevelManager1");
            ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
            playerScript4.leftarrow.SetActive (false);
            playerScript4.scorevar_1 = scorevar1;
            playerScript4.scorevar_2 = scorevar2;
            playerScript4.scorevar_3 = scorevar_1;
            playerScript4.scorevar_4 = scorevar_2;
            playerScript4.scorevar_5 = scorevar_3;
            playerScript4.scorevar_6 = scorevar_4;
            playerScript4.flaagg++;
            playerScript4.restartStopwatch ();
            self.SetActive (false);
        }

        // self.SetActive (false);
        // change
    }

    IEnumerator Upload (String user, String game, String gameMode, String positions, String positions1) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("game", game);
        form.AddField ("gameMode", gameMode);
        form.AddField ("positions", positions);
        form.AddField ("positions1", positions1);

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
    public int datacheck;
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

                // scorevar = sv;
                // scorevar1 = sv2;
                // scorevar2 = sv3;
                // scorevar3 = sv4;

                sPostions = myObject.positions;
                sPostions1 = myObject.positions1;

                if (myObject.solved != null) {
                    sSolved = myObject.solved.Length > 0 ? myObject.solved : sSolved;
                }
                if (myObject.score != null) {
                    scorevar = myObject.score;
                }
                if (myObject.score1 != null) {
                    scorevar1 = myObject.score1;
                    scorevar2 = myObject.score2;
                    scorevar_1 = myObject.score3;
                    scorevar_2 = myObject.score4;
                    scorevar_3 = myObject.score5;
                    scorevar_4 = myObject.score6;
                    scorevar_5 = myObject.score7;
                }

                if (sPostions.Length > 0) {
                    string[] posPairs = sPostions.Split (',');
                    foreach (string posPair in posPairs) {
                        string[] post = posPair.Split ('-');
                        objects[Int32.Parse (post[0])].transform.position = cards[Int32.Parse (post[1])].transform.position;
                    }
                }
                if (sPostions1.Length > 0) {
                    string[] posPairs1 = sPostions1.Split (',');
                    foreach (string posPair1 in posPairs1) {
                        string[] post1 = posPair1.Split ('-');
                        objects2[Int32.Parse (post1[0])].transform.position = cards2[Int32.Parse (post1[1])].transform.position;
                    }
                }
                if (sSolved.Length > 0) {
                    solved = sSolved;
                    string[] sols = sSolved.Split (',');
                    foreach (string sol in sols) {
                        // record[Int32.Parse(sol)] = true;
                        dropped[(int) Decimal.Truncate (Int32.Parse (sol) / 5)]++;
                        objects[Int32.Parse (sol)].transform.position = objects2[Int32.Parse (sol)].transform.position;
                        objects2[Int32.Parse (sol)].SetActive (false);

                    }
                }
                if (alphabet == "abc") {
                    GameObject thePlayer4 = GameObject.Find ("LevelManager");
                    ObjectRenderer playerScript4 = thePlayer4.GetComponent<ObjectRenderer> ();
                    playerScript4.scorevar_2 = scorevar1;
                    playerScript4.scorevar_3 = scorevar2;
                    playerScript4.datacheck++;
                    setInitialPosition ();
                    render ();
                    self.SetActive (false);
                }
                // timer = new Stopwatch();
                timer.Start ();
                if (alphabet == "abc") {
                    resumePop.SetActive (false);
                } else {
                    datacheck++;
                }

                if (alphabet != "abc") {
                    float1.SetActive (true);
                    float2.SetActive (true);
                    Scene currentScene = SceneManager.GetActiveScene ();
                    string sceneName = currentScene.name;
                    if (sceneName == "New" || sceneName == "NewMultiplayer") {
                        if (GameObject.Find ("Levelmanager")) {
                            GameObject thePlayer = GameObject.Find ("Levelmanager");
                            LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
                            playerScript.onStarting (true, session);
                        }

                        if (GameObject.Find ("Levelmanager1")) {
                            GameObject thePlayer = GameObject.Find ("Levelmanager1");
                            LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
                            playerScript.onStarting (true, session);
                        }

                    }
                }
                Debug.Log ("five");

            }
        }
    }

    public void onStarting (bool resume, int sess) {
        session = sess;

        if (number == 123) {
            gameMode = "1";
        }
        if (resume) {
            StartCoroutine (GetRequest ("https://evening-fortress-14821.herokuapp.com/select/" + username + "/" + game + "/" + gameMode));
        } else {
            Debug.Log ("Match");

            Debug.Log (gameMode);
            StartCoroutine (Upload (username, game, gameMode, postions, postions1));

            if (alphabet == "abc") {
                self.SetActive (false);
            }
            // timer = new Stopwatch();
            timer.Start ();
            resumePop.SetActive (false);
            if (alphabet != "abc") {
                float1.SetActive (true);
                float2.SetActive (true);
                Scene currentScene = SceneManager.GetActiveScene ();
                string sceneName = currentScene.name;
                if (sceneName == "New" || sceneName == "NewMultiplayer" ) {

                    if (GameObject.Find ("Levelmanager")) {

                        GameObject thePlayer = GameObject.Find ("Levelmanager");
                        LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
                        playerScript.onStarting (resume, session);

                    }

                    if (GameObject.Find ("Levelmanager1")) {
                        GameObject thePlayer = GameObject.Find ("Levelmanager1");
                        LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
                        playerScript.onStarting (resume, session);

                    }

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

    IEnumerator ShowMessage (int number, float delay) {
        Stagecomplete.text = "" + Stagetext[number];
        yield return new WaitForSeconds (delay);
        ltext.SetActive (true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds (2f);
        ltext.SetActive (false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }

    public void Increment () {
        scorevar = scorevar + 2;
        // scorevar2 = scorevar2 + 2;
        if (indicator >= 0 && indicator <= 3) {
            scorevar1 = scorevar1 + 2;
        } else if (indicator >= 4 && indicator <= 7) {
            scorevar2 = scorevar2 + 2;
        }

    }
    public void setInitialPosition () {
        int j = 0;
        int x = 0;
        x = indicator;
        for (int i = x * 5; i < x * 5 + 5; i++) {
            inipos[j] = objects[i].transform.position;
            j++;
        }
        j = 0;
    }
    public void randomize () {
        for (int i = 0; i < 40; i += 5) {

            List<int> termsList = new List<int> ();
            List<int> termsList2 = new List<int> ();

            for (int j = 0; j < 5; j++) {

                int pos = random_except_list (5, termsList.ToArray ());
                termsList.Add (pos);
                termsList.Sort ();
                objects[i + j].transform.position = cards[pos].transform.position;

                if (postions.Length > 0) {
                    postions = postions + "," + (i + j).ToString () + "-" + pos.ToString ();
                } else {
                    postions = (i + j).ToString () + "-" + pos.ToString ();
                }
                int pos2 = random_except_list (5, termsList2.ToArray ());
                termsList2.Add (pos2);
                termsList2.Sort ();
                objects2[i + j].transform.position = cards2[pos2].transform.position;

                if (postions1.Length > 0) {
                    postions1 = postions1 + "," + (i + j).ToString () + "-" + pos2.ToString ();
                } else {
                    postions1 = (i + j).ToString () + "-" + pos2.ToString ();
                }
            }
        }
    }

    /////////////////////////Level Complete animation/////////////////////////////////////////
    public void endanim () {
        levelcomanim.SetActive (false);
    }
    public void ddown () {
        if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 < 40) {
            down1.SetActive (true);
            GameObject thePlayer1 = GameObject.Find ("levelManager");
            ObjectRenderer playerScript1 = thePlayer1.GetComponent<ObjectRenderer> ();
            if (scorevar1 == 40) {
                playerScript1.leftarrow.SetActive (false);
            }
            playerScript1.scorevar_3 = scorevar1;
            playerScript1.scorevar_4 = scorevar2;
            playerScript1.scorevar_5 = scorevar_1;
            playerScript1.scorevar_1 = scorevar_4;
            playerScript1.scorevar_2 = scorevar_5;
            playerScript1.flaagg++;
            playerScript1.restartStopwatch ();
            self.SetActive (false);
            return;
        } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 < 24) {
            down1.SetActive (true);
            GameObject thePlayer2 = GameObject.Find ("levelManager");
            ObjectRenderer playerScript2 = thePlayer2.GetComponent<ObjectRenderer> ();
            if (scorevar1 == 40) {
                playerScript2.leftarrow.SetActive (false);
            }
            playerScript2.indicator = 3;
            playerScript2.onclickright ();
            playerScript2.scorevar_3 = scorevar1;
            playerScript2.scorevar_4 = scorevar2;
            playerScript2.scorevar_5 = scorevar_1;
            playerScript2.scorevar_1 = scorevar_4;
            playerScript2.scorevar_2 = scorevar_5;
            playerScript2.flaagg++;
            playerScript2.restartStopwatch ();
            self.SetActive (false);
            return;
        } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 < 6) {
            down2.SetActive (true);
            GameObject thePlayer3 = GameObject.Find ("Levelmanager1");
            LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
            if (scorevar1 == 40) {
                playerScript3.leftbutton.SetActive (false);
            }
            playerScript3.scorevar_1 = scorevar_5;
            playerScript3.scorevar_2 = scorevar1;
            playerScript3.scorevar_3 = scorevar2;
            playerScript3.scorevar_4 = scorevar_1;
            playerScript3.scorevar_6 = scorevar_3;
            playerScript3.scorevar_5 = scorevar_2;
            playerScript3.flaagg++;
            playerScript3.restartStopwatch ();
            self.SetActive (false);
            return;

        } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 == 6 && scorevar_5 < 20) {
            down3.SetActive (true);
            GameObject thePlayer4 = GameObject.Find ("LevelManager1");
            ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
            if (scorevar1 == 40) {
                playerScript4.leftarrow.SetActive (false);
            }
            playerScript4.scorevar_1 = scorevar1;
            playerScript4.scorevar_2 = scorevar2;
            playerScript4.scorevar_3 = scorevar_1;
            playerScript4.scorevar_4 = scorevar_2;
            playerScript4.scorevar_5 = scorevar_3;
            playerScript4.scorevar_6 = scorevar_4;
            playerScript4.flaagg++;
            playerScript4.restartStopwatch ();
            self.SetActive (false);
            return;

        }
        down.SetActive (true);
        GameObject thePlayer = GameObject.Find ("Levelmanager");
        LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
        if (scorevar1 == 40) {
            playerScript.leftbutton.SetActive (false);
        }
        playerScript.flag = 2;
        playerScript.flaagg++;
        playerScript.restartStopwatch ();

        self.SetActive (false);
    }
    public void ddown1 () {
        if (scorevar_1 == 6 && scorevar_2 < 20) {
            down3.SetActive (true);
            GameObject thePlayer4 = GameObject.Find ("LevelManager1");
            ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
            if (scorevar1 == 40 && scorevar_3 == 40 && scorevar_4 == 24 && scorevar_5 == 6) {
                playerScript4.leftarrow.SetActive (false);
            }
            playerScript4.scorevar_1 = scorevar_3;
            playerScript4.scorevar_2 = scorevar_4;
            playerScript4.scorevar_3 = scorevar_5;
            playerScript4.scorevar_4 = scorevar1;
            playerScript4.scorevar_5 = scorevar2;
            playerScript4.scorevar_6 = scorevar_1;
            playerScript4.flaagg++;
            playerScript4.restartStopwatch ();
            self.SetActive (false);
            return;
        } else if (scorevar_1 == 6 && scorevar_2 == 20) {
            StartCoroutine (ShowMessage1 (0.5f));
            return;
        }
        down.SetActive (true);
        GameObject thePlayer = GameObject.Find ("Levelmanager1");
        LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
        if (scorevar1 == 40 && scorevar_3 == 40 && scorevar_4 == 24 && scorevar_5 == 6) {
            playerScript.leftbutton.SetActive (false);
        }
        playerScript.flag = 4;
        playerScript.flaagg++;
        playerScript.restartStopwatch ();

        self.SetActive (false);
    }
    public void endanimation () {
        // Gameover.SetActive (false);
        SceneManager.LoadScene ("Game Selection");
    }
    async public void onclickright () {
        //change
        // timer.Restart();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/matching/s" + session + "/q" + (indicator + 2 + qh))
        //     .GetValueAsync().ContinueWith(task =>
        //     {
        //         if (task.IsFaulted)
        //         {
        //             // Handle the error...
        //         }
        //         else if (task.IsCompleted)
        //         {

        //             DataSnapshot snapshot = task.Result;
        //             int att = 0;
        //             if (snapshot.Exists && snapshot.ChildrenCount > 0)
        //             {
        //                 // Do something with snapshot...
        //                 foreach (var childSnapshot in snapshot.Children)
        //                 {
        //                     if (Int32.Parse(childSnapshot.Key.Substring(1)) > att)
        //                         att = Int32.Parse(childSnapshot.Key.Substring(1));
        //                 }
        //             }

        //             att++;
        //             attemptNo = att;

        //         }
        //     });
        // Debug.Log(attemptNo);
        /////
        if (DOTween.TotalPlayingTweens () == 0) {
            leftarrow.SetActive (true);
            if (number == 123) {
                if (scorevar >= 160) {
                    SettingsPenal.gameover ();
                    Gameover.SetActive (true);
                    Invoke ("endanimation", 6.5f);
                    return;

                }
                if (indicator >= 4 && scorevar2 == 24) {
                    levelcomanim.SetActive (true);
                    SettingsPenal.playanimation ();
                    Invoke ("endanim", 3f);
                    Invoke ("ddown", 3.3f);
                    return;
                }
                if (indicator == 7) {
                    if (scorevar_1 == 6 && scorevar_2 < 40) {
                        down1.SetActive (true);
                        GameObject thePlayer1 = GameObject.Find ("levelManager");
                        ObjectRenderer playerScript1 = thePlayer1.GetComponent<ObjectRenderer> ();
                        playerScript1.scorevar_3 = scorevar1;
                        playerScript1.scorevar_4 = scorevar2;
                        playerScript1.scorevar_5 = scorevar_1;
                        playerScript1.scorevar_1 = scorevar_4;
                        playerScript1.scorevar_2 = scorevar_5;
                        playerScript1.flaagg++;
                        playerScript1.restartStopwatch ();
                        self.SetActive (false);
                        return;
                    } else if (scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 < 24) {
                        down1.SetActive (true);
                        GameObject thePlayer2 = GameObject.Find ("levelManager");
                        ObjectRenderer playerScript2 = thePlayer2.GetComponent<ObjectRenderer> ();
                        playerScript2.indicator = 3;
                        playerScript2.onclickright ();
                        playerScript2.scorevar_3 = scorevar1;
                        playerScript2.scorevar_4 = scorevar2;
                        playerScript2.scorevar_5 = scorevar_1;
                        playerScript2.scorevar_1 = scorevar_4;
                        playerScript2.scorevar_2 = scorevar_5;
                        playerScript2.flaagg++;
                        playerScript2.restartStopwatch ();
                        self.SetActive (false);
                        return;
                    } else if (scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 < 6) {
                        down2.SetActive (true);
                        GameObject thePlayer3 = GameObject.Find ("Levelmanager1");
                        LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
                        playerScript3.scorevar_1 = scorevar_5;
                        playerScript3.scorevar_2 = scorevar1;
                        playerScript3.scorevar_3 = scorevar2;
                        playerScript3.scorevar_4 = scorevar_1;
                        playerScript3.scorevar_6 = scorevar_3;
                        playerScript3.scorevar_5 = scorevar_2;
                        playerScript3.flaagg++;
                        playerScript3.restartStopwatch ();
                        self.SetActive (false);
                        return;

                    } else if (scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 == 6 && scorevar_5 < 20) {
                        down3.SetActive (true);
                        GameObject thePlayer4 = GameObject.Find ("LevelManager1");
                        ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
                        playerScript4.scorevar_1 = scorevar1;
                        playerScript4.scorevar_2 = scorevar2;
                        playerScript4.scorevar_3 = scorevar_1;
                        playerScript4.scorevar_4 = scorevar_2;
                        playerScript4.scorevar_5 = scorevar_3;
                        playerScript4.scorevar_6 = scorevar_4;
                        playerScript4.flaagg++;
                        playerScript4.restartStopwatch ();
                        self.SetActive (false);
                        return;

                    } else if (scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 == 6 && scorevar_5 == 20) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }
                    down.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("Levelmanager");
                    LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();

                    playerScript.flag = 2;
                    playerScript.flaagg++;
                    playerScript.restartStopwatch ();
                    self.SetActive (false);
                    return;
                }
            }

            if (alphabet == "abc") {
                if (scorevar >= 160) {
                    SettingsPenal.gameover ();
                    Gameover.SetActive (true);
                    Invoke ("endanimation", 6.5f);
                    return;
                }
                if (indicator >= 4 && scorevar2 == 24) {
                    levelcomanim.SetActive (true);
                    SettingsPenal.playanimation ();
                    Invoke ("endanim", 3f);
                    Invoke ("ddown1", 3.3f);
                    return;
                }
                if (indicator == 7) {
                    if (scorevar_1 == 6 && scorevar_2 < 20) {
                        down3.SetActive (true);
                        GameObject thePlayer4 = GameObject.Find ("LevelManager1");
                        ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
                        playerScript4.scorevar_1 = scorevar_3;
                        playerScript4.scorevar_2 = scorevar_4;
                        playerScript4.scorevar_3 = scorevar_5;
                        playerScript4.scorevar_4 = scorevar1;
                        playerScript4.scorevar_5 = scorevar2;
                        playerScript4.scorevar_6 = scorevar_1;
                        playerScript4.flaagg++;
                        playerScript4.restartStopwatch ();
                        self.SetActive (false);
                        return;
                    } else if (scorevar_1 == 6 && scorevar_2 == 20) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }
                    down.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("Levelmanager1");
                    LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
                    playerScript.flag = 4;
                    playerScript.flaagg++;
                    playerScript.restartStopwatch ();
                    self.SetActive (false);
                    return;
                }
            }

            // dropped = 0;
            anf = 90;
            a = -1;
            if (scorevar1 == 40 && indicator <= 3) {
                levelcomanim.SetActive (true);
                SettingsPenal.playanimation ();
                StartCoroutine (wait ());
                Debug.Log (indicator);

                return;
            }
            if (indicator == 3) {
                if (number == 123) {

                    if (scorevar2 == 24 && scorevar_1 < 6) {
                        down.SetActive (true);
                        GameObject thePlayer = GameObject.Find ("Levelmanager");
                        LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
                        playerScript.flag = 2;
                        playerScript.flaagg++;
                        playerScript.restartStopwatch ();
                        self.SetActive (false);
                        return;
                    } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 < 40) {
                        down1.SetActive (true);
                        GameObject thePlayer1 = GameObject.Find ("levelManager");
                        ObjectRenderer playerScript1 = thePlayer1.GetComponent<ObjectRenderer> ();
                        playerScript1.scorevar_3 = scorevar1;
                        playerScript1.scorevar_4 = scorevar2;
                        playerScript1.scorevar_5 = scorevar_1;
                        playerScript1.scorevar_1 = scorevar_4;
                        playerScript1.scorevar_2 = scorevar_5;
                        playerScript1.flaagg++;
                        playerScript1.restartStopwatch ();
                        self.SetActive (false);
                        return;
                    } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 < 24) {
                        down1.SetActive (true);
                        GameObject thePlayer2 = GameObject.Find ("levelManager");
                        ObjectRenderer playerScript2 = thePlayer2.GetComponent<ObjectRenderer> ();
                        playerScript2.indicator = 3;
                        playerScript2.onclickright ();
                        playerScript2.scorevar_3 = scorevar1;
                        playerScript2.scorevar_4 = scorevar2;
                        playerScript2.scorevar_5 = scorevar_1;
                        playerScript2.scorevar_1 = scorevar_4;
                        playerScript2.scorevar_2 = scorevar_5;
                        playerScript2.flaagg++;
                        playerScript2.restartStopwatch ();
                        self.SetActive (false);
                        return;
                    } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 < 6) {
                        down2.SetActive (true);
                        GameObject thePlayer3 = GameObject.Find ("Levelmanager1");
                        LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
                        playerScript3.scorevar_1 = scorevar_5;
                        playerScript3.scorevar_2 = scorevar1;
                        playerScript3.scorevar_3 = scorevar2;
                        playerScript3.scorevar_4 = scorevar_1;
                        playerScript3.scorevar_6 = scorevar_3;
                        playerScript3.scorevar_5 = scorevar_2;
                        playerScript3.flaagg++;
                        playerScript3.restartStopwatch ();
                        self.SetActive (false);
                        return;

                    } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 == 6 && scorevar_5 < 20) {
                        down3.SetActive (true);
                        GameObject thePlayer4 = GameObject.Find ("LevelManager1");
                        ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
                        playerScript4.scorevar_1 = scorevar1;
                        playerScript4.scorevar_2 = scorevar2;
                        playerScript4.scorevar_3 = scorevar_1;
                        playerScript4.scorevar_4 = scorevar_2;
                        playerScript4.scorevar_5 = scorevar_3;
                        playerScript4.scorevar_6 = scorevar_4;
                        playerScript4.flaagg++;
                        playerScript4.restartStopwatch ();
                        self.SetActive (false);
                        return;

                    } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 == 6 && scorevar_5 == 20) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }
                }
                if (alphabet == "abc") {
                    if (scorevar2 == 24 && scorevar_1 < 6) {
                        down.SetActive (true);
                        GameObject thePlayer3 = GameObject.Find ("Levelmanager1");
                        LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
                        playerScript3.scorevar_1 = scorevar_2;
                        playerScript3.scorevar_2 = scorevar_3;
                        playerScript3.scorevar_3 = scorevar_4;
                        playerScript3.scorevar_4 = scorevar_5;
                        playerScript3.scorevar_6 = scorevar2;
                        playerScript3.scorevar_5 = scorevar1;
                        playerScript3.flaagg++;
                        playerScript3.restartStopwatch ();
                        self.SetActive (false);
                        return;
                    } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 < 20) {
                        down1.SetActive (true);
                        GameObject thePlayer4 = GameObject.Find ("LevelManager1");
                        ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
                        playerScript4.scorevar_1 = scorevar_3;
                        playerScript4.scorevar_2 = scorevar_4;
                        playerScript4.scorevar_3 = scorevar_5;
                        playerScript4.scorevar_4 = scorevar1;
                        playerScript4.scorevar_5 = scorevar2;
                        playerScript4.scorevar_6 = scorevar_1;
                        playerScript4.flaagg++;
                        playerScript4.restartStopwatch ();
                        self.SetActive (false);
                        return;
                    } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 20) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }
                }
                StartCoroutine (ShowMessage (0, 0.5f));
            }
            animation ();

            indicator++;

            // render ();
            Invoke ("render", 0.5f);
            setInitialPosition ();
            Invoke ("clearob2", 0.7f);
        }
    }
    IEnumerator wait () {
        yield return new WaitForSeconds (3);
        endanim ();
        if (number == 123) {
            if (scorevar1 == 40 && scorevar2 < 24) {
                indicator = 3;

            }

            if (scorevar2 == 24 && scorevar_1 < 6) {
                down.SetActive (true);
                GameObject thePlayer = GameObject.Find ("Levelmanager");
                LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
                playerScript.leftbutton.SetActive (false);
                playerScript.flag = 2;
                playerScript.flaagg++;
                playerScript.restartStopwatch ();
                self.SetActive (false);
                yield break;
            } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 < 40) {
                down1.SetActive (true);
                GameObject thePlayer1 = GameObject.Find ("levelManager");
                ObjectRenderer playerScript1 = thePlayer1.GetComponent<ObjectRenderer> ();
                playerScript1.leftarrow.SetActive (false);
                playerScript1.scorevar_3 = scorevar1;
                playerScript1.scorevar_4 = scorevar2;
                playerScript1.scorevar_5 = scorevar_1;
                playerScript1.scorevar_1 = scorevar_4;
                playerScript1.scorevar_2 = scorevar_5;
                playerScript1.flaagg++;
                playerScript1.restartStopwatch ();
                self.SetActive (false);
                yield break;
            } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 < 24) {
                down1.SetActive (true);
                GameObject thePlayer2 = GameObject.Find ("levelManager");
                ObjectRenderer playerScript2 = thePlayer2.GetComponent<ObjectRenderer> ();
                playerScript2.indicator = 3;
                playerScript2.onclickright ();
                playerScript2.leftarrow.SetActive (false);
                playerScript2.scorevar_3 = scorevar1;
                playerScript2.scorevar_4 = scorevar2;
                playerScript2.scorevar_5 = scorevar_1;
                playerScript2.scorevar_1 = scorevar_4;
                playerScript2.scorevar_2 = scorevar_5;
                playerScript2.flaagg++;
                playerScript2.restartStopwatch ();
                self.SetActive (false);
                yield break;
            } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 < 6) {
                down2.SetActive (true);
                GameObject thePlayer3 = GameObject.Find ("Levelmanager1");
                LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
                playerScript3.leftbutton.SetActive (false);
                playerScript3.scorevar_1 = scorevar_5;
                playerScript3.scorevar_2 = scorevar1;
                playerScript3.scorevar_3 = scorevar2;
                playerScript3.scorevar_4 = scorevar_1;
                playerScript3.scorevar_6 = scorevar_3;
                playerScript3.scorevar_5 = scorevar_2;
                playerScript3.flaagg++;
                playerScript3.restartStopwatch ();
                self.SetActive (false);
                yield break;

            } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 == 6 && scorevar_5 < 20) {
                down3.SetActive (true);
                GameObject thePlayer4 = GameObject.Find ("LevelManager1");
                ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
                playerScript4.leftarrow.SetActive (false);
                playerScript4.scorevar_1 = scorevar1;
                playerScript4.scorevar_2 = scorevar2;
                playerScript4.scorevar_3 = scorevar_1;
                playerScript4.scorevar_4 = scorevar_2;
                playerScript4.scorevar_5 = scorevar_3;
                playerScript4.scorevar_6 = scorevar_4;
                playerScript4.flaagg++;
                playerScript4.restartStopwatch ();
                self.SetActive (false);
                yield break;

            } else if (scorevar2 == 24 && scorevar_1 == 6 && scorevar_2 == 40 && scorevar_3 == 24 && scorevar_4 == 6 && scorevar_5 == 20) {
                StartCoroutine (ShowMessage1 (0.5f));
                yield break;
            }
        }
        if (alphabet == "abc") {
            if (scorevar == 24 && scorevar_1 < 6) {
                down2.SetActive (true);
                GameObject thePlayer3 = GameObject.Find ("Levelmanager1");
                LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
                if (scorevar_3 == 40 && scorevar_4 == 24 && scorevar_5 == 6) {
                    playerScript3.leftbutton.SetActive (false);
                }
                playerScript3.scorevar_1 = scorevar_2;
                playerScript3.scorevar_2 = scorevar_3;
                playerScript3.scorevar_3 = scorevar_4;
                playerScript3.scorevar_4 = scorevar_5;
                playerScript3.scorevar_6 = scorevar2;
                playerScript3.scorevar_5 = scorevar1;
                playerScript3.flaagg++;
                playerScript3.restartStopwatch ();
                self.SetActive (false);
                yield break;
            } else if (scorevar == 24 && scorevar_1 == 6 && scorevar_2 < 20) {
                down3.SetActive (true);
                GameObject thePlayer4 = GameObject.Find ("LevelManager1");
                ObjectRenderer1 playerScript4 = thePlayer4.GetComponent<ObjectRenderer1> ();
                if (scorevar_3 == 40 && scorevar_4 == 24 && scorevar_5 == 6) {
                    playerScript4.leftarrow.SetActive (false);
                }
                playerScript4.scorevar_1 = scorevar_3;
                playerScript4.scorevar_2 = scorevar_4;
                playerScript4.scorevar_3 = scorevar_5;
                playerScript4.scorevar_4 = scorevar1;
                playerScript4.scorevar_5 = scorevar2;
                playerScript4.scorevar_6 = scorevar_1;
                playerScript4.flaagg++;
                playerScript4.restartStopwatch ();
                self.SetActive (false);
                yield break;
            } else if (scorevar == 24 && scorevar_1 == 6 && scorevar_2 < 20) {
                StartCoroutine (ShowMessage1 (0.5f));
                yield break;
            }
        }

        leftarrow.SetActive (false);

        animation ();

        indicator++;
        if (indicator == 4) {
            StartCoroutine (ShowMessage (0, 3f));
        }
        setInitialPosition ();
        Invoke ("render", 0.5f);

    }
    IEnumerator ShowMessage1 (float delay) {
        yield return new WaitForSeconds (delay);
        finaltext.SetActive (true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds (2f);
        finaltext.SetActive (false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }
    async public void onclickleft () {
        //change
        // timer.Restart ();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference ("users/" + username + "/matching/s" + session + "/q" + (indicator + qh))
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
        ////
        if (DOTween.TotalPlayingTweens () == 0) {
            if (indicator == 0) {
                if (number == 123) {

                    SceneManager.LoadScene ("Game Selection", LoadSceneMode.Single);
                    return;
                }
            }
            if (alphabet == "abc") {

                if (indicator > 3 && scorevar2 == 24 && scorevar1 < 40) {

                    indicator = 4;

                }
                if (indicator == 0 || (scorevar1 == 40 && scorevar2 == 24) || (indicator <= 3 && scorevar1 == 40) || (indicator == 4 && scorevar1 == 40)) {
                    if (scorevar_5 == 6 && scorevar_4 < 24) {
                        up1.SetActive (true);
                        GameObject thePlayer4 = GameObject.Find ("LevelManager");
                        ObjectRenderer playerScript4 = thePlayer4.GetComponent<ObjectRenderer> ();
                        playerScript4.scorevar = scorevar;
                        playerScript4.scorevar_1 = scorevar_5;
                        playerScript4.scorevar_2 = scorevar1;
                        playerScript4.scorevar_3 = scorevar2;
                        playerScript4.scorevar_4 = scorevar_1;
                        playerScript4.scorevar_5 = scorevar_2;
                        playerScript4.flaagg++;
                        playerScript4.restartStopwatch ();
                        self.SetActive (false);
                        // play2.SetActive(false);
                        return;
                    } else if (scorevar_5 == 6 && scorevar_4 == 24 && scorevar_3 < 40) {
                        up1.SetActive (true);
                        GameObject thePlayer1 = GameObject.Find ("LevelManager");
                        ObjectRenderer playerScript1 = thePlayer1.GetComponent<ObjectRenderer> ();
                        playerScript1.indicator = 4;
                        playerScript1.onclickleft ();
                        playerScript1.scorevar = scorevar;
                        playerScript1.scorevar_1 = scorevar_5;
                        playerScript1.scorevar_2 = scorevar1;
                        playerScript1.scorevar_3 = scorevar2;
                        playerScript1.scorevar_4 = scorevar_1;
                        playerScript1.scorevar_5 = scorevar_2;
                        playerScript1.restartStopwatch ();
                        self.SetActive (false);
                        // play2.SetActive(false);
                        return;
                    }
                    up.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("Levelmanager");
                    LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
                    playerScript.scorevar = scorevar;
                    playerScript.scorevar_1 = scorevar1;
                    playerScript.scorevar_2 = scorevar2;
                    playerScript.scorevar_3 = scorevar_1;
                    playerScript.scorevar_4 = scorevar_2;
                    playerScript.scorevar_5 = scorevar_3;
                    playerScript.scorevar_6 = scorevar_4;
                    playerScript.flaagg++;

                    playerScript.restartStopwatch ();
                    self.SetActive (false);
                    // play2.SetActive(false);
                    return;
                }

            }

            if (indicator == 4) {

                StartCoroutine (ShowMessage (1, 0.5f));

            }
            if (number == 123) {
                if (scorevar1 == 40 && indicator == 5) {
                    leftarrow.SetActive (false);
                }
            }
            if (alphabet == "abc") {
                if ((scorevar_3 == 40 && scorevar_4 == 24 && scorevar_5 == 6 && indicator == 1) || (scorevar_3 == 40 && scorevar_4 == 24 && scorevar_5 == 6 && scorevar1 == 40 && indicator == 5)) {
                    leftarrow.SetActive (false);

                }
            }

            var --;
            // dropped = 0;
            anf = 270;
            a = 1;
            animation ();
            if (indicator != 0) {
                indicator--;

                setInitialPosition ();

                // render ();
                Invoke ("render", 0.5f);

            }
            Invoke ("clearob2", 0.7f);

        }

    }
    public void clearob2 () {
        for (int i = 0; i < 40; i++) {
            if (objects[i].transform.position == objects2[i].transform.position) {
                objects2[i].SetActive (false);

            }
        }
    }
    public void animation () {
        for (int i = 0; i < cards.Length; i++) {
            cards[i].transform.DORotate (new Vector3 (0, cards[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
            objects[i + ((indicator) * 5)].transform.DORotate (new Vector3 (0, objects[i + ((indicator) * 5)].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
            cards2[i].transform.DORotate (new Vector3 (0, cards2[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
            objects2[i + ((indicator) * 5)].transform.DORotate (new Vector3 (0, objects2[i + ((indicator) * 5)].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
        }

    }
    void flaag () {
        up.SetActive (true);
        GameObject thePlayer = GameObject.Find ("Levelmanager");
        LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
        scorevar = playerScript.scorevar;

        scorevar_1 = playerScript.scorevar_3;
        scorevar_2 = playerScript.scorevar_4;
        scorevar_3 = playerScript.scorevar_5;
        scorevar_4 = playerScript.scorevar_6;
        scorevar_5 = playerScript.scorevar2;

        playerScript.restartStopwatch ();
        flag++;
        up.SetActive (false);

    }
    public void render () {
        for (int i = 0; i < 40; i++) {
            if (i < 5) {
                cards2[i].SetActive (true);
            }
            objects[i].SetActive (false);
            objects2[i].SetActive (false);
            if ((indicator == 0) && (i >= 0 && i < 5)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);

            } else if ((indicator == 1) && (i >= 5 && i < 10)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 2) && (i >= 10 && i < 15)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 3) && (i >= 15 && i < 20)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 4) && (i >= 20 && i < 25)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 5) && (i >= 25 && i < 30)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 6) && (i >= 30 && i < 35)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 7) && (i >= 35 && i < 40)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            }
            if (indicator >= 0) {

                objects[i].transform.eulerAngles = new Vector3 (objects[i].transform.eulerAngles.x, anf, objects[i].transform.eulerAngles.z);
                objects2[i].transform.eulerAngles = new Vector3 (objects[i].transform.eulerAngles.x, anf, objects[i].transform.eulerAngles.z);

            }

            // if (objects[i].transform.position == objects2[i].transform.position)
            // {
            //     var++;
            //     Debug.Log(var);
            //     objects2[i].SetActive(false);
            // }
        }
        if (indicator >= 0) {
            // Debug.Log("Called");
            animation ();
        }
        if (indicator == 4) {
            cards2[0].SetActive (false);
            cards2[4].SetActive (false);
            for (int j = 20; j < 25; j++) {
                if ((cards2[0].transform.position == objects2[j].transform.position) || (cards2[4].transform.position == objects2[j].transform.position)) {
                    objects2[j].SetActive (false);
                }
            }
        } else if (indicator == 5) {
            cards2[0].SetActive (false);
            cards2[4].SetActive (false);
            for (int j = 25; j < 30; j++) {
                if ((cards2[0].transform.position == objects2[j].transform.position) || (cards2[4].transform.position == objects2[j].transform.position)) {
                    objects2[j].SetActive (false);
                }
            }
        } else if (indicator == 6) {
            cards2[0].SetActive (false);
            cards2[4].SetActive (false);
            for (int j = 30; j < 35; j++) {
                if ((cards2[0].transform.position == objects2[j].transform.position) || (cards2[4].transform.position == objects2[j].transform.position)) {
                    objects2[j].SetActive (false);
                }
            }
        } else if (indicator == 7) {
            cards2[0].SetActive (false);
            cards2[4].SetActive (false);
            for (int j = 35; j < 40; j++) {
                if ((cards2[0].transform.position == objects2[j].transform.position) || (cards2[4].transform.position == objects2[j].transform.position)) {
                    objects2[j].SetActive (false);
                }
            }

        }

    }
    public static int random_except_list (int n, int[] x) {
        int result = r.Next (n - x.Length);

        for (int i = 0; i < x.Length; i++) {
            if (result < x[i])
                return result;
            result++;
        }
        return result;
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

            reposnseTime = timer.Elapsed.TotalSeconds.ToString ("0.##");
            timer.Restart ();
            dragLocker = false;
        }
        // //////
        // Debug.Log (objects[ob].transform.position);
        // Debug.Log (objects2[ob].transform.position);

        if (objects[ob].transform.position == objects2[ob].transform.position) {
            Debug.Log ("i am here");
            return;
            // goto abc;

        }
        // Debug.Log ("i am here2");

        var point = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        objects[ob].transform.position = new Vector3 (point.x, point.y, objects[ob].transform.position.z);
        // abc:
    }
    public void drop (int a) {

        Vector2 objPos;
        Vector2 mousePos;
        float mousePosY, mousePosX;

        objPos = objects2[a].transform.position; //gets player position
        mousePos = Input.mousePosition; //gets mouse postion
        mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        mousePosX = mousePos.x - objPos.x; //gets the distance between object and mouse position for x
        mousePosY = mousePos.y - objPos.y; //gets the distance between object and mouse position for y  if you want this.
        mousePosX = mousePosX * mousePosX;
        mousePosY = mousePosY * mousePosY;
        var c = mousePosX + mousePosY;
        string dropDistance = Mathf.Sqrt (c).ToString ("0.##");

        //change
        string dragTime = timer.Elapsed.TotalSeconds.ToString ("0.##");
        dragLocker = true;
        timer.Restart ();
        string status = "wrong";
        /////

        string destination = "-";
        for (int p = indicator * 5; p < ((indicator * 5) + 5); p++) {
            if ((Vector3.Distance (objects[a].transform.position, objects2[p].transform.position) < 10) && (objects2[p].activeInHierarchy)) {
                destination = sources[p];
            }
        }
        if (objects[a].transform.position == objects2[a].transform.position) {
            Debug.Log ("i am here");
            return;
            // goto abc;

        }

        float Distance = Vector3.Distance (objects[a].transform.position, objects2[a].transform.position);

        if (objects2[a].activeInHierarchy) {
            if (Distance < 10) {
                destination = sources[a];

                objects[a].transform.position = objects2[a].transform.position;
                //  objects2[a].GetActive
                dropped[indicator]++;

                SettingsPenal.playeffect (0);

                if (objects2[a].active) {
                    // dropped++;
                    Increment ();
                }
                objects2[a].SetActive (false);

                //  if(objects[a].transform.position==objects2[a].transform.position)
                // {

                //     return;
                // }
                //change
                status = "right";
                //////
                if (solved.Length > 0) {
                    solved = solved + "," + a.ToString ();
                } else {
                    solved = a.ToString ();
                }
            } else {
                objects[a].transform.position = inipos[a % 5];
                SettingsPenal.playeffect (1);
            }
        } else {
            objects[a].transform.position = inipos[a % 5];
            SettingsPenal.playeffect (1);
        }
        //change
        saveAttempt (reposnseTime, dragTime, status, dragDistance, dropDistance, sources[a], destination);
        /////

        if (dropped[indicator] == 5) {
            onclickright ();

        } else if (dropped[indicator] == 3 && indicator > 3) {
            onclickright ();

        }
    }
    //change
    IEnumerator updateAnalytics (String user, String game, int session, int questionNo, string reposnseTime, string dragTime, string status, string dragDistance, string dropDistance, string source, string destination) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("game", game);
        form.AddField ("session", session);
        form.AddField ("questionNo", questionNo);
        form.AddField ("reposnseTime", reposnseTime);
        form.AddField ("dragTime", dragTime);
        form.AddField ("status", status);
        form.AddField ("dragDistance", dragDistance);
        form.AddField ("dropDistance", dropDistance);
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
    async void saveAttempt (string reposnseTime, string dragTime, string status, string dragDistance, string dropDistance, string source, string destination) {

        // Debug.Log("in funtionn");
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // AtemptMatching atempt = new AtemptMatching(reposnseTime, dragTime, status, dragDistance, dropDistance, source, destination);
        // string json = JsonUtility.ToJson(atempt);
        // Debug.Log("qh");
        // Debug.Log(qh);

        // reference.Child("users").Child(username).Child("matching").Child("s" + session).Child("q" + (indicator + 1 + qh)).Child("a" + attemptNo).SetRawJsonValueAsync(json);
        // attemptNo++;
        int questionNo = indicator + 1 + qh;
        StartCoroutine (updateAnalytics (username, game, session, questionNo, reposnseTime, dragTime, status, dragDistance, dropDistance, source, destination));

        if (status == "right") {
            StartCoroutine (updateScore (username, game, gameMode, scorevar, scorevar1, scorevar2, scorevar_1, scorevar_2, scorevar_3, scorevar_4, scorevar_5, solved));
        }
    }
    /////
    public void restartTimer () {
        timer.Restart ();
    }
    void Update () {
        // Debug.Log ("jajajajaja");
        if (number == 123) {
            if (datacheck == 5) {
                resumePop.SetActive (false);
                jump ();
                datacheck++;
            }
        }

        if (flag == 5) {
            flaag ();
        }
        if (number == 123) {
            if (flaagg == 1) {
                StartCoroutine (ShowMessage (0, 0.5f));
                flaagg--;
            }
        }
        if (alphabet == "abc") {
            if (flaagg == 1) {
                StartCoroutine (ShowMessage (1, 0.5f));
                flaagg--;
            } else if (flaagg == 2) {
                StartCoroutine (ShowMessage (0, 0.5f));
                flaagg = flaagg - 2;
            }
        }

        // if (scorevar == 10)
        // {
        //     stars[0].SetActive(true);
        // }
        // if (scorevar == 20)
        // {
        //     stars[1].SetActive(true);
        // }
        // if (scorevar == 30)
        // {
        //     stars[2].SetActive(true);
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

//change
class AtemptMatching {
    public string reposnseTime;
    public string dragTime;
    public string status;
    public string dragDistance;
    public string dropDistance;
    public string source;
    public string destination;
    public string timeStamp;

    public AtemptMatching () { }

    public AtemptMatching (string reposnseTime, string dragTime, string status, string dragDistance, string dropDistance, string source, string destination) {
        this.reposnseTime = reposnseTime;
        this.dragTime = dragTime;
        this.status = status;
        this.dragDistance = dragDistance;
        this.dropDistance = dropDistance;
        this.source = source;
        this.destination = destination;
        this.timeStamp = DateTime.Now.ToString ("MM/dd/yyyy HH:mm:ss");

    }
    // public AtemptMatching(string reposnseTime, string dragTime, string status, string dragDistance, string dropDistance)
    // {
    //     this.reposnseTime = reposnseTime;
    //     this.dragTime = dragTime;
    //     this.status = status;
    //     this.dragDistance = dragDistance;
    //     this.dropDistance = dropDistance;
    // }
}
/////