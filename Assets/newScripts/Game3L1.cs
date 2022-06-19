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

public class Game3L1 : MonoBehaviour {

    public GameObject[] cards;
    public GameObject[] cards2;
    public GameObject[] objects;
    public GameObject[] objects2;
    public Vector2[] inipos;
    public int dropped;
    public int indicator;
    public static System.Random r = new System.Random ();
    public int var;
    public GameObject[] stars;
    public int number;
    public string alphabet;
    public GameObject self;
    public GameObject right;
    public GameObject wrong;
    public GameObject up;
    public GameObject down;
    public GameObject down1;
    public int scorevar = 0;
    public int scorevar2 = 0;
    public int scorevar1 = 0;
    public int scorevar3 = 0;
    public int scorevar_2 = 0;
    public int scorevar_1 = 0;
    public int scorevar_3 = 0;
    public Text newText;
    public int flag;
    public bool[] record;
    public string[] title;
    public Text fortitle;
    public int anf;
    public int a;
    public int game7;
    public GameObject levelcomanim;
    public Text Stagecomplete;
    public string Stagetext, Stagetext2;
    public GameObject finaltext;
    public GameObject ltext;
    //change
    public Stopwatch timer;
    // public bool dragLocker;
    private string reposnseTime;
    private string username;
    private int session;
    private int attemptNo;
    private int qh;
    public string game;
    public GameObject Gameover;
    public GameObject leftarrow;
    private string postions = "";
    private string solved = "";
    string sPostions = "";
    string sSolved = "";
    public GameObject resumePop;

    /////

    public string[] answers;

    // public Score other;

    async void Start () {

        //change
        qh = 0;
        if (game == "emotions") {
            qh = 5;
        }

        if (game == "classification") {
            answers[0] = "table";
            answers[1] = "shoe";
            answers[2] = "banana";
            answers[3] = "chair";
            answers[4] = "ladder";
            answers[5] = "car";
            answers[6] = "matchbox";
            answers[7] = "iron";
            answers[8] = "spoon";
            answers[9] = "catepilar";
            answers[10] = "parrot";
            answers[11] = "fish";
            answers[12] = "fish";
            answers[13] = "cat";
            answers[14] = "lion";
            answers[15] = "mango";
            answers[16] = "watermelon";
            answers[17] = "carrot";
            answers[18] = "catepilar";
            answers[19] = "rhino";
            answers[20] = "cat";
            answers[21] = "book";
            answers[22] = "fork";
            answers[23] = "hammer";
            answers[24] = "table";
            answers[25] = "ring";
            answers[26] = "blush brush";
            answers[27] = "towel";
            answers[28] = "alarmclock";
            answers[29] = "blue t-shirt";
        } else if (game == "selection") {
            answers[0] = "cat";
            answers[1] = "bird";
            answers[2] = "fish";
            answers[3] = "football";
            answers[4] = "parrot";
            answers[5] = "shoe";
            answers[6] = "book";
            answers[7] = "banana";
            answers[8] = "hammer";
            answers[9] = "catepilar";
            answers[10] = "parrot";
            answers[11] = "fish";
            answers[12] = "potato";
            answers[13] = "peach";
            answers[14] = "mango";
            answers[15] = "cat";
            answers[16] = "lion";
            answers[17] = "dog";
            answers[18] = "fork";
            answers[19] = "spectula";
            answers[20] = "spoon";
            answers[21] = "apple";
            answers[22] = "tomato";
            answers[23] = "ball";
            answers[24] = "car";
            answers[25] = "cycle";
            answers[26] = "bus";
            answers[27] = "pencil";
            answers[28] = "spectula";
            answers[29] = "voilin";
        } else if (game == "adjective") {
            answers[0] = "pizza";
            answers[1] = "fire";
            answers[2] = "ice";
            answers[3] = "mountain";
            answers[4] = "table";
            answers[5] = "coat";
            answers[6] = "matchbox";
            answers[7] = "cup";
            answers[8] = "football";
            answers[9] = "pencil";
            answers[10] = "scale";
            answers[11] = "bat";
            answers[12] = "juice glass";
            answers[13] = "icecream";
            answers[14] = "hot tea";
            answers[15] = "ball";
            answers[16] = "bricks";
            answers[17] = "teddy bear";
            answers[18] = "night beach";
            answers[19] = "night city";
            answers[20] = "sunny day";
            answers[21] = "lion";
            answers[22] = "dog";
            answers[23] = "cat";
            answers[24] = "tree";
            answers[25] = "plant";
            answers[26] = "plantbud";
            answers[27] = "icecream cup";
            answers[28] = "ice";
            answers[29] = "glass";

        } else if (game == "emotions") {
            answers[0] = "surprised face";
            answers[1] = "weeping face";
            answers[2] = "happy face";
            answers[3] = "suprised face";
            answers[4] = "weeping face";
            answers[5] = "sad face";
            answers[6] = "suprised face";
            answers[7] = "happy face";
            answers[8] = "angry face";
            answers[9] = "sad face";
            answers[10] = "angry face";
            answers[11] = "surprised face";
            answers[12] = "happy face";
            answers[13] = "sad face";
            answers[14] = "weeping face";
        } else if (game == "association") {
            answers[0] = "shoe";
            answers[1] = "house";
            answers[2] = "wood";
            answers[3] = "cat";
            answers[4] = "bee";
            answers[5] = "bird";
            answers[6] = "caterpilar";
            answers[7] = "parrot";
            answers[8] = "fish";
            answers[9] = "key";
            answers[10] = "pencil";
            answers[11] = "spoon";
            answers[12] = "nailcutter";
            answers[13] = "hammer";
            answers[14] = "pencil";
            answers[15] = "books";
            answers[16] = "plate";
            answers[17] = "wood";
            answers[18] = "vegitables basket";
            answers[19] = "bricks";
            answers[20] = "table";
            answers[21] = "shoe";
            answers[22] = "cycle";
            answers[23] = "books";
            answers[24] = "teacup";
            answers[25] = "nailcutter";
            answers[26] = "color pencils";
            answers[27] = "nailcutter";
            answers[28] = "glass";
            answers[29] = "pencil box";
            answers[30] = "butterfly";
            answers[31] = "bird";
            answers[32] = "bee";
            answers[33] = "juice pack";
            answers[34] = "milk pack";
            answers[35] = "honey pot";
            answers[36] = "mango";
            answers[37] = "lays";
            answers[38] = "bread";
            answers[39] = "juice glass";
            answers[40] = "cup";
            answers[41] = "plate";
            answers[42] = "scale";
            answers[43] = "hammer";
            answers[44] = "fork & knife";

        }
        username = File.ReadAllText (Application.dataPath + "/user.txt");
        /////

        // self.SetActive(false);
        // if (var == 12)
        // {
        //     self.SetActive(false);
        // }

        indicator = 0;
        render ();
        randomize ();
        right.SetActive (false);
        wrong.SetActive (false);

        // setInitialPosition();

        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive (false);
        }

        // other = new Score();
        // if (alphabet == "abc")
        // {
        //     self.SetActive(false);
        // }
        scorevar = 0;

        //change
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference ("users/" + username + "/" + game)
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
        //     .GetReference ("users/" + username + "/" + game + "/s" + session + "/q" + (indicator + 1 + qh))
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

        // timer = new Stopwatch();
        // timer.Start();

        /////

    }
    public void jump () {
        if (game7 == 5) {
            if (scorevar1 == 10 && scorevar3 < 20) {
                indicator = 5;
                render ();
                waiting ();
                leftarrow.SetActive (false);
            }
            if (scorevar1 == 10 && scorevar3 == 20 && scorevar_1 < 48) {
                down.SetActive (true);
                GameObject thePlayer = GameObject.Find ("LevelManager1");
                game7l2 playerScript = thePlayer.GetComponent<game7l2> ();
                playerScript.scorevar = scorevar;
                playerScript.leftarrow.SetActive (false);
                playerScript.restartStopwatch ();
                playerScript.flag++;
                self.SetActive (false);

            }
            if (scorevar1 == 10 && scorevar3 == 20 && scorevar_1 == 48 && scorevar_2 < 20) {
                down1.SetActive (true);
                GameObject thePlayer1 = GameObject.Find ("LevelManager2");
                game7l3 playerScript1 = thePlayer1.GetComponent<game7l3> ();
                playerScript1.leftarrow.SetActive (false);
                playerScript1.scorevar = scorevar;
                playerScript1.restartStopwatch ();
                playerScript1.flag++;
                self.SetActive (false);

            }
            return;
        }
        if (var != 13) {
            Debug.Log ("in jamp");
            if (scorevar2 == 20) {
                down.SetActive (true);
                GameObject thePlayer = GameObject.Find ("LevelManager1");
                game3l2 playerScript = thePlayer.GetComponent<game3l2> ();
                playerScript.scorevar = scorevar;
                if (playerScript.scorevar1 < 8) {
                    playerScript.indicator = 0;
                    playerScript.flag++;
                } else if (playerScript.scorevar1 == 8 && playerScript.scorevar2 < 20) {
                    playerScript.indicator = 4;
                    playerScript.render ();
                    playerScript.waiting ();
                    playerScript.flag += 2;
                } else if (playerScript.scorevar1 == 8 && playerScript.scorevar2 == 20 && playerScript.scorevar3 < 20) {
                    playerScript.indicator = 9;
                    playerScript.render ();
                    playerScript.waiting ();
                    playerScript.flag += 3;
                }
                playerScript.leftarrow.SetActive (false);
                playerScript.scorevar0 = scorevar2;
                self.SetActive (false);
            }

        }
    }

    IEnumerator Upload (String user, String game, String gameMode, String positions) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("game", game);
        form.AddField ("gameMode", gameMode);
        form.AddField ("positions", positions);

        using (UnityWebRequest www = UnityWebRequest.Post ("https://evening-fortress-14821.herokuapp.com/select", form)) {
            yield return www.SendWebRequest ();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log (www.error);
            } else {
                Debug.Log ("Form upload complete!");
            }
        }
    }
    IEnumerator updateScore (String user, String game, String gameMode, int score, int score1, int score2, int score3, int score4, int score5, int score6, String solved) {
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

                if (myObject.solved != null) {
                    sSolved = myObject.solved.Length > 0 ? myObject.solved : sSolved;
                }
                if (myObject.score != null) {
                    scorevar = myObject.score;
                }
                if (myObject.score1 != null) {
                    scorevar1 = myObject.score1;
                    scorevar2 = myObject.score2;
                    scorevar3 = myObject.score3;
                    scorevar_1 = myObject.score4;
                    scorevar_2 = myObject.score5;
                    scorevar_3 = myObject.score6;
                }

                if (sPostions.Length > 0) {
                    string[] posPairs = sPostions.Split (',');
                    foreach (string posPair in posPairs) {
                        string[] post = posPair.Split ('-');
                        objects[Int32.Parse (post[0])].transform.position = cards[Int32.Parse (post[1])].transform.position;
                    }
                }
                if (sSolved.Length > 0) {
                    solved = sSolved;
                    string[] sols = sSolved.Split (',');
                    foreach (string sol in sols) {
                        record[Int32.Parse (sol)] = true;
                        // dropped[(int)Decimal.Truncate(Int32.Parse(sol) / 5)]++;
                        // objects[Int32.Parse(sol)].transform.position = objects2[Int32.Parse(sol)].transform.position;

                    }
                }
                if (var == 12 || alphabet == "abc") {
                    if (var == 12) {
                        GameObject thePlayer = GameObject.Find ("LevelManager");
                        Game6l1 playerScript = thePlayer.GetComponent<Game6l1> ();
                        playerScript.scorevar_2 = scorevar2;
                        playerScript.datacheck++;
                        Debug.Log ("3l1");
                    }
                    self.SetActive (false);
                }

                timer = new Stopwatch ();
                timer.Start ();
                datacheck++;
                if (var == 12 || alphabet == "abc") {
                    resumePop.SetActive (false);
                    waiting ();
                }

            }
        }
    }

    public void onStarting (bool resume, int sess) {
        Debug.Log ("Cal,,3");

        session = sess;

        if (resume) {
            StartCoroutine (GetRequest ("https://evening-fortress-14821.herokuapp.com/select/" + username + "/" + game + "/1"));
        } else {
            Debug.Log ("Cal,,4");

            StartCoroutine (Upload (username, game, "1", postions));

            if (var == 12 || alphabet == "abc") {
                self.SetActive (false);
            }

            timer = new Stopwatch ();
            timer.Start ();
            resumePop.SetActive (false);
        }
    }

    public void exittomainmanu () {
        SceneManager.LoadScene ("Game Selection", LoadSceneMode.Single);
    }

    public void onclick (int index) {
        //change
        reposnseTime = timer.Elapsed.TotalSeconds.ToString ("0.##");
        string status = "wrong";
        bool saveData = !record[indicator];
        ////

        bool stat;
        if ((index + 1) % 3 == 0) {
            right.transform.position = objects[index].transform.position;
            right.SetActive (true);
            record[indicator] = true;
            SettingsPenal.playeffect (0);

            scorevar = scorevar + 2;
            scorevar2 = scorevar2 + 2;
            if (game7 == 5) {
                if (indicator >= 0 && indicator <= 4) {
                    scorevar1 = scorevar1 + 2;
                } else if (indicator >= 5 && indicator <= 14) {
                    scorevar3 = scorevar3 + 2;
                }
            }

            //change
            status = "right";
            if (solved.Length > 0) {
                solved = solved + "," + indicator.ToString ();
            } else {
                solved = indicator.ToString ();
            }
            /////
            stat = true;

        } else {
            wrong.transform.position = objects[index].transform.position;
            wrong.SetActive (true);
            stat = false;
            SettingsPenal.playeffect (1);

        }
        //change
        if (saveData) {
            saveAttempt (reposnseTime, status, title[indicator], answers[index]);
        }

        /////
        StartCoroutine (Example (stat));
    }
    //change
    IEnumerator updateAnalytics (String user, String game, int session, int questionNo, string reposnseTime, string status, string question, string answer) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("game", game);
        form.AddField ("session", session);
        form.AddField ("questionNo", questionNo);
        form.AddField ("reposnseTime", reposnseTime);
        form.AddField ("status", status);
        form.AddField ("question", question);
        form.AddField ("answer", answer);

        using (UnityWebRequest www = UnityWebRequest.Post ("https://evening-fortress-14821.herokuapp.com/analytics", form)) {
            yield return www.SendWebRequest ();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log (www.error);
            } else {
                Debug.Log ("attemmpt upload complete!");
            }
        }
    }
    async void saveAttempt (string reposnseTime, string status, string question, string answer) {

        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // AtemptClassification atempt = new AtemptClassification(reposnseTime, status, question, answer);
        // string json = JsonUtility.ToJson(atempt);

        // reference.Child("users").Child(username).Child(game).Child("s" + session).Child("q" + (indicator + 1 + qh)).Child("a" + attemptNo).SetRawJsonValueAsync(json);
        // attemptNo++;
        int questionNo = indicator + 1 + qh;
        StartCoroutine (updateAnalytics (username, game, session, questionNo, reposnseTime, status, question, answer));

        if (status == "right") {
            StartCoroutine (updateScore (username, game, "1", scorevar, scorevar1, scorevar2, scorevar3, scorevar_1, scorevar_2, scorevar_3, solved));
        }
    }
    /////
    public void restartTimer () {
        timer.Restart ();
    }
    IEnumerator Example (bool flag) {

        yield return new WaitForSeconds (0.5f);
        timer.Restart ();
        if (flag) {

            // right.SetActive(false);
            onclickright ();

        } else {
            wrong.SetActive (false);

        }

    }
    IEnumerator ShowMessage1 (float delay) {
        yield return new WaitForSeconds (delay);
        finaltext.SetActive (true);
        yield return new WaitForSeconds (2f);
        finaltext.SetActive (false);
    }
    public void endanimation () {
        // Gameover.SetActive (false);
        SceneManager.LoadScene ("Game Selection");
    }
    public void randomize () {
        for (int i = 0; i < objects.Length; i += 3) {

            List<int> termsList = new List<int> ();
            List<int> termsList2 = new List<int> ();

            for (int j = 0; j < 3; j++) {

                int pos = random_except_list (3, termsList.ToArray ());
                termsList.Add (pos);
                termsList.Sort ();

                objects[i + j].transform.position = cards[pos].transform.position;

                if (postions.Length > 0) {
                    postions = postions + "," + (i + j).ToString () + "-" + pos.ToString ();
                } else {
                    postions = (i + j).ToString () + "-" + pos.ToString ();
                }

                // int pos2 = random_except_list(5, termsList2.ToArray());
                // termsList2.Add(pos2);
                // termsList2.Sort();
                // objects2[i + j].transform.position = cards2[pos2].transform.position;
            }
        }
    }
    /////////////////these functions are for the animations to work at the right time without any co-routine
    public void endanim () {
        levelcomanim.SetActive (false);
    }
    public void ddown () {
        down.SetActive (true);
        GameObject thePlayer = GameObject.Find ("LevelManager1");
        game3l2 playerScript = thePlayer.GetComponent<game3l2> ();

        playerScript.scorevar = scorevar;
        if (playerScript.scorevar1 < 8) {
            playerScript.indicator = 0;
            playerScript.flag++;
        } else if (playerScript.scorevar1 == 8 && playerScript.scorevar2 < 20) {
            playerScript.indicator = 4;
            playerScript.flag += 2;
        } else if (playerScript.scorevar1 == 8 && playerScript.scorevar2 == 20 && playerScript.scorevar3 < 20) {
            playerScript.indicator = 9;
            playerScript.flag += 3;
        } else if (playerScript.scorevar1 == 8 && playerScript.scorevar2 == 20 && playerScript.scorevar3 == 20) {

        }
        playerScript.leftarrow.SetActive (false);
        playerScript.restartStopwatch ();
        playerScript.scorevar0 = scorevar2;
        self.SetActive (false);
    }

    public void ddown1 () {
        if (scorevar_2 == 20 && scorevar_1 < 10) {
            StartCoroutine (ShowMessage1 (0.5f));
            return;
        }
        down.SetActive (true);
        GameObject thePlayer = GameObject.Find ("LevelManager2");
        game6l3 playerScript = thePlayer.GetComponent<game6l3> ();
        if (scorevar_1 == 10) {
            playerScript.leftarrow.SetActive (false);
        }
        playerScript.scorevar = scorevar;
        playerScript.scorevar_1 = scorevar2;
        playerScript.scorevar_2 = scorevar_1;
        playerScript.restartStopwatch ();
        playerScript.flag++;

        self.SetActive (false);
    }
    public void ddown2 () {
        if (scorevar_1 == 48 && scorevar_2 < 20) {
            down1.SetActive (true);
            GameObject thePlayer1 = GameObject.Find ("LevelManager2");
            game7l3 playerScript1 = thePlayer1.GetComponent<game7l3> ();
            if (scorevar1 == 10) {
                playerScript1.leftarrow.SetActive (false);
            }
            playerScript1.scorevar = scorevar;
            playerScript1.scorevar_1 = scorevar1;
            playerScript1.scorevar_2 = scorevar3;
            playerScript1.scorevar_3 = scorevar_1;
            playerScript1.restartStopwatch ();
            playerScript1.flag++;
            self.SetActive (false);
            return;
        }
        if (scorevar1 < 10 && scorevar_1 == 48 && scorevar_2 == 20) {
            StartCoroutine (ShowMessage1 (0.5f));
            return;
        }
        down.SetActive (true);
        GameObject thePlayer = GameObject.Find ("LevelManager1");
        game7l2 playerScript = thePlayer.GetComponent<game7l2> ();
        if (scorevar1 == 10) {
            playerScript.leftarrow.SetActive (false);
        }
        playerScript.scorevar = scorevar;
        playerScript.scorevar_2 = scorevar1;
        playerScript.scorevar_3 = scorevar3;
        playerScript.restartStopwatch ();
        playerScript.flag++;
        self.SetActive (false);

    }
    public async void restartStopwatch () {
        try {
            timer.Restart ();
        } catch {
            Debug.Log ("re");
        }
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/sorting/s" + session + "/q" + (indicator + qh + 1))
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
    }
    async public void onclickright () {
        //change
        // timer.Restart ();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference ("users/" + username + "/" + game + "/s" + session + "/q" + (indicator + 2 + qh))
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
        if (DOTween.TotalPlayingTweens () == 0) {
            leftarrow.SetActive (true);
            if (game7 == 5) {
                if (scorevar >= 98) {
                    SettingsPenal.gameover ();
                    Gameover.SetActive (true);
                    Invoke ("endanimation", 6.5f);
                    return;
                }
                if (scorevar3 == 20) {
                    levelcomanim.SetActive (true);
                    SettingsPenal.playanimation ();
                    Invoke ("endanim", 3f);
                    Invoke ("ddown2", 3.3f);
                    return;
                }
                if (indicator == 14) {
                    if (scorevar_1 == 48 && scorevar_2 < 20) {
                        down.SetActive (true);
                        GameObject thePlayer1 = GameObject.Find ("LevelManager2");
                        game7l3 playerScript1 = thePlayer1.GetComponent<game7l3> ();
                        playerScript1.scorevar = scorevar;
                        playerScript1.scorevar_1 = scorevar1;
                        playerScript1.scorevar_2 = scorevar3;
                        playerScript1.scorevar_3 = scorevar_1;
                        playerScript1.restartStopwatch ();
                        playerScript1.flag++;
                        self.SetActive (false);
                        return;
                    }
                    if (scorevar_1 == 48 && scorevar_2 == 20) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }
                    down.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("LevelManager1");
                    game7l2 playerScript = thePlayer.GetComponent<game7l2> ();
                    playerScript.scorevar = scorevar;
                    playerScript.scorevar_2 = scorevar1;
                    playerScript.scorevar_3 = scorevar3;
                    playerScript.scorevar_1 = scorevar_2;
                    playerScript.restartStopwatch ();
                    playerScript.flag++;
                    self.SetActive (false);
                    return;
                }
            }

            if (var != 13) {
                if (scorevar == 68) {
                    SettingsPenal.gameover ();
                    Gameover.SetActive (true);
                    Invoke ("endanimation", 6.5f);
                    return;
                }
                if (scorevar2 == 20) {
                    levelcomanim.SetActive (true);
                    SettingsPenal.playanimation ();
                    Invoke ("endanim", 3f);
                    Invoke ("ddown", 3.3f);
                    return;

                }
                if (indicator == 9) {

                    down.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("LevelManager1");
                    game3l2 playerScript = thePlayer.GetComponent<game3l2> ();
                    playerScript.scorevar = scorevar;
                    playerScript.restartStopwatch ();
                    if (playerScript.scorevar1 < 8) {
                        playerScript.indicator = 0;
                        playerScript.flag++;
                    } else if (playerScript.scorevar1 == 8 && playerScript.scorevar2 < 20) {
                        playerScript.indicator = 4;
                        playerScript.flag += 2;
                    } else if (playerScript.scorevar1 == 8 && playerScript.scorevar2 == 20 && playerScript.scorevar3 < 20) {
                        playerScript.indicator = 9;
                        playerScript.flag += 3;
                    } else if (playerScript.scorevar1 == 8 && playerScript.scorevar2 == 20 && playerScript.scorevar3 == 20) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }

                    playerScript.scorevar0 = scorevar2;
                    self.SetActive (false);
                    return;
                }
            }

            if (var == 12) {
                if (scorevar >= 40) {
                    SettingsPenal.gameover ();
                    Gameover.SetActive (true);
                    Invoke ("endanimation", 6.5f);
                    return;
                }
                if (scorevar2 == 10) {
                    levelcomanim.SetActive (true);
                    SettingsPenal.playanimation ();
                    Invoke ("endanim", 3f);
                    Invoke ("ddown1", 3.3f);
                    return;

                }

                if (indicator == 4) {
                    if (scorevar_2 == 20) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }
                    down.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("LevelManager2");
                    game6l3 playerScript = thePlayer.GetComponent<game6l3> ();
                    playerScript.scorevar = scorevar;
                    playerScript.scorevar_1 = scorevar2;
                    playerScript.scorevar_2 = scorevar_1;
                    playerScript.restartStopwatch ();
                    playerScript.flag++;

                    self.SetActive (false);
                    return;
                }
            }

            dropped = 0;
            anf = 90;
            a = -1;
            if (game7 == 5) {

                if ((indicator >= 0 && indicator <= 4 && scorevar1 == 10)) {

                    levelcomanim.SetActive (true);
                    SettingsPenal.playanimation ();
                    leftarrow.SetActive (false);
                    StartCoroutine (wait ());
                    if (indicator == 5) {
                        StartCoroutine (ShowMessage (3.5f));
                    }
                    return;

                } else if (indicator == 4) {
                    if (scorevar3 == 20 && scorevar_1 < 48) {
                        down.SetActive (true);
                        GameObject thePlayer = GameObject.Find ("LevelManager1");
                        game7l2 playerScript = thePlayer.GetComponent<game7l2> ();
                        playerScript.scorevar = scorevar;
                        playerScript.scorevar_2 = scorevar1;
                        playerScript.scorevar_3 = scorevar3;
                        playerScript.scorevar_1 = scorevar_2;
                        playerScript.restartStopwatch ();
                        playerScript.flag++;
                        self.SetActive (false);
                        return;
                    }
                    if (scorevar3 == 20 && scorevar_1 == 48 && scorevar_2 < 20) {
                        down1.SetActive (true);
                        GameObject thePlayer1 = GameObject.Find ("LevelManager2");
                        game7l3 playerScript1 = thePlayer1.GetComponent<game7l3> ();
                        playerScript1.scorevar = scorevar;
                        playerScript1.scorevar_1 = scorevar1;
                        playerScript1.scorevar_2 = scorevar3;
                        playerScript1.scorevar_3 = scorevar_1;
                        playerScript1.restartStopwatch ();
                        playerScript1.flag++;
                        self.SetActive (false);
                        return;
                    }
                    if (scorevar3 == 20 && scorevar_1 == 48 && scorevar_2 == 20) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }
                    StartCoroutine (ShowMessage (0.5f));
                }

            }

            animation ();
            indicator++;
            // right.SetActive (record[indicator]);
            // right.transform.position = objects[((indicator + 1) * 3) - 1].transform.position;

            Invoke ("render", 0.5f);
            Invoke ("waiting", 0.5f);

            // render();
        }

    }
    IEnumerator wait () {
        yield return new WaitForSeconds (3);
        endanim ();
        if (scorevar3 == 20 && scorevar_1 < 48) {
            down.SetActive (true);
            GameObject thePlayer = GameObject.Find ("LevelManager1");
            game7l2 playerScript = thePlayer.GetComponent<game7l2> ();
            playerScript.leftarrow.SetActive (false);
            playerScript.scorevar = scorevar;
            playerScript.scorevar_2 = scorevar1;
            playerScript.scorevar_3 = scorevar3;
            playerScript.scorevar_1 = scorevar_2;
            playerScript.restartStopwatch ();
            playerScript.flag++;
            self.SetActive (false);
            yield break;
        }
        if (scorevar3 == 20 && scorevar_1 == 48 && scorevar_2 < 20) {
            down1.SetActive (true);
            GameObject thePlayer1 = GameObject.Find ("LevelManager2");
            game7l3 playerScript1 = thePlayer1.GetComponent<game7l3> ();
            playerScript1.leftarrow.SetActive (false);
            playerScript1.scorevar = scorevar;
            playerScript1.scorevar_1 = scorevar1;
            playerScript1.scorevar_2 = scorevar3;
            playerScript1.scorevar_3 = scorevar_1;
            playerScript1.restartStopwatch ();
            playerScript1.flag++;
            self.SetActive (false);
            yield break;
        }

        animation ();
        indicator = 5;

        // right.SetActive (record[indicator]);
        // right.transform.position = objects[((indicator + 1) * 3) - 1].transform.position;
        Invoke ("render", 0.5f);
        Invoke ("waiting", 0.5f);

    }
    async public void onclickleft () {
        //change
        // timer.Restart ();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference ("users/" + username + "/" + game + "/s" + session + "/q" + (indicator + qh))
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

            if (var == 12) {
                if ((indicator == 0 && scorevar_1 < 10) || (scorevar2 == 10 && scorevar_1 < 10)) {
                    up.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("LevelManager");
                    Game6l1 playerScript = thePlayer.GetComponent<Game6l1> ();
                    playerScript.scorevar = scorevar;
                    playerScript.scorevar_1 = scorevar_2;
                    playerScript.scorevar_2 = scorevar2;
                    playerScript.flag++;
                    playerScript.restartStopwatch ();
                    self.SetActive (false);
                    return;
                } else if (indicator == 1 && scorevar_1 == 10) {
                    leftarrow.SetActive (false);
                }
            }

            if (game7 == 5) {
                if (indicator == 6 && scorevar1 == 10) {
                    leftarrow.SetActive (false);
                }
                if (indicator == 5) {
                    StartCoroutine (ShowMessage2 (0.5f));

                }
            }
            if (indicator == 0) {
                SceneManager.LoadScene ("Game Selection");

            }

            dropped = 0;
            anf = 270;
            a = 1;
            animation ();
            if (indicator != 0) {
                indicator--;
                Debug.Log (indicator);
                // if (indicator == 3)
                // {
                //     randomize();
                // }

                // setInitialPosition();
                Invoke ("render", 0.5f);

                // render();
                Invoke ("waiting", 0.5f);

            }

        }

    }
    public void waiting () {
        right.SetActive (record[indicator]);
        right.transform.position = objects[((indicator + 1) * 3) - 1].transform.position;
    }
    public void animation () {
        for (int i = 0; i < cards.Length; i++) {
            cards[i].transform.DORotate (new Vector3 (0, cards[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);

            objects[i + ((indicator) * 3)].transform.DORotate (new Vector3 (0, objects[i + ((indicator) * 3)].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
        }

        right.transform.DORotate (new Vector3 (0, right.transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);

        if (game7 == 5) {
            for (int i = 0; i < cards2.Length; i++) {
                cards2[i].transform.DORotate (new Vector3 (0, cards2[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);

            }

        }
    }

    private void render () {
        if (objects2.Length > 0) {
            for (int i = 0; i < objects2.Length; i++) {
                objects2[i].SetActive (false);
            }
            objects2[indicator].SetActive (true);
        }

        for (int i = 0; i < objects.Length; i++) {
            // if (i < 3)
            // {
            //     // cards2[i].SetActive(true);
            // }
            objects[i].SetActive (false);
            // objects2[i].SetActive(false);
            if ((indicator == 0) && (i >= 0 && i < 3)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);

            } else if ((indicator == 1) && (i >= 3 && i < 6)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 2) && (i >= 6 && i < 9)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 3) && (i >= 9 && i < 12)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 4) && (i >= 12 && i < 15)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 5) && (i >= 15 && i < 18)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 6) && (i >= 18 && i < 21)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 7) && (i >= 21 && i < 24)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 8) && (i >= 24 && i < 27)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 9) && (i >= 27 && i < 30)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 10) && (i >= 30 && i < 33)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 11) && (i >= 33 && i < 36)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 12) && (i >= 36 && i < 39)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 13) && (i >= 39 && i < 42)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            } else if ((indicator == 14) && (i >= 42 && i < 45)) {
                objects[i].SetActive (true);
                // objects2[i].SetActive(true);
            }

            if (indicator >= 0) {
                objects[i].transform.eulerAngles = new Vector3 (objects[i].transform.eulerAngles.x, anf, objects[i].transform.eulerAngles.z);
                right.transform.eulerAngles = new Vector3 (right.transform.eulerAngles.x, anf, right.transform.eulerAngles.z);

            }
            // if (objects[i].transform.position == objects2[i].transform.position)
            // {
            //     var++;
            //     Debug.Log(var);
            //     objects2[i].SetActive(false);
            // }
        }
        if (indicator >= 0) {
            animation ();
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
    IEnumerator ShowMessage (float delay) {
        Debug.Log ("i am here");
        Stagecomplete.text = "" + Stagetext;
        yield return new WaitForSeconds (delay);
        ltext.SetActive (true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds (2f);
        ltext.SetActive (false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }

    IEnumerator ShowMessage2 (float delay) {

        Stagecomplete.text = "" + Stagetext2;
        yield return new WaitForSeconds (delay);
        ltext.SetActive (true);
        yield return new WaitForSeconds (2f);
        ltext.SetActive (false);
    }

    void Update () {
        if (datacheck == 2 || datacheck == 7) {
            resumePop.SetActive (false);
            waiting ();
            jump ();
            datacheck = 0;
        }
        fortitle.text = "" + title[indicator];

        if (flag == 1) {
            StartCoroutine (ShowMessage (0.5f));
            flag--;
        }

        // if (var == 12) {
        //     if (scorevar2 == 10 && scorevar_2 > 0) {
        //         onclickleft ();
        //     }
        // }
        newText.text = "" + scorevar;

        if (var == 12) {
            if (scorevar >= 10) {
                stars[0].SetActive (true);
            }
            if (scorevar >= 20) {
                stars[1].SetActive (true);
            }
            if (scorevar >= 30) {
                stars[2].SetActive (true);
            }
            if (scorevar >= 40) {
                stars[3].SetActive (true);
            }
        } else if (var == 13) {
            if (scorevar >= 20) {
                stars[0].SetActive (true);
            }
            if (scorevar >= 40) {
                stars[1].SetActive (true);
            }
            if (scorevar >= 60) {
                stars[2].SetActive (true);
            }
            if (scorevar >= 80) {
                stars[3].SetActive (true);
            }
        } else {
            if (scorevar >= 17) {
                stars[0].SetActive (true);
            }
            if (scorevar >= 34) {
                stars[1].SetActive (true);
            }
            if (scorevar >= 51) {
                stars[2].SetActive (true);
            }
            if (scorevar >= 68) {
                stars[3].SetActive (true);
            }
        }

    }
}

//change
class AtemptClassification {
    public string reposnseTime;
    public string status;
    public string question;
    public string answer;
    public string timeStamp;

    public AtemptClassification () { }

    public AtemptClassification (string reposnseTime, string status, string question, string answer) {
        this.reposnseTime = reposnseTime;
        this.status = status;
        this.question = question;
        this.answer = answer;
        this.timeStamp = DateTime.Now.ToString ("MM/dd/yyyy HH:mm:ss");
    }
    public AtemptClassification (string reposnseTime, string status) {
        this.reposnseTime = reposnseTime;
        this.status = status;
    }
}
/////

class Resume {
    public string positions;
    public string positions1;
    public string solved;
    public int score;
    public int score1;
    public int score2;
    public int score3;
    public int score4;
    public int score5;
    public int score6;
    public int score7;

}