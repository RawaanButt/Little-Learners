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

public class game7l2 : MonoBehaviour {
    public GameObject[] cards;
    public GameObject[] cards2;
    public GameObject[] objects;
    public GameObject[] objects2;
    public Vector2[] inipos;
    public int[] dropped;
    private int indicator;
    public static System.Random r = new System.Random ();
    public int var;
    public GameObject[] stars;
    public int number;
    public string alphabet;

    public GameObject self;
    public GameObject up;
    public GameObject down;

    public int scorevar = 0;
    public int scorevar2 = 0;
    public int scorevar1 = 0;
    public int scorevar_1 = 0;
    public int scorevar_2 = 0;
    public int scorevar_3 = 0;
    public GameObject Gameover;
    public GameObject leftarrow;

    public Text newText;
    public int flag;
    public string title;
    public Text fortitle;
    public int anf;
    public int a;
    public GameObject levelcomanim;
    public Text Stagecomplete;
    public string Stagetext;
    public GameObject ltext;
    public GameObject finaltext;
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

    private string postions = "";
    private string postions1 = "";
    private string solved = "";
    string sPostions = "";
    string sPostions1 = "";
    string sSolved = "";
    public GameObject resumePop;
    public string game = "association";

    // public Score other;

    async void Start () {
        //change
        qh = 15;

        sources[0] = "playground";
        sources[1] = "school";
        sources[2] = "hospital";
        sources[3] = "washroom";
        sources[4] = "zoo";
        sources[5] = "kitchen";
        sources[6] = "garden";
        sources[7] = "parking";
        sources[8] = "bedroom";
        sources[9] = "zoo";
        sources[10] = "school";
        sources[11] = "washroom";
        sources[12] = "kitchen";
        sources[13] = "bedroom";
        sources[14] = "garden";
        sources[15] = "hospital";
        sources[16] = "postoffice";
        sources[17] = "kitchen";
        sources[18] = "policestation";
        sources[19] = "school";
        sources[20] = "shoeshop";
        sources[21] = "airport";
        sources[22] = "school";
        sources[23] = "hospital";

        destinations[0] = "seasaw";
        destinations[1] = "books";
        destinations[2] = "medicine";
        destinations[3] = "toothpaste & toothbrush";
        destinations[4] = "lion";
        destinations[5] = "mom cooking";
        destinations[6] = "vegitables basket";
        destinations[7] = "car";
        destinations[8] = "sleeping baby";
        destinations[9] = "zebra";
        destinations[10] = "school bag";
        destinations[11] = "baby bath";
        destinations[12] = "clean dishes";
        destinations[13] = "pillow";
        destinations[14] = "swing";
        destinations[15] = "doctor";
        destinations[16] = "postman";
        destinations[17] = "mom cooking";
        destinations[18] = "police officer";
        destinations[19] = "teacher";
        destinations[20] = "shoe maker";
        destinations[21] = "pilot";
        destinations[22] = "student";
        destinations[23] = "nurse";

        dragLocker = true;
        username = File.ReadAllText (Application.dataPath + "/user.txt");
        /////

        // self.SetActive(false);

        indicator = 0;
        render ();
        randomize ();

        setInitialPosition ();
        var = -5;
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
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/association")
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
        //             sess++;
        //             session = sess;

        //         }
        //     });
        // // Debug.Log(session);

        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/association/s" + session + "/q" + (indicator + 1 + qh))
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

        // timer = new Stopwatch();
        // timer.Start();

        /////
    }

    IEnumerator Upload (String user, String game, String gameMode, String positions) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("game", game);
        form.AddField ("gameMode", gameMode);
        form.AddField ("positions", positions);
        // form.AddField("positions1", positions1);

        using (UnityWebRequest www = UnityWebRequest.Post ("https://evening-fortress-14821.herokuapp.com/select", form)) {
            yield return www.SendWebRequest ();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log (www.error);
            } else {
                Debug.Log ("Form upload complete!");
            }
        }
    }
    IEnumerator updateScore (String user, String game, String gameMode, int score, int score1, int score2, int score3, int score4, int score5, String solved) {
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
                // sPostions1 = myObject.positions1;

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
                }

                if (sPostions.Length > 0) {
                    string[] posPairs = sPostions.Split (',');
                    foreach (string posPair in posPairs) {
                        string[] post = posPair.Split ('-');
                        objects[Int32.Parse (post[0])].transform.position = cards[Int32.Parse (post[1])].transform.position;
                    }
                }
                // if (sPostions1.Length > 0)
                // {
                //     string[] posPairs1 = sPostions1.Split(',');
                //     foreach (string posPair1 in posPairs1)
                //     {
                //         string[] post1 = posPair1.Split('-');
                //         objects[Int32.Parse(post1[0])].transform.position = cards[Int32.Parse(post1[1])].transform.position;
                //     }
                // }
                if (sSolved.Length > 0) {
                    solved = sSolved;
                    string[] sols = sSolved.Split (',');
                    foreach (string sol in sols) {
                        // record[Int32.Parse(sol)] = true;
                        dropped[(int) Decimal.Truncate (Int32.Parse (sol) / 3)]++;
                        objects[Int32.Parse (sol)].transform.position = objects2[Int32.Parse (sol)].transform.position;

                    }
                }
                GameObject thePlayer = GameObject.Find ("LevelManager");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1> ();
                playerScript.scorevar_1 = scorevar2;
                playerScript.datacheck += 3;
                setInitialPosition ();
                render ();
                self.SetActive (false);

                timer = new Stopwatch ();
                timer.Start ();
                resumePop.SetActive (false);
            }
        }
    }

    public void onStarting (bool resume, int sess) {
        session = sess;
        Debug.Log ("game7l2");

        if (resume) {
            StartCoroutine (GetRequest ("https://evening-fortress-14821.herokuapp.com/select/" + username + "/" + game + "/2"));
        } else {
            Debug.Log ("Cal,,4");

            StartCoroutine (Upload (username, game, "2", postions));

            self.SetActive (false);

            timer = new Stopwatch ();
            timer.Start ();
            resumePop.SetActive (false);
        }
    }

    public void Increment () {
        scorevar = scorevar + 2;
        scorevar2 = scorevar2 + 2;

    }
    public void setInitialPosition () {
        int j = 0;
        int x = 0;
        // if(indicator > 4){

        // }else {
        x = indicator;
        // }
        for (int i = x * 3; i < x * 3 + 3; i++) {
            inipos[j] = objects[i].transform.position;
            j++;
        }
        j = 0;
    }
    public void randomize () {
        for (int i = 0; i < 24; i += 3) {

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
            }
        }
    }
    /////////////////these functions are for the animations to work at the right time without any co-routine
    public void endanim () {
        levelcomanim.SetActive (false);
    }
    public void ddown () {
        if (scorevar_1 == 20 && (scorevar_3 < 20 || scorevar_2 < 10)) {
            StartCoroutine (ShowMessage1 (0.5f));
            return;
        }
        down.SetActive (true);
        GameObject thePlayer = GameObject.Find ("LevelManager2");
        game7l3 playerScript = thePlayer.GetComponent<game7l3> ();
        if (scorevar_2 == 10 && scorevar_3 == 20) {
            playerScript.leftarrow.SetActive (false);
        }
        playerScript.scorevar = scorevar;
        playerScript.scorevar_1 = scorevar_2;
        playerScript.scorevar_2 = scorevar_3;
        playerScript.scorevar_3 = scorevar2;
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
    async public void onclickright () {
        //change
        // timer.Restart ();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference ("users/" + username + "/association/s" + session + "/q" + (indicator + 2 + qh))
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
            if (scorevar >= 98) {
                SettingsPenal.gameover ();
                Gameover.SetActive (true);
                Invoke ("endanimation", 6.5f);
                return;
            }
            if (scorevar2 == 48) {
                levelcomanim.SetActive (true);
                SettingsPenal.playanimation ();
                Invoke ("endanim", 3f);
                Invoke ("ddown", 3.3f);
                return;

            }
            if (indicator == 7) {
                if (scorevar_1 == 20) {
                    StartCoroutine (ShowMessage1 (0.5f));
                    return;
                }
                down.SetActive (true);
                GameObject thePlayer = GameObject.Find ("LevelManager2");
                game7l3 playerScript = thePlayer.GetComponent<game7l3> ();
                playerScript.scorevar = scorevar;
                playerScript.scorevar_1 = scorevar_2;
                playerScript.scorevar_2 = scorevar_3;
                playerScript.scorevar_3 = scorevar2;
                playerScript.restartStopwatch ();
                playerScript.flag++;
                self.SetActive (false);
                return;
            }
            anf = 90;
            a = -1;

            animation ();
            indicator++;
            // if (indicator > 3)
            // {
            //     randomize();
            //     var++;
            // }

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
        //     .GetReference ("users/" + username + "/association/s" + session + "/q" + (indicator + qh))
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
            if ((indicator == 0 || scorevar2 == 48) && scorevar_3 < 20) {
                up.SetActive (true);
                GameObject thePlayer = GameObject.Find ("LevelManager");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1> ();
                playerScript.scorevar = scorevar;
                playerScript.flag++;
                playerScript.scorevar_1 = scorevar2;
                playerScript.scorevar_2 = scorevar_1;
                playerScript.restartStopwatch ();
                self.SetActive (false);
                return;
            } else if ((indicator == 0 || scorevar2 == 48) && scorevar_3 == 20 && scorevar_2 < 10) {
                up.SetActive (true);
                GameObject thePlayer1 = GameObject.Find ("LevelManager");
                Game3L1 playerScript1 = thePlayer1.GetComponent<Game3L1> ();
                playerScript1.indicator = 5;
                playerScript1.onclickleft ();
                playerScript1.scorevar = scorevar;
                playerScript1.scorevar_1 = scorevar2;
                playerScript1.scorevar_2 = scorevar_1;
                playerScript1.restartStopwatch ();
                self.SetActive (false);
                return;
            }
            if (indicator == 1 && scorevar_3 == 20 && scorevar_2 == 10) {
                leftarrow.SetActive (false);
            }
            var --;
            anf = 270;
            a = 1;
            animation ();
            if (indicator != 0) {
                indicator--;
                // if (indicator == 3)
                // {
                //     randomize();
                // }

                setInitialPosition ();
                Invoke ("render", 0.5f);

                // render();

            }
            for (int i = 0; i < 15; i++) {
                if (objects[i].transform.position == objects2[i].transform.position) {
                    objects2[i].SetActive (false);
                }
            }
        }

    }
    public void animation () {
        for (int i = 0; i < cards.Length; i++) {
            cards[i].transform.DORotate (new Vector3 (0, cards[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);

            objects[i + ((indicator) * 3)].transform.DORotate (new Vector3 (0, objects[i + ((indicator) * 3)].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
            cards2[i].transform.DORotate (new Vector3 (0, cards2[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);

            objects2[i + ((indicator) * 3)].transform.DORotate (new Vector3 (0, objects2[i + ((indicator) * 3)].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
        }
    }

    // void flaag()
    // {
    //     up.SetActive(true);
    //     GameObject thePlayer = GameObject.Find("Levelmanager");
    //     LevelManager playerScript = thePlayer.GetComponent<LevelManager>();
    //     scorevar = playerScript.scorevar;
    //     flag++;
    //     up.SetActive(false);

    // }
    private void render () {
        for (int i = 0; i < 24; i++) {
            // if (i < 3)
            // {
            //     cards2[i].SetActive(true);
            // }
            objects[i].SetActive (false);
            objects2[i].SetActive (false);
            if ((indicator == 0) && (i >= 0 && i < 3)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);

            } else if ((indicator == 1) && (i >= 3 && i < 6)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 2) && (i >= 6 && i < 9)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 3) && (i >= 9 && i < 12)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 4) && (i >= 12 && i < 15)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 5) && (i >= 15 && i < 18)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 6) && (i >= 18 && i < 21)) {
                objects[i].SetActive (true);
                objects2[i].SetActive (true);
            } else if ((indicator == 7) && (i >= 21 && i < 24)) {
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

            try {
                reposnseTime = timer.Elapsed.TotalSeconds.ToString ("0.##");
                timer.Restart ();
            } catch {
                Debug.Log ("HKOJ");
            }

            dragLocker = false;
        }
        //////
        if (objects[ob].transform.position == objects2[ob].transform.position) {
            return;
        }
        var point = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        objects[ob].transform.position = new Vector3 (point.x, point.y, objects[ob].transform.position.z);

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
        for (int p = indicator * 3; p < ((indicator * 3) + 3); p++) {
            if ((Vector3.Distance (objects[a].transform.position, objects2[p].transform.position) < 10) && (objects2[p].activeInHierarchy)) {
                destination = destinations[p];
            }
        }

        float Distance = Vector3.Distance (objects[a].transform.position, objects2[a].transform.position);
        if (Distance < 10) {

            destination = destinations[a];

            objects[a].transform.position = objects2[a].transform.position;
            //  objects2[a].GetActive
            SettingsPenal.playeffect (0);

            if (objects2[a].active) {
                dropped[indicator]++;
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
            objects[a].transform.position = inipos[a % 3];
            SettingsPenal.playeffect (1);

        }
        //change
        saveAttempt (reposnseTime, dragTime, status, dragDistance, dropDistance, sources[a], destination);
        /////

        if (dropped[indicator] == 3) {

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
        // AtemptAssociation atempt = new AtemptAssociation(reposnseTime, dragTime, status, dragDistance, dropDistance, source, destination);
        // string json = JsonUtility.ToJson(atempt);
        // Debug.Log("qh");
        // Debug.Log(qh);

        // reference.Child("users").Child(username).Child("association").Child("s" + session).Child("q" + (indicator + 1 + qh)).Child("a" + attemptNo).SetRawJsonValueAsync(json);
        // attemptNo++;

        int questionNo = indicator + 1 + qh;
        StartCoroutine (updateAnalytics (username, game, session, questionNo, reposnseTime, dragTime, status, dragDistance, dropDistance, source, destination));

        if (status == "right") {
            // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child(gType).Child("solved").SetValueAsync(solved);
            StartCoroutine (updateScore (username, game, "2", scorevar, scorevar1, scorevar2, scorevar_1, scorevar_2, scorevar_3, solved));

        }
    }
    /////
    IEnumerator ShowMessage (float delay) {
        Stagecomplete.text = "" + Stagetext;
        yield return new WaitForSeconds (delay);
        ltext.SetActive (true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds (2f);
        ltext.SetActive (false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }
    public void restartTimer () {
        timer.Restart ();
    }

    void Update () {
        fortitle.text = "" + title;
        if (flag == 1) {
            StartCoroutine (ShowMessage (0.5f));

            flag--;
        }
        // if (flag == 5)
        // {
        //     flaag();
        // }

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
        if (flag == 1) {
            StartCoroutine (ShowMessage (0.5f));
            flag--;
        }
    }
}

//change
class AtemptAssociation {
    public string reposnseTime;
    public string dragTime;
    public string status;
    public string dragDistance;
    public string dropDistance;
    public string source;
    public string destination;
    public string timeStamp;

    public AtemptAssociation () { }
    public AtemptAssociation (string reposnseTime, string dragTime, string status, string dragDistance, string dropDistance, string source, string destination) {
        this.reposnseTime = reposnseTime;
        this.dragTime = dragTime;
        this.status = status;
        this.dragDistance = dragDistance;
        this.dropDistance = dropDistance;
        this.source = source;
        this.destination = destination;
        this.timeStamp = DateTime.Now.ToString ("MM/dd/yyyy HH:mm:ss");

    }

    // public AtemptAssociation(string reposnseTime, string dragTime, string status, string dragDistance, string dropDistance)
    // {
    //     this.reposnseTime = reposnseTime;
    //     this.dragTime = dragTime;
    //     this.status = status;
    //     this.dragDistance = dragDistance;
    //     this.dropDistance = dropDistance;
    // }
}
/////