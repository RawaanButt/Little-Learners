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

public class SortingS2 : MonoBehaviour {
    public GameObject[] cards;
    public GameObject[] cards2;
    public GameObject[] objects;
    public GameObject[] objects2;
    public Vector2[] inipos;
    public int[] dropped;
    public int indicator;
    public static System.Random r = new System.Random ();
    public int var;
    public GameObject[] stars;
    public int number;
    public string alphabet;

    public GameObject self;
    public GameObject up, up1;
    public GameObject down;

    public int scorevar = 0;
    public int scorevar2 = 0;
    public Text newText;
    public int flag;
    public Text Stagecomplete;
    public string[] Stagetext;
    public GameObject ltext;
    public GameObject finaltext, leftarrow;
    public int scorevar_1 = 0;
    public int scorevar_2 = 0;
    public int scorevar_3 = 0;
    public int scorevar_4 = 0;
    public int scorevar_5 = 0;
    public int scorevar_6 = 0;

    public GameObject Gameover;

    public Text header;
    public string headertext;
    public string headertext1;
    public string headertext2;
    public string headertext3;

    public int anf;
    public int a;
    public GameObject right;

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

    private string solved = "";
    private string postions = "";
    private string gType = "none";

    public GameObject resumePop;
    private DatabaseReference reference;

    public int[] attemptNos;

    // public Score other;
    string sPostions = "";
    string sSolved = "";
    int sv = 0;

    void Start () {
        //change
        qh = 12;

        dragLocker = true;
        username = File.ReadAllText (Application.dataPath + "/user.txt");
        /////

        sources[0] = "matchbox";
        sources[1] = "blue cup";
        sources[2] = "book";
        sources[3] = "fan";
        sources[4] = "chair";
        sources[5] = "chair";
        sources[6] = "fan";
        sources[7] = "book";
        sources[8] = "blue cup";
        sources[9] = "matchbox";
        sources[10] = "bnana";
        sources[11] = "shoe";
        sources[12] = "table";
        sources[13] = "car";
        sources[14] = "bus";
        sources[15] = "bus";
        sources[16] = "car";
        sources[17] = "table";
        sources[18] = "shoe";
        sources[19] = "bnana";
        sources[20] = "golf ball";
        sources[21] = "tennis ball";
        sources[22] = "cricket ball";
        sources[23] = "raghbi";
        sources[24] = "football";
        sources[25] = "tooth pic";
        sources[26] = "ladle";
        sources[27] = "bat";
        sources[28] = "ladder";
        sources[29] = "tawer";
        sources[30] = "baby";
        sources[31] = "school boy";
        sources[32] = "young girl";
        sources[33] = "middleage man";
        sources[34] = "old lady";
        sources[35] = "apple";
        sources[36] = "butter";
        sources[37] = "duck";
        sources[38] = "eagle";
        sources[39] = "fish";
        sources[40] = "grapes";
        sources[41] = "honey";
        sources[42] = "ice";
        sources[43] = "juice";
        sources[44] = "carrot";
        sources[45] = "cat";
        sources[46] = "cotton";
        sources[47] = "cup";
        sources[48] = "cute";
        sources[49] = "dark";
        sources[50] = "dinner";
        sources[51] = "dive";
        sources[52] = "dove";
        sources[53] = "duck";

        indicator = 0;
        render ();
        randomize ();

        var = -5;
        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive (false);
        }

        // other = new Score();
        if (alphabet == "abc") {

            self.SetActive (false);
        }
        scorevar = 0;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        /////
    }
    IEnumerator Upload (String user, String gameType, String postions) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("gameType", gameType);
        form.AddField ("positions", postions);

        using (UnityWebRequest www = UnityWebRequest.Post ("https://evening-fortress-14821.herokuapp.com/sorting", form)) {
            yield return www.SendWebRequest ();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log (www.error);
            } else {
                Debug.Log ("Form upload complete!");
            }
        }
    }
    IEnumerator updateScore (String user, String gameType, int score, int score2, String solved) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("gameType", gameType);
        form.AddField ("score", score);
        form.AddField ("score2", score2);
        form.AddField ("solved", solved);

        using (UnityWebRequest www = UnityWebRequest.Post ("https://evening-fortress-14821.herokuapp.com/sorting/score", form)) {
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
                Debug.Log (pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                Resume myObject = JsonUtility.FromJson<Resume> (webRequest.downloadHandler.text);
                sPostions = myObject.positions;
                // if(myObject.solved.length > 0){

                // }
                Debug.Log (myObject);
                Debug.Log (myObject.positions);
                Debug.Log ("Got data");
                // load data
                // load positions
                scorevar = sv;
                sPostions = myObject.positions;

                if (myObject.solved != null) {

                    sSolved = myObject.solved.Length > 0 ? myObject.solved : sSolved;
                }
                if (myObject.score != null) {
                    scorevar = myObject.score;
                }
                if (myObject.score2 != null) {
                    scorevar2 = myObject.score2;
                }

                if (sPostions.Length > 0) {
                    string[] posPairs = sPostions.Split (',');
                    foreach (string posPair in posPairs) {
                        string[] post = posPair.Split ('-');
                        // Int32.Parse(post[0])
                        objects[Int32.Parse (post[0])].transform.position = cards[Int32.Parse (post[1])].transform.position;
                    }
                }

                if (sSolved.Length > 0) {
                    solved = sSolved;
                    string[] sols = sSolved.Split (',');
                    foreach (string sol in sols) {
                        // Int32.Parse(sol)
                        dropped[(int) Decimal.Truncate (Int32.Parse (sol) / 5)]++;
                        objects[Int32.Parse (sol)].transform.position = objects2[Int32.Parse (sol)].transform.position;

                    }
                }

                // if (onquestionNo >= 12)
                // {
                //     indicator = onquestionNo - qh;
                //     render();
                // }
                // else
                // {
                GameObject thePlayer = GameObject.Find ("LevelManager1");
                SortingS1 playerScript = thePlayer.GetComponent<SortingS1> ();
                playerScript.scorevar_4 = scorevar2;
                playerScript.datacheck++;
                Debug.Log ("sorting s2 data received");
                self.SetActive (false);
                // }

                setInitialPosition ();
                render ();

                timer = new Stopwatch ();
                timer.Start ();
                resumePop.SetActive (false);

            }
        }
    }
    public async void onStarting (bool resume, int sess) {
        //change
        session = sess;

        if (resume) {
            reference.Child ("users").Child (username).Child ("savedState").Child ("test2").SetValueAsync ("this is tets");
        }

        // // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/sorting")
        //     .GetValueAsync().ContinueWith(task =>
        //     {
        //         if (task.IsFaulted)
        //         {
        //             // Handle the error...
        //         }
        //         else if (task.IsCompleted)
        //         {

        //             DataSnapshot snapshot = task.Result;
        //             int sess = 0;
        //             if (snapshot.Exists && snapshot.ChildrenCount > 0)
        //             {
        //                 // Do something with snapshot...
        //                 foreach (var childSnapshot in snapshot.Children)
        //                 {
        //                     if (Int32.Parse(childSnapshot.Key.Substring(1)) > sess)
        //                         sess = Int32.Parse(childSnapshot.Key.Substring(1));
        //                 }
        //             }
        //             if (!resume)
        //             {
        //                 sess++;

        //             }
        //             session = sess;

        //         }
        //     });
        // // Debug.Log(session);

        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/sorting/s" + session)
        //     .GetValueAsync().ContinueWith(task =>
        //     {
        //         if (task.IsFaulted)
        //         {
        //             // Handle the error...
        //         }
        //         else if (task.IsCompleted)
        //         {

        //             DataSnapshot snapshot = task.Result;
        //             if (snapshot.Exists && snapshot.ChildrenCount > 0)
        //             {
        //                 // Do something with snapshot...
        //                 foreach (var childSnapshot in snapshot.Children)
        //                 {
        //                     if (childSnapshot.Key.Substring(1) != "avedState")
        //                     {
        //                         int q = Int32.Parse(childSnapshot.Key.Substring(1));
        //                         if (q >= 13)
        //                         {
        //                             int att = 0;
        //                             foreach (var childSnapshot2 in childSnapshot.Children)
        //                             {
        //                                 if (Int32.Parse(childSnapshot2.Key.Substring(1)) > att)
        //                                     att = Int32.Parse(childSnapshot2.Key.Substring(1));
        //                             }

        //                             attemptNos[q - qh - 1] = att;
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //     });
        // // Debug.Log(attemptNo);
        gType = "objects";

        if (resume) {
            // get data

            // load data
            // load positions
            // scorevar = sv;
            StartCoroutine (GetRequest ("https://evening-fortress-14821.herokuapp.com/sorting/" + username + "/" + gType));

        } else {
            //save positions
            // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
            // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child(gType).Child("positions").SetValueAsync(postions);
            StartCoroutine (Upload (username, gType, postions));
            self.SetActive (false);
            setInitialPosition ();

            timer = new Stopwatch ();
            timer.Start ();
            resumePop.SetActive (false);

        }

        /////
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
    public void restartStopwatch () {
        timer.Restart ();

    }
    // public void Increment()
    // {
    //     scorevar = scorevar + 2;

    // }
    public void setInitialPosition () {
        int j = 0;
        int x = 0;
        // if(indicator > 4){
        x = indicator;
        // }else {
        //     x = indicator;
        // }
        for (int i = x * 5; i < x * 5 + 5; i++) {
            inipos[j] = objects[i].transform.position;
            j++;
        }
        j = 0;
    }
    public void randomize () {
        for (int i = 0; i < 55; i += 5) {

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

                // int pos2 = random_except_list(5, termsList2.ToArray());
                // termsList2.Add(pos2);
                // termsList2.Sort();
                // objects2[i + j].transform.position = cards2[pos2].transform.position;
            }
        }
    }
    public void endanimation () {

        SceneManager.LoadScene ("Game Selection");
    }
    public void onclickright () {

        //change
        timer.Restart ();

        /////
        if (DOTween.TotalPlayingTweens () == 0) {
            if (indicator == 10 && scorevar < 230) {
                StartCoroutine (ShowMessage1 (0.5f));
                return;
            } else if (scorevar >= 230) {
                SettingsPenal.gameover ();
                Gameover.SetActive (true);
                Invoke ("endanimation", 6.5f);
                return;
            }

            anf = 90;
            a = -1;
            animation ();
            indicator++;
            // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
            // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child("onquestionNo").SetValueAsync((indicator + qh).ToString());

            setInitialPosition ();
            Invoke ("render", 0.5f);
        }

    }
    public void onclickleft () {
        //change
        timer.Restart ();

        ////

        if (DOTween.TotalPlayingTweens () == 0) {
            if (indicator == 0 || scorevar2 == 110) {

                if (scorevar_4 < 20 || scorevar_5 < 20 || scorevar_6 < 20) {
                    up.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("LevelManager");
                    SortingS1 playerScript = thePlayer.GetComponent<SortingS1> ();
                    if (scorevar_6 < 20) {
                        playerScript.flag += 2;
                    }
                    if (scorevar_6 == 20 && scorevar_5 < 20) {
                        if (playerScript.indicator != 3) {
                            playerScript.indicator = 4;
                            playerScript.onclickleft ();
                            playerScript.flag += 3;
                        }

                    }
                    if (scorevar_6 == 20 && scorevar_5 == 20 && scorevar_4 < 20) {
                        if (playerScript.indicator != 1) {
                            playerScript.indicator = 2;
                            playerScript.onclickleft ();
                            playerScript.flag += 4;

                        }
                    }
                    playerScript.scorevar = scorevar;
                    playerScript.scorevar_1 = scorevar_1;
                    playerScript.scorevar_2 = scorevar_2;
                    playerScript.scorevar_3 = scorevar_3;
                    playerScript.scorevar_4 = scorevar2;

                    playerScript.restartStopwatch ();
                    self.SetActive (false);
                    return;
                } else if (scorevar_1 < 20 || scorevar_2 < 20 || scorevar_3 < 20) {
                    up1.SetActive (true);
                    GameObject thePlayer1 = GameObject.Find ("LevelManager1");
                    SortingS1 playerScript1 = thePlayer1.GetComponent<SortingS1> ();
                    if (scorevar_3 < 20) {
                        playerScript1.flag++;
                    }
                    if (scorevar_3 == 20 && scorevar_2 < 20) {
                        if (playerScript1.indicator != 3) {
                            playerScript1.indicator = 4;
                            playerScript1.onclickleft ();
                            playerScript1.flag += 3;
                        }

                    }
                    if (scorevar_3 == 20 && scorevar_2 == 20 && scorevar_1 < 20) {
                        if (playerScript1.indicator != 1) {
                            playerScript1.indicator = 2;
                            playerScript1.onclickleft ();
                            playerScript1.flag += 4;
                        }
                    }
                    playerScript1.scorevar = scorevar;
                    playerScript1.scorevar_1 = scorevar_4;
                    playerScript1.scorevar_2 = scorevar_5;
                    playerScript1.scorevar_3 = scorevar_6;
                    playerScript1.scorevar_4 = scorevar2;
                    playerScript1.restartStopwatch ();
                    self.SetActive (false);
                    return;
                }

                // if (number == 123)
                // {
                //     SceneManager.LoadScene("Game Selection", LoadSceneMode.Single);
                //     return;
                // }
                // if (alphabet == "abc")
                // {
                //     up.SetActive(true);
                //     GameObject thePlayer = GameObject.Find("Levelmanager");
                //     LevelManager playerScript = thePlayer.GetComponent<LevelManager>();
                //     playerScript.scorevar = scorevar;
                //     self.SetActive(false);
                //     // play2.SetActive(false);
                //     return;
                // }
            }

            if (indicator == 1 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_6 == 20 && scorevar_5 == 20 && scorevar_4 == 20) {
                leftarrow.SetActive (false);
            }

            var --;

            anf = 270;
            a = 1;
            animation ();
            if (indicator != 0) {
                indicator--;
                // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
                // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                // reference.Child("users").Child(username).Child("sorting").Child("s" + session).Child("savedState").Child("onquestionNo").SetValueAsync((indicator + qh).ToString());
                // if (indicator == 3)
                // {
                //     randomize();
                // }

                setInitialPosition ();
                Invoke ("render", 0.5f);

                // render();

            }
            for (int i = 0; i < 20; i++) {
                if (objects[i].transform.position == objects2[i].transform.position) {
                    objects2[i].SetActive (false);
                }
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

    private void render () {
        for (int i = 0; i < 55; i++) {
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
            } else if ((indicator == 8) && (i >= 40 && i < 45)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 9) && (i >= 45 && i < 50)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 10) && (i >= 50 && i < 55)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            }
            if (indicator >= 0) {
                objects[i].transform.eulerAngles = new Vector3 (objects[i].transform.eulerAngles.x, anf, objects[i].transform.eulerAngles.z);
                objects2[i].transform.eulerAngles = new Vector3 (objects[i].transform.eulerAngles.x, anf, objects[i].transform.eulerAngles.z);

            }

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
            if (Vector3.Distance (objects[a].transform.position, objects2[p].transform.position) < 10) {
                destination = ((p % 5) + 1).ToString ();
            }
        }

        float Distance = Vector3.Distance (objects[a].transform.position, objects2[a].transform.position);
        if (Distance < 10) {
            destination = ((a % 5) + 1).ToString ();
            objects[a].transform.position = objects2[a].transform.position;
            //  objects2[a].GetActive

            scorevar = scorevar + 2;
            scorevar2 = scorevar2 + 2;
            SettingsPenal.playeffect (0);

            // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
            // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child("scorevar").SetValueAsync(scorevar);

            if (objects2[a].active) {
                dropped[indicator]++;
                // Increment();
            }
            objects2[a].SetActive (false);

            //  if(objects[a].transform.position==objects2[a].transform.position)
            // {

            //     return;
            // }
            //change
            status = "right";
            //////
            //resume
            if (solved.Length > 0) {
                solved = solved + "," + a.ToString ();
            } else {
                solved = a.ToString ();
            }
            // Debug.Log(solved);
            string[] tokens = solved.Split (',');
            foreach (string token in tokens) {
                // Debug.Log("solved:" + token);
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
        }
        // }
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
    void saveAttempt (string reposnseTime, string dragTime, string status, string dragDistance, string dropDistance, string source, string destination) {

        // Debug.Log("in funtionn");
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // Atempt atempt = new Atempt(reposnseTime, dragTime, status, dragDistance, dropDistance, source, destination);
        // string json = JsonUtility.ToJson(atempt);
        // // Debug.Log("qh");
        // // Debug.Log(qh);
        // attemptNos[indicator]++;

        // reference.Child("users").Child(username).Child("sorting").Child("s" + session).Child("q" + (indicator + 1 + qh)).Child("a" + attemptNos[indicator]).SetRawJsonValueAsync(json);

        int questionNo = indicator + 1 + qh;
        StartCoroutine (updateAnalytics (username, "sorting", session, questionNo, reposnseTime, dragTime, status, dragDistance, dropDistance, source, destination));

        if (status == "right") {

            // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child(gType).Child("solved").SetValueAsync(solved);
            StartCoroutine (updateScore (username, gType, scorevar, scorevar2, solved));
        }
    }
    /////

    void Update () {

        if (indicator == 0 || indicator == 2 || (indicator >= 4 && indicator <= 6)) {
            header.text = "" + headertext;
        } else if (indicator == 1 || indicator == 3) {
            header.text = "" + headertext1;

        } else {
            header.text = "" + headertext2;
        }
        newText.text = "" + scorevar;

        if (scorevar >= 60) {
            stars[0].SetActive (true);
        }
        if (scorevar >= 120) {
            stars[1].SetActive (true);
        }
        if (scorevar >= 180) {
            stars[2].SetActive (true);
        }
        if (scorevar >= 230) {
            stars[3].SetActive (true);
        }

        if (flag == 1) {
            StartCoroutine (ShowMessage (0, 0.5f));
            flag--;
        }
        // if (flag == 5)
        // {
        //     flaag();
        // }
        // newText.text = "" + scorevar;

    }
}