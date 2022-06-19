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

public class ObjectRenderer1 : MonoBehaviour {
    public GameObject[] cards;
    public GameObject[] cards2;
    public GameObject[] objects;
    public GameObject[] objects2;
    public Vector2[] inipos;
    public int[] dropped;
    public int indicator;
    public GameObject self;
    public GameObject up;
    public GameObject up1;
    public GameObject up2;
    public GameObject up3;
    public GameObject down;
    public static System.Random r = new System.Random ();
    public int scorevar;
    public int scorevar2;

    public int scorevar_1 = 0;
    public int scorevar_2 = 0;
    public int scorevar_3 = 0;
    public int scorevar_4 = 0;
    public int scorevar_5 = 0;
    public int scorevar_6 = 0;

    public Text newText;
    public int flag;
    public int anf;
    public int a;
    public GameObject[] stars;
    public GameObject rightarrow;
    public GameObject Gameover;
    public int flaagg;
    public Text Stagecomplete;
    public string[] Stagetext;
    public GameObject ltext;
    public GameObject finaltext;
    public GameObject leftarrow;

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
    public string gameMode = "5";

    string sPostions = "";
    string sPostions1 = "";
    string sSolved = "";
    private string solved = "";
    private string postions = "";
    private string postions1 = "";
    public GameObject resumePop;

    async void Start () {
        timer = new Stopwatch ();

        //change
        qh = 22;
        sources[0] = "apple";
        sources[1] = "banana";
        sources[2] = "bat";
        sources[3] = "football";
        sources[4] = "ice cream";
        sources[5] = "book";
        sources[6] = "bricks";
        sources[7] = "candy";
        sources[8] = "catepilar";
        sources[9] = "watermelon";
        dragLocker = true;
        username = File.ReadAllText (Application.dataPath + "/user.txt");
        /////
        // self.SetActive (false);

        indicator = 0;
        render ();
        randomize ();

        setInitialPosition ();
        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive (false);
        }
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
                    scorevar2 = myObject.score1;
                    scorevar_1 = myObject.score2;
                    scorevar_2 = myObject.score3;
                    scorevar_3 = myObject.score4;
                    scorevar_4 = myObject.score5;
                    scorevar_5 = myObject.score6;
                    scorevar_6 = myObject.score7;
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
                GameObject thePlayer4 = GameObject.Find ("LevelManager");
                ObjectRenderer playerScript4 = thePlayer4.GetComponent<ObjectRenderer> ();
                playerScript4.scorevar_5 = scorevar2;
                playerScript4.datacheck++;
                setInitialPosition ();
                render ();
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
        if (resume) {
            StartCoroutine (GetRequest ("https://evening-fortress-14821.herokuapp.com/select/" + username + "/" + game + "/" + gameMode));
        } else {
            Debug.Log ("Object");

            Debug.Log (gameMode);
            StartCoroutine (Upload (username, game, gameMode, postions, postions1));

            self.SetActive (false);

            // timer = new Stopwatch();
            timer.Start ();
            resumePop.SetActive (false);
        }
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
    IEnumerator ShowMessage (int number, float delay) {
        Stagecomplete.text = "" + Stagetext[number];
        yield return new WaitForSeconds (delay);
        ltext.SetActive (true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds (2f);
        ltext.SetActive (false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }
    IEnumerator ShowMessage1 (float delay) {
        yield return new WaitForSeconds (delay);
        finaltext.SetActive (true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds (2f);
        finaltext.SetActive (false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }

    public void setInitialPosition () {
        int j = 0;
        int x = 0;
        // if(indicator > 4){
        x = indicator % 4;
        // }else {
        //     x = indicator;
        // }
        for (int i = x * 5; i < x * 5 + 5; i++) {
            inipos[j] = objects[i].transform.position;
            j++;
        }
        j = 0;
    }
    void flaag () {
        up.SetActive (true);
        GameObject thePlayer = GameObject.Find ("Levelmanager1");
        LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
        scorevar = playerScript.scorevar;

        scorevar_1 = playerScript.scorevar_2;
        scorevar_2 = playerScript.scorevar_3;
        scorevar_3 = playerScript.scorevar_4;
        scorevar_4 = playerScript.scorevar_5;
        scorevar_5 = playerScript.scorevar_6;
        scorevar_6 = playerScript.scorevar2;

        playerScript.restartStopwatch ();
        flag++;
        up.SetActive (false);
        return;
    }

    public void randomize () {
        for (int i = 0; i < 10; i += 5) {

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
    public void endanimation () {
        // Gameover.SetActive (false);
        SceneManager.LoadScene ("Game Selection");
    }
    async public void onclickright () {
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
        if (DOTween.TotalPlayingTweens () == 0) {
            leftarrow.SetActive (true);
            if (indicator == 1 && scorevar < 160) {
                StartCoroutine (ShowMessage1 (0.5f));
                return;
            }
            if (scorevar >= 160) {
                SettingsPenal.gameover ();
                Gameover.SetActive (true);
                Invoke ("endanimation", 6.5f);
                return;

            }
            // dropped = 0;
            anf = 90;
            a = -1;
            animation ();
            indicator++;
            setInitialPosition ();
            Invoke ("render", 0.5f);

            // render();

        }

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
            if (indicator == 0 || scorevar == 20) {
                if (scorevar_6 == 6 && scorevar_5 < 24) {
                    up1.SetActive (true);
                    GameObject thePlayer1 = GameObject.Find ("levelManager");
                    ObjectRenderer playerScript1 = thePlayer1.GetComponent<ObjectRenderer> ();
                    playerScript1.scorevar = scorevar;
                    playerScript1.scorevar_1 = scorevar_6;
                    playerScript1.scorevar_2 = scorevar2;
                    playerScript1.scorevar_3 = scorevar_1;
                    playerScript1.scorevar_4 = scorevar_2;
                    playerScript1.scorevar_5 = scorevar_3;
                    playerScript1.flaagg = 2;
                    playerScript1.restartStopwatch ();
                    self.SetActive (false);
                    return;

                } else if (scorevar_6 == 6 && scorevar_5 == 24 && scorevar_4 < 40) {
                    up1.SetActive (true);
                    GameObject thePlayer2 = GameObject.Find ("levelManager");
                    ObjectRenderer playerScript2 = thePlayer2.GetComponent<ObjectRenderer> ();
                    playerScript2.indicator = 4;
                    playerScript2.onclickleft ();
                    playerScript2.scorevar = scorevar;
                    playerScript2.scorevar_1 = scorevar_6;
                    playerScript2.scorevar_2 = scorevar2;
                    playerScript2.scorevar_3 = scorevar_1;
                    playerScript2.scorevar_4 = scorevar_2;
                    playerScript2.scorevar_5 = scorevar_3;
                    playerScript2.restartStopwatch ();
                    self.SetActive (false);
                    return;

                } else if (scorevar_6 == 6 && scorevar_5 == 24 && scorevar_4 == 40 && scorevar_3 < 6) {
                    up2.SetActive (true);
                    GameObject thePlayer3 = GameObject.Find ("Levelmanager");
                    LevelManager playerScript3 = thePlayer3.GetComponent<LevelManager> ();
                    playerScript3.scorevar = scorevar;
                    playerScript3.scorevar_1 = scorevar_4;
                    playerScript3.scorevar_2 = scorevar_5;
                    playerScript3.scorevar_3 = scorevar_6;
                    playerScript3.scorevar_4 = scorevar2;
                    playerScript3.scorevar_5 = scorevar_1;
                    playerScript3.scorevar_6 = scorevar_2;
                    playerScript3.flaagg++;
                    playerScript3.restartStopwatch ();
                    self.SetActive (false);
                    // play2.SetActive(false);
                    return;

                } else if (scorevar_6 == 6 && scorevar_5 == 24 && scorevar_4 == 40 && scorevar_3 == 6 && scorevar_2 < 24) {
                    up3.SetActive (true);

                    GameObject thePlayer4 = GameObject.Find ("LevelManager");
                    ObjectRenderer playerScript4 = thePlayer4.GetComponent<ObjectRenderer> ();
                    playerScript4.scorevar = scorevar;
                    playerScript4.scorevar_1 = scorevar_3;
                    playerScript4.scorevar_2 = scorevar_4;
                    playerScript4.scorevar_3 = scorevar_5;
                    playerScript4.scorevar_4 = scorevar_6;
                    playerScript4.scorevar_5 = scorevar2;
                    playerScript4.flaagg++;
                    playerScript4.restartStopwatch ();
                    self.SetActive (false);
                    // play2.SetActive(false);
                    return;

                } else if (scorevar_6 == 6 && scorevar_5 == 24 && scorevar_4 == 40 && scorevar_3 == 6 && scorevar_2 == 24 && scorevar_1 < 40) {
                    up3.SetActive (true);
                    GameObject thePlayer5 = GameObject.Find ("LevelManager");
                    ObjectRenderer playerScript5 = thePlayer5.GetComponent<ObjectRenderer> ();
                    playerScript5.indicator = 4;
                    playerScript5.onclickleft ();
                    playerScript5.scorevar = scorevar;
                    playerScript5.scorevar_1 = scorevar_3;
                    playerScript5.scorevar_2 = scorevar_4;
                    playerScript5.scorevar_3 = scorevar_5;
                    playerScript5.scorevar_4 = scorevar_6;
                    playerScript5.scorevar_5 = scorevar2;

                    playerScript5.restartStopwatch ();
                    self.SetActive (false);
                    // play2.SetActive(false);
                    return;
                }
                up.SetActive (true);
                GameObject thePlayer = GameObject.Find ("Levelmanager1");
                LevelManager playerScript = thePlayer.GetComponent<LevelManager> ();
                playerScript.scorevar = scorevar;
                playerScript.scorevar_1 = scorevar2;
                playerScript.scorevar_2 = scorevar_1;
                playerScript.scorevar_3 = scorevar_2;
                playerScript.scorevar_4 = scorevar_3;
                playerScript.scorevar_5 = scorevar_4;
                playerScript.scorevar_6 = scorevar_5;
                playerScript.flaagg++;

                playerScript.restartStopwatch ();
                self.SetActive (false);
                return;

            }
            // dropped = 0;
            anf = 270;
            a = 1;
            animation ();
            if (indicator != 0 || scorevar != 20) {
                if (scorevar_1 == 40 && scorevar_2 == 24 && scorevar_3 == 6 && scorevar_4 == 40 && scorevar_5 == 24 && scorevar_6 == 6 && indicator == 1) {
                    leftarrow.SetActive (false);
                }
                indicator--;
                if (indicator == 3) {
                    Invoke ("randomize", 0.5f);

                    // randomize();
                }
                setInitialPosition ();
                Invoke ("render", 0.5f);

                // render();
            }

        }

    }
    public void animation () {
        if (indicator <= 3) {
            // Debug.Log(indicator);
            for (int i = 0; i < cards.Length; i++) {
                cards[i].transform.DORotate (new Vector3 (0, cards[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
                objects[i + ((indicator) * 5)].transform.DORotate (new Vector3 (0, objects[i + ((indicator) * 5)].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
                cards2[i].transform.DORotate (new Vector3 (0, cards2[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
                objects2[i + ((indicator) * 5)].transform.DORotate (new Vector3 (0, objects2[i + ((indicator) * 5)].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
            }
        }
    }

    private void render () {
        for (int i = 0; i < 10; i++) {
            if (i < 5) {
                cards2[i].SetActive (true);
            }
            objects[i].SetActive (false);
            objects2[i].SetActive (false);
            if ((indicator == 0 || indicator == 4) && (i >= 0 && i < 5)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 1 || indicator == 5) && (i >= 5 && i < 10)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            }
            if (indicator >= 0) {

                objects[i].transform.eulerAngles = new Vector3 (objects[i].transform.eulerAngles.x, anf, objects[i].transform.eulerAngles.z);
                objects2[i].transform.eulerAngles = new Vector3 (objects[i].transform.eulerAngles.x, anf, objects[i].transform.eulerAngles.z);

            }
            // else if ((indicator == 2 || indicator == 6) && (i >= 10 && i < 15))
            // {
            //     objects[i].SetActive(true);
            //     objects2[i].SetActive(true);
            // }
            // else if ((indicator == 3 || indicator == 7) && (i >= 15 && i < 20))
            // {
            //     objects[i].SetActive(true);
            //     objects2[i].SetActive(true);
            // }
        }
        if (indicator >= 0) {
            // Debug.Log("Called");
            animation ();
        }

        if (indicator == 4) {
            cards2[0].SetActive (false);
            cards2[4].SetActive (false);
            for (int j = 0; j < 5; j++) {
                if ((cards2[0].transform.position == objects2[j].transform.position) || (cards2[4].transform.position == objects2[j].transform.position)) {
                    objects2[j].SetActive (false);
                }
            }
        } else if (indicator == 5) {
            cards2[0].SetActive (false);
            cards2[4].SetActive (false);
            for (int j = 5; j < 10; j++) {
                if ((cards2[0].transform.position == objects2[j].transform.position) || (cards2[4].transform.position == objects2[j].transform.position)) {
                    objects2[j].SetActive (false);
                }
            }
        }
        // else if (indicator == 6)
        // {
        //     cards2[0].SetActive(false);
        //     cards2[4].SetActive(false);
        //     for (int j = 10; j < 15; j++)
        //     {
        //         if ((cards2[0].transform.position == objects2[j].transform.position) || (cards2[4].transform.position == objects2[j].transform.position))
        //         {
        //             objects2[j].SetActive(false);
        //         }
        //     }
        // }
        // else if (indicator == 7)
        // {
        //     cards2[0].SetActive(false);
        //     cards2[4].SetActive(false);
        //     for (int j = 15; j < 20; j++)
        //     {
        //         if ((cards2[0].transform.position == objects2[j].transform.position) || (cards2[4].transform.position == objects2[j].transform.position))
        //         {
        //             objects2[j].SetActive(false);
        //         }
        //     }
        // }
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

    async public void Drag (int ob) {
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
        //////
        if (objects[ob].transform.position == objects2[ob].transform.position) {
            return;
        }
        var point = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        objects[ob].transform.position = new Vector3 (point.x, point.y, objects[ob].transform.position.z);

    }
    public void restartTimer () {
        timer.Restart ();
    }
    async public void drop (int a) {
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

        float Distance = Vector3.Distance (objects[a].transform.position, objects2[a].transform.position);
        if (Distance < 10) {
            destination = sources[a];

            objects[a].transform.position = objects2[a].transform.position;
            scorevar = scorevar + 2;
            scorevar2 = scorevar2 + 2;
            dropped[indicator]++;
            SettingsPenal.playeffect (0);

            //  objects2[a].GetActive

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
        //change
        saveAttempt (reposnseTime, dragTime, status, dragDistance, dropDistance, sources[a], destination);
        /////
        Debug.Log (dropped);

        if (dropped[indicator] == 5) {

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
            StartCoroutine (updateScore (username, game, gameMode, scorevar, scorevar2, scorevar_1, scorevar_2, scorevar_3, scorevar_4, scorevar_5, scorevar_6, solved));
        }
    }
    /////

    void Update () {
        if (flag == 5) {
            flaag ();

        }
        if (flaagg == 1) {
            StartCoroutine (ShowMessage (0, 0.5f));
            flaagg--;
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