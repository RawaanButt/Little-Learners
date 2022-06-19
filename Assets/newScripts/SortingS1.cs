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

public class SortingS1 : MonoBehaviour {
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
    public GameObject up;
    public GameObject down, down1;

    public int scorevar = 0;
    public int scorevar2 = 0;
    public int scorevar3 = 0;
    public int scorevar4 = 0;
    public int scorevar_1 = 0;
    public int scorevar_2 = 0;
    public int scorevar_3 = 0;
    public int scorevar_4 = 0;
    public GameObject finaltext, Gameover;

    public Text newText;
    public Text Stagecomplete;
    public string[] Stagetext;
    public GameObject ltext, leftarrow;

    public int flag;

    public Text header;
    public string headertext;
    public string headertext1;
    public Vector3 dot;
    public int anf;
    public int a;
    public GameObject levelcomanim;
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
    string sPostions = "";
    string sSolved = "";
    int sv = 0;
    int sv2 = 0;
    int sv3 = 0;
    int sv4 = 0;
    public int datacheck;

    void Start () {

        //change
        qh = 0;
        if (alphabet == "abc") {
            qh = 6;
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
            sources[11] = "3";
            sources[12] = "5";
            sources[13] = "7";
            sources[14] = "9";
            sources[15] = "2";
            sources[16] = "4";
            sources[17] = "6";
            sources[18] = "8";
            sources[19] = "10";
            sources[20] = "1";
            sources[21] = "3";
            sources[22] = "5";
            sources[23] = "7";
            sources[24] = "9";
            sources[25] = "2";
            sources[26] = "4";
            sources[27] = "6";
            sources[28] = "8";
            sources[29] = "10";

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
            sources[11] = "c";
            sources[12] = "e";
            sources[13] = "g";
            sources[14] = "i";
            sources[15] = "b";
            sources[16] = "d";
            sources[17] = "f";
            sources[18] = "h";
            sources[19] = "j";
            sources[20] = "a";
            sources[21] = "c";
            sources[22] = "e";
            sources[23] = "g";
            sources[24] = "i";
            sources[25] = "b";
            sources[26] = "d";
            sources[27] = "f";
            sources[28] = "h";
            sources[29] = "j";
        }

        dragLocker = true;
        username = File.ReadAllText (Application.dataPath + "/user.txt");
        // yy
        indicator = 0;
        render ();
        randomize ();

        var = -5;
        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive (false);
        }

        scorevar = 0;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

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
    IEnumerator updateScore (String user, String gameType, int score, int score2, int score3, int score4, String solved) {
        WWWForm form = new WWWForm ();
        form.AddField ("user", user);
        form.AddField ("gameType", gameType);
        print($"Score 1: {score}, Score 2: {score2}, Score3: {score3}, Score 4: {score4}");
        form.AddField ("score", score);
        form.AddField ("score2", score2);
        form.AddField ("score3", score3);
        form.AddField ("score4", score4);
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

    public void jump () {
        Debug.Log ("lala");
        if (scorevar2 == 20 && scorevar3 < 20) {
            indicator = 2;
            leftarrow.SetActive (false);
            setInitialPosition ();
            render ();
        } else if (scorevar2 == 20 && scorevar3 == 20 && scorevar4 < 20) {
            indicator = 4;
            leftarrow.SetActive (false);
            setInitialPosition ();
            render ();
        } else if (scorevar2 == 20 && scorevar3 == 20 && scorevar4 == 20 && scorevar_1 < 20) {
            down.SetActive (true);
            GameObject thePlayer = GameObject.Find ("LevelManager");
            SortingS1 playerScript = thePlayer.GetComponent<SortingS1> ();
            playerScript.leftarrow.SetActive (false);
            playerScript.scorevar = scorevar;
            playerScript.scorevar_1 = scorevar2;
            playerScript.scorevar_2 = scorevar3;
            playerScript.scorevar_3 = scorevar4;
            playerScript.scorevar_4 = scorevar_4;
            // playerScript.restartStopwatch ();
            playerScript.flag++;
            self.SetActive (false);
        } else if (scorevar2 == 20 && scorevar3 == 20 && scorevar4 == 20 && scorevar_1 <= 20 && scorevar_2 < 20) {
            down.SetActive (true);
            GameObject thePlayer1 = GameObject.Find ("LevelManager");
            SortingS1 playerScript1 = thePlayer1.GetComponent<SortingS1> ();
            playerScript1.indicator = 2;
            playerScript1.setInitialPosition ();
            playerScript1.render ();
            playerScript1.leftarrow.SetActive (false);
            playerScript1.scorevar = scorevar;
            playerScript1.scorevar_1 = scorevar2;
            playerScript1.scorevar_2 = scorevar3;
            playerScript1.scorevar_3 = scorevar4;
            playerScript1.scorevar_4 = scorevar_4;
            // playerScript1.restartStopwatch ();
            playerScript1.flag += 3;
            self.SetActive (false);
        } else if (scorevar2 == 20 && scorevar3 == 20 && scorevar4 == 20 && scorevar_1 <= 20 && scorevar_2 == 20 && scorevar_3 < 20) {
            down.SetActive (true);
            GameObject thePlayer2 = GameObject.Find ("LevelManager");
            SortingS1 playerScript2 = thePlayer2.GetComponent<SortingS1> ();
            playerScript2.indicator = 4;
            playerScript2.setInitialPosition ();
            playerScript2.render ();
            playerScript2.scorevar = scorevar;
            playerScript2.leftarrow.SetActive (false);
            playerScript2.scorevar_1 = scorevar2;
            playerScript2.scorevar_2 = scorevar3;
            playerScript2.scorevar_3 = scorevar4;
            playerScript2.scorevar_4 = scorevar_4;
            // playerScript2.restartStopwatch ();
            playerScript2.flag += 2;
            self.SetActive (false);
        } else if (scorevar2 == 20 && scorevar3 == 20 && scorevar4 == 20 && scorevar_1 <= 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 < 110) {
            down1.SetActive (true);
            GameObject thePlayer3 = GameObject.Find ("LevelManager2");
            SortingS2 playerScript3 = thePlayer3.GetComponent<SortingS2> ();
            playerScript3.scorevar = scorevar;
            playerScript3.leftarrow.SetActive (false);
            playerScript3.scorevar_1 = scorevar2;
            playerScript3.scorevar_2 = scorevar3;
            playerScript3.scorevar_3 = scorevar4;
            playerScript3.scorevar_4 = scorevar_1;
            playerScript3.scorevar_5 = scorevar_2;
            playerScript3.scorevar_6 = scorevar_3;
            // playerScript3.restartStopwatch ();
            playerScript3.flag++;
            self.SetActive (false);
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
                sPostions = myObject.positions;

                scorevar = sv;
                scorevar2 = sv2;
                scorevar3 = sv3;
                scorevar4 = sv4;
                sPostions = myObject.positions;

                if (myObject.solved != null) {
                    sSolved = myObject.solved.Length > 0 ? myObject.solved : sSolved;
                }
                if (myObject.score != null) {
                    scorevar = myObject.score;
                }
                if (myObject.score2 != null) {
                    scorevar2 = myObject.score2;
                    scorevar3 = myObject.score3;
                    scorevar4 = myObject.score4;
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
                        dropped[(int) Decimal.Truncate (Int32.Parse (sol) / 5)]++;
                        objects[Int32.Parse (sol)].transform.position = objects2[Int32.Parse (sol)].transform.position;

                    }
                }
                if (alphabet == "abc") {
                    GameObject thePlayer = GameObject.Find ("LevelManager1");
                    SortingS1 playerScript = thePlayer.GetComponent<SortingS1> ();
                    playerScript.scorevar_1 = scorevar2;
                    playerScript.scorevar_2 = scorevar3;
                    playerScript.scorevar_3 = scorevar4;
                    playerScript.datacheck++;
                    self.SetActive (false);
                }
                setInitialPosition ();
                render ();

                timer = new Stopwatch ();
                timer.Start ();

                if (alphabet == "abc") {
                    resumePop.SetActive (false);

                } else {
                    datacheck++;

                }

            }
        }
    }

    public async void onStarting (bool resume, int sess) {
        session = sess;

        if (alphabet == "abc") {
            gType = "alpha";
        } else if (number == 123) {
            gType = "number";
        }

        if (resume) {
            StartCoroutine (GetRequest ("https://evening-fortress-14821.herokuapp.com/sorting/" + username + "/" + gType));
        } else {
            StartCoroutine (Upload (username, gType, postions));

            if (alphabet == "abc") {
                self.SetActive (false);
            }
            setInitialPosition ();

            timer = new Stopwatch ();
            timer.Start ();
            resumePop.SetActive (false);
        }
    }

    public void restartTimer () {
        timer.Restart ();
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

    public void exittomainmanu () {
        SceneManager.LoadScene ("Game Selection", LoadSceneMode.Single);
    }

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

        for (int i = 0; i < 30; i += 5) {

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
    //////////////////////Level complete animation///////////
    public void endanim () {
        levelcomanim.SetActive (false);
    }

    public void ddown () {
        if (scorevar_1 == 20 && scorevar_2 < 20) {
            down.SetActive (true);
            GameObject thePlayer5 = GameObject.Find ("LevelManager");
            SortingS1 playerScript5 = thePlayer5.GetComponent<SortingS1> ();
            playerScript5.indicator = 1;
            if (scorevar2 == 20 && scorevar3 == 20) {
                playerScript5.leftarrow.SetActive (false);
            }
            playerScript5.onclickright ();
            playerScript5.scorevar = scorevar;
            playerScript5.scorevar_1 = scorevar2;
            playerScript5.scorevar_2 = scorevar3;
            playerScript5.scorevar_3 = scorevar4;
            playerScript5.scorevar_4 = scorevar_4;
            playerScript5.restartStopwatch ();
            playerScript5.flag++;
            self.SetActive (false);
            return;
        } else if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 < 20) {
            down.SetActive (true);
            GameObject thePlayer6 = GameObject.Find ("LevelManager");
            SortingS1 playerScript6 = thePlayer6.GetComponent<SortingS1> ();
            playerScript6.indicator = 3;
            playerScript6.onclickright ();
            if (scorevar2 == 20 && scorevar3 == 20) {
                playerScript6.leftarrow.SetActive (false);
            }
            playerScript6.scorevar = scorevar;
            playerScript6.scorevar_1 = scorevar2;
            playerScript6.scorevar_2 = scorevar3;
            playerScript6.scorevar_3 = scorevar4;
            playerScript6.scorevar_4 = scorevar_4;
            playerScript6.restartStopwatch ();
            playerScript6.flag++;
            self.SetActive (false);
            return;
        } else if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 < 110) {
            down1.SetActive (true);
            GameObject thePlayer7 = GameObject.Find ("LevelManager2");
            SortingS2 playerScript7 = thePlayer7.GetComponent<SortingS2> ();
            playerScript7.scorevar = scorevar;
            if (scorevar2 == 20 && scorevar3 == 20) {
                playerScript7.leftarrow.SetActive (false);
            }
            playerScript7.scorevar_1 = scorevar2;
            playerScript7.scorevar_2 = scorevar3;
            playerScript7.scorevar_3 = scorevar4;
            playerScript7.scorevar_4 = scorevar_1;
            playerScript7.scorevar_5 = scorevar_2;
            playerScript7.scorevar_6 = scorevar_3;
            playerScript7.restartStopwatch ();
            playerScript7.flag++;
            self.SetActive (false);
            return;
        } else if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 == 30) {
            StartCoroutine (ShowMessage1 (0.5f));
            return;
        }
        down.SetActive (true);
        GameObject thePlayer = GameObject.Find ("LevelManager");
        SortingS1 playerScript = thePlayer.GetComponent<SortingS1> ();
        if (scorevar2 == 20 && scorevar3 == 20) {
            playerScript.leftarrow.SetActive (false);
        }
        playerScript.restartStopwatch ();
        playerScript.scorevar_1 = scorevar2;
        playerScript.scorevar_2 = scorevar3;
        playerScript.scorevar_3 = scorevar4;
        playerScript.scorevar = scorevar;
        playerScript.scorevar_4 = scorevar_4;

        playerScript.flag++;
        self.SetActive (false);
        return;
    }
    public void uup () {
        if (scorevar_4 < 110) {
            down.SetActive (true);
            GameObject thePlayer = GameObject.Find ("LevelManager2");
            SortingS2 playerScript = thePlayer.GetComponent<SortingS2> ();
            if (scorevar2 == 20 && scorevar3 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20) {
                playerScript.leftarrow.SetActive (false);
            }
            playerScript.restartStopwatch ();
            playerScript.scorevar = scorevar;
            playerScript.flag++;
            self.SetActive (false);
            return;
        } else {
            StartCoroutine (ShowMessage1 (0.5f));
            return;
        }

    }
    public void restartStopwatch () {
        timer.Restart ();

        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child("onquestionNo").SetValueAsync((indicator + qh).ToString());

    }
    IEnumerator ShowMessage1 (float delay) {
        yield return new WaitForSeconds (delay);
        finaltext.SetActive (true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds (2f);
        finaltext.SetActive (false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }
    IEnumerator wait () {
        yield return new WaitForSeconds (3);
        endanim ();

        if (number == 123) {
            if (indicator >= 0 && indicator <= 1) {
                if (scorevar3 < 20) {
                    indicator = 2;
                    leftarrow.SetActive (false);
                } else if (scorevar3 == 20 && scorevar4 < 20) {
                    indicator = 4;
                    leftarrow.SetActive (false);
                } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 < 20) {
                    down.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("LevelManager");
                    SortingS1 playerScript = thePlayer.GetComponent<SortingS1> ();
                    playerScript.scorevar = scorevar;
                    playerScript.leftarrow.SetActive (false);
                    playerScript.scorevar_1 = scorevar2;
                    playerScript.scorevar_2 = scorevar3;
                    playerScript.scorevar_3 = scorevar4;
                    playerScript.scorevar_4 = scorevar_4;
                    playerScript.restartStopwatch ();
                    playerScript.flag++;
                    self.SetActive (false);
                    yield break;
                } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 < 20) {
                    down.SetActive (true);
                    GameObject thePlayer1 = GameObject.Find ("LevelManager");
                    SortingS1 playerScript1 = thePlayer1.GetComponent<SortingS1> ();
                    playerScript1.leftarrow.SetActive (false);
                    playerScript1.scorevar = scorevar;
                    playerScript1.scorevar_1 = scorevar2;
                    playerScript1.scorevar_2 = scorevar3;
                    playerScript1.scorevar_3 = scorevar4;
                    playerScript1.scorevar_4 = scorevar_4;
                    playerScript1.restartStopwatch ();
                    playerScript1.flag++;
                    self.SetActive (false);
                    yield break;
                } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 <= 20 && scorevar_2 == 20 && scorevar_3 < 20) {
                    down.SetActive (true);
                    GameObject thePlayer2 = GameObject.Find ("LevelManager");
                    SortingS1 playerScript2 = thePlayer2.GetComponent<SortingS1> ();

                    playerScript2.leftarrow.SetActive (false);

                    playerScript2.scorevar = scorevar;
                    playerScript2.scorevar_1 = scorevar2;
                    playerScript2.scorevar_2 = scorevar3;
                    playerScript2.scorevar_3 = scorevar4;
                    playerScript2.scorevar_4 = scorevar_4;
                    playerScript2.restartStopwatch ();
                    playerScript2.flag++;
                    self.SetActive (false);
                    yield break;
                } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 <= 20 && scorevar_2 == 20 && scorevar_3 < 20 && scorevar_4 < 110) {
                    down1.SetActive (true);
                    GameObject thePlayer3 = GameObject.Find ("LevelManager2");
                    SortingS2 playerScript3 = thePlayer3.GetComponent<SortingS2> ();
                    playerScript3.scorevar = scorevar;
                    playerScript3.leftarrow.SetActive (false);
                    playerScript3.scorevar_1 = scorevar2;
                    playerScript3.scorevar_2 = scorevar3;
                    playerScript3.scorevar_3 = scorevar4;
                    playerScript3.scorevar_4 = scorevar_1;
                    playerScript3.scorevar_5 = scorevar_2;
                    playerScript3.scorevar_6 = scorevar_3;
                    playerScript3.restartStopwatch ();
                    playerScript3.flag++;
                    self.SetActive (false);
                    yield break;
                } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 == 110) {
                    StartCoroutine (ShowMessage1 (0.5f));
                    yield break;
                }
                // indicator++;
            } else if (indicator >= 2 && indicator <= 3) {
                if (scorevar4 < 20) {
                    indicator = 4;
                    if (scorevar2 == 20) {
                        leftarrow.SetActive (false);
                    }

                } else if (scorevar4 == 20 && scorevar_1 < 20) {
                    down.SetActive (true);
                    GameObject thePlayer4 = GameObject.Find ("LevelManager");
                    SortingS1 playerScript4 = thePlayer4.GetComponent<SortingS1> ();
                    playerScript4.scorevar = scorevar;
                    if (scorevar2 == 20) {
                        playerScript4.leftarrow.SetActive (false);
                    }
                    playerScript4.scorevar_1 = scorevar2;
                    playerScript4.scorevar_2 = scorevar3;
                    playerScript4.scorevar_3 = scorevar4;
                    playerScript4.scorevar_4 = scorevar_4;
                    playerScript4.restartStopwatch ();
                    playerScript4.flag++;
                    self.SetActive (false);
                    yield break;
                } else if (scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 < 20) {
                    down.SetActive (true);
                    GameObject thePlayer5 = GameObject.Find ("LevelManager");
                    SortingS1 playerScript5 = thePlayer5.GetComponent<SortingS1> ();

                    if (scorevar2 == 20) {
                        playerScript5.leftarrow.SetActive (false);
                    }

                    playerScript5.scorevar = scorevar;
                    playerScript5.scorevar_1 = scorevar2;
                    playerScript5.scorevar_2 = scorevar3;
                    playerScript5.scorevar_3 = scorevar4;
                    playerScript5.scorevar_4 = scorevar_4;
                    playerScript5.restartStopwatch ();
                    playerScript5.flag++;
                    self.SetActive (false);
                    yield break;
                } else if (scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 < 20) {
                    down.SetActive (true);
                    GameObject thePlayer6 = GameObject.Find ("LevelManager");
                    SortingS1 playerScript6 = thePlayer6.GetComponent<SortingS1> ();
                    playerScript6.indicator = 3;
                    if (scorevar2 == 20) {
                        playerScript6.leftarrow.SetActive (false);
                    }
                    playerScript6.onclickright ();
                    playerScript6.scorevar = scorevar;
                    playerScript6.scorevar_1 = scorevar2;
                    playerScript6.scorevar_2 = scorevar3;
                    playerScript6.scorevar_3 = scorevar4;
                    playerScript6.scorevar_4 = scorevar_4;
                    playerScript6.restartStopwatch ();
                    playerScript6.flag++;
                    self.SetActive (false);
                    yield break;
                } else if (scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 < 110) {
                    down1.SetActive (true);
                    GameObject thePlayer7 = GameObject.Find ("LevelManager2");
                    SortingS2 playerScript7 = thePlayer7.GetComponent<SortingS2> ();
                    playerScript7.scorevar = scorevar;
                    playerScript7.scorevar_1 = scorevar2;
                    if (scorevar2 == 20) {
                        playerScript7.leftarrow.SetActive (false);
                    }
                    playerScript7.scorevar_2 = scorevar3;
                    playerScript7.scorevar_3 = scorevar4;
                    playerScript7.scorevar_4 = scorevar_1;
                    playerScript7.scorevar_5 = scorevar_2;
                    playerScript7.scorevar_6 = scorevar_3;
                    playerScript7.restartStopwatch ();
                    playerScript7.flag++;
                    self.SetActive (false);
                    yield break;
                } else if (scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 == 110) {
                    StartCoroutine (ShowMessage1 (0.5f));
                    yield break;
                }

            }
        } else if (alphabet == "abc") {
            if (scorevar2 == 20 && scorevar3 < 20) {

                if (scorevar3 < 20) {
                    indicator = 2;
                    if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20) {
                        leftarrow.SetActive (false);
                    }
                } else if (scorevar3 == 20 && scorevar4 < 20) {
                    indicator = 4;
                    if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20) {
                        leftarrow.SetActive (false);
                    }
                } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_4 < 110) {
                    down.SetActive (true);
                    GameObject thePlayer8 = GameObject.Find ("LevelManager2");
                    SortingS2 playerScript8 = thePlayer8.GetComponent<SortingS2> ();
                    playerScript8.scorevar = scorevar;
                    if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20) {
                        playerScript8.leftarrow.SetActive (false);
                    }
                    playerScript8.scorevar_1 = scorevar_1;
                    playerScript8.scorevar_2 = scorevar_2;
                    playerScript8.scorevar_3 = scorevar_3;
                    playerScript8.scorevar_4 = scorevar2;
                    playerScript8.scorevar_5 = scorevar3;
                    playerScript8.scorevar_6 = scorevar4;
                    playerScript8.restartStopwatch ();
                    playerScript8.flag++;
                    self.SetActive (false);
                    yield break;
                } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_4 == 110) {
                    StartCoroutine (ShowMessage1 (0.5f));
                    yield break;
                }
            }
            if (scorevar3 == 20) {

                if (scorevar4 < 20) {
                    indicator = 4;
                    if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar2 == 20) {
                        leftarrow.SetActive (false);
                    }
                } else if (scorevar4 == 20 && scorevar_4 < 110) {
                    down.SetActive (true);
                    GameObject thePlayer9 = GameObject.Find ("LevelManager2");
                    SortingS2 playerScript9 = thePlayer9.GetComponent<SortingS2> ();
                    playerScript9.scorevar = scorevar;
                    if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar2 == 20) {
                        playerScript9.leftarrow.SetActive (false);
                    }
                    playerScript9.scorevar_1 = scorevar_1;
                    playerScript9.scorevar_2 = scorevar_2;
                    playerScript9.scorevar_3 = scorevar_3;
                    playerScript9.scorevar_4 = scorevar2;
                    playerScript9.scorevar_5 = scorevar3;
                    playerScript9.scorevar_6 = scorevar4;
                    playerScript9.restartStopwatch ();
                    playerScript9.flag++;
                    self.SetActive (false);
                    yield break;
                } else if (scorevar4 == 20 && scorevar_4 == 110) {
                    StartCoroutine (ShowMessage1 (0.5f));
                    yield break;
                }
            }

        }
        animation ();
        Debug.Log (indicator);
        setInitialPosition ();
        Invoke ("render", 0.5f);

    }
    public void endanimation () {

        SceneManager.LoadScene ("Game Selection");
    }

    public void onclickright () {
        //change
        timer.Restart ();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/sorting/s" + session + "/q" + (indicator + 2 + qh))
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
            if (scorevar >= 230) {
                SettingsPenal.gameover ();
                Gameover.SetActive (true);
                Invoke ("endanimation", 6.5f);
                return;
            }
            if (indicator >= 4 && scorevar4 == 20) {
                levelcomanim.SetActive (true);
                SettingsPenal.playanimation ();
                Invoke ("endanim", 3f);

                if (number == 123) {
                    if (scorevar4 == 20) {
                        Invoke ("ddown", 3.3f);
                        return;
                    }
                } else if (alphabet == "abc") {
                    if (scorevar4 == 20) {
                        Invoke ("uup", 3.3f);
                        return;
                    }
                }

            }

            if (indicator == 5) {

                if (number == 123) {
                    if (scorevar_1 == 20 && scorevar_2 < 20) {
                        down.SetActive (true);
                        GameObject thePlayer5 = GameObject.Find ("LevelManager");
                        SortingS1 playerScript5 = thePlayer5.GetComponent<SortingS1> ();

                        playerScript5.scorevar = scorevar;
                        playerScript5.scorevar_1 = scorevar2;
                        playerScript5.scorevar_2 = scorevar3;
                        playerScript5.scorevar_3 = scorevar4;
                        playerScript5.scorevar_4 = scorevar_4;
                        playerScript5.restartStopwatch ();
                        playerScript5.flag += 3;
                        self.SetActive (false);
                        return;
                    } else if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 < 20) {
                        down.SetActive (true);
                        GameObject thePlayer6 = GameObject.Find ("LevelManager");
                        SortingS1 playerScript6 = thePlayer6.GetComponent<SortingS1> ();
                        playerScript6.scorevar = scorevar;
                        playerScript6.scorevar_1 = scorevar2;
                        playerScript6.scorevar_2 = scorevar3;
                        playerScript6.scorevar_3 = scorevar4;
                        playerScript6.scorevar_4 = scorevar_4;
                        playerScript6.restartStopwatch ();
                        playerScript6.flag += 2;
                        self.SetActive (false);
                        return;
                    } else if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 < 110) {
                        down1.SetActive (true);
                        GameObject thePlayer7 = GameObject.Find ("LevelManager2");
                        SortingS2 playerScript7 = thePlayer7.GetComponent<SortingS2> ();
                        playerScript7.scorevar = scorevar;
                        playerScript7.scorevar_1 = scorevar2;
                        playerScript7.scorevar_2 = scorevar3;
                        playerScript7.scorevar_3 = scorevar4;
                        playerScript7.scorevar_4 = scorevar_1;
                        playerScript7.scorevar_5 = scorevar_2;
                        playerScript7.scorevar_6 = scorevar_3;
                        playerScript7.restartStopwatch ();
                        playerScript7.flag++;
                        self.SetActive (false);
                        return;
                    } else if (scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 == 110) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }
                    down.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("LevelManager");
                    SortingS1 playerScript = thePlayer.GetComponent<SortingS1> ();
                    playerScript.scorevar = scorevar;
                    playerScript.scorevar_1 = scorevar2;
                    playerScript.scorevar_2 = scorevar3;
                    playerScript.scorevar_3 = scorevar4;
                    playerScript.scorevar_4 = scorevar_4;

                    playerScript.restartStopwatch ();
                    playerScript.flag++;
                    self.SetActive (false);
                    return;

                } else {
                    if (scorevar_4 < 110) {
                        down.SetActive (true);
                        GameObject thePlayer = GameObject.Find ("LevelManager2");
                        SortingS2 playerScript = thePlayer.GetComponent<SortingS2> ();
                        playerScript.scorevar = scorevar;
                        playerScript.scorevar_1 = scorevar_1;
                        playerScript.scorevar_2 = scorevar_2;
                        playerScript.scorevar_3 = scorevar_3;
                        playerScript.scorevar_4 = scorevar2;
                        playerScript.scorevar_5 = scorevar3;
                        playerScript.scorevar_6 = scorevar4;
                        playerScript.restartStopwatch ();
                        playerScript.flag++;
                        self.SetActive (false);
                        return;
                    } else if (scorevar_4 == 110) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }

                }
            }

            anf = 90;
            a = -1;

            if (((indicator >= 0 && indicator <= 1) && scorevar2 == 20) || ((indicator >= 2 && indicator <= 3) && scorevar3 == 20)) {
                Debug.Log ("i am in");
                levelcomanim.SetActive (true);
                SettingsPenal.playanimation ();
                if (number == 123) {
                    if ((indicator >= 0 && indicator <= 1) && scorevar2 == 20) {
                        StartCoroutine (ShowMessage (0, 3.7f));
                    } else {
                        StartCoroutine (ShowMessage (1, 3.7f));
                    }
                } else {
                    if ((indicator >= 0 && indicator <= 1) && scorevar2 == 20) {
                        StartCoroutine (ShowMessage (3, 3.7f));
                    } else {
                        StartCoroutine (ShowMessage (4, 3.7f));
                    }
                }
                StartCoroutine (wait ());
                // Invoke ("leveltext(0)", 3.5f);

                return;
            }

            ///////////////
            if (number == 123) {
                if (indicator == 1) {
                    if (scorevar3 == 20 && scorevar4 < 20) {
                        indicator = 3;
                    } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 < 20) {
                        down.SetActive (true);
                        GameObject thePlayer = GameObject.Find ("LevelManager");
                        SortingS1 playerScript = thePlayer.GetComponent<SortingS1> ();
                        playerScript.scorevar = scorevar;
                        playerScript.scorevar_1 = scorevar2;
                        playerScript.scorevar_2 = scorevar3;
                        playerScript.scorevar_3 = scorevar4;
                        playerScript.scorevar_4 = scorevar_4;
                        playerScript.restartStopwatch ();
                        playerScript.flag++;
                        self.SetActive (false);
                        return;
                    } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 <= 20 && scorevar_2 < 20) {
                        down.SetActive (true);
                        GameObject thePlayer1 = GameObject.Find ("LevelManager");
                        SortingS1 playerScript1 = thePlayer1.GetComponent<SortingS1> ();

                        playerScript1.scorevar = scorevar;
                        playerScript1.scorevar_1 = scorevar2;
                        playerScript1.scorevar_2 = scorevar3;
                        playerScript1.scorevar_3 = scorevar4;
                        playerScript1.scorevar_4 = scorevar_4;
                        playerScript1.restartStopwatch ();
                        playerScript1.flag += 3;
                        self.SetActive (false);
                        return;
                    } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 <= 20 && scorevar_2 == 20 && scorevar_3 < 20) {
                        down.SetActive (true);
                        GameObject thePlayer2 = GameObject.Find ("LevelManager");
                        SortingS1 playerScript2 = thePlayer2.GetComponent<SortingS1> ();

                        playerScript2.scorevar = scorevar;
                        playerScript2.scorevar_1 = scorevar2;
                        playerScript2.scorevar_2 = scorevar3;
                        playerScript2.scorevar_3 = scorevar4;
                        playerScript2.scorevar_4 = scorevar_4;
                        playerScript2.restartStopwatch ();
                        playerScript2.flag += 2;
                        self.SetActive (false);
                        return;
                    } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 <= 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 < 110) {
                        down1.SetActive (true);
                        GameObject thePlayer3 = GameObject.Find ("LevelManager2");
                        SortingS2 playerScript3 = thePlayer3.GetComponent<SortingS2> ();
                        playerScript3.scorevar = scorevar;
                        playerScript3.scorevar_1 = scorevar2;
                        playerScript3.scorevar_2 = scorevar3;
                        playerScript3.scorevar_3 = scorevar4;
                        playerScript3.scorevar_4 = scorevar_1;
                        playerScript3.scorevar_5 = scorevar_2;
                        playerScript3.scorevar_6 = scorevar_3;
                        playerScript3.restartStopwatch ();
                        playerScript3.flag++;
                        self.SetActive (false);
                        return;
                    } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 == 110) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }

                } else if (indicator == 3) {
                    if (scorevar4 == 20 && scorevar_1 < 20) {
                        down.SetActive (true);
                        GameObject thePlayer4 = GameObject.Find ("LevelManager");
                        SortingS1 playerScript4 = thePlayer4.GetComponent<SortingS1> ();
                        playerScript4.scorevar = scorevar;
                        playerScript4.scorevar_1 = scorevar2;
                        playerScript4.scorevar_2 = scorevar3;
                        playerScript4.scorevar_3 = scorevar4;
                        playerScript4.scorevar_4 = scorevar_4;
                        playerScript4.restartStopwatch ();
                        playerScript4.flag++;
                        self.SetActive (false);
                        return;
                    } else if (scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 < 20) {
                        down.SetActive (true);
                        GameObject thePlayer5 = GameObject.Find ("LevelManager");
                        SortingS1 playerScript5 = thePlayer5.GetComponent<SortingS1> ();

                        playerScript5.scorevar = scorevar;
                        playerScript5.scorevar_1 = scorevar2;
                        playerScript5.scorevar_2 = scorevar3;
                        playerScript5.scorevar_3 = scorevar4;
                        playerScript5.scorevar_4 = scorevar_4;
                        playerScript5.restartStopwatch ();
                        playerScript5.flag += 3;
                        self.SetActive (false);
                        return;
                    } else if (scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 < 20) {
                        down.SetActive (true);
                        GameObject thePlayer6 = GameObject.Find ("LevelManager");
                        SortingS1 playerScript6 = thePlayer6.GetComponent<SortingS1> ();
                        playerScript6.scorevar = scorevar;
                        playerScript6.scorevar_1 = scorevar2;
                        playerScript6.scorevar_2 = scorevar3;
                        playerScript6.scorevar_3 = scorevar4;
                        playerScript6.scorevar_4 = scorevar_4;
                        playerScript6.restartStopwatch ();
                        playerScript6.flag += 2;
                        self.SetActive (false);
                        return;
                    } else if (scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 < 110) {
                        down1.SetActive (true);
                        GameObject thePlayer7 = GameObject.Find ("LevelManager2");
                        SortingS2 playerScript7 = thePlayer7.GetComponent<SortingS2> ();
                        playerScript7.scorevar = scorevar;
                        playerScript7.scorevar_1 = scorevar2;
                        playerScript7.scorevar_2 = scorevar3;
                        playerScript7.scorevar_3 = scorevar4;
                        playerScript7.scorevar_4 = scorevar_1;
                        playerScript7.scorevar_5 = scorevar_2;
                        playerScript7.scorevar_6 = scorevar_3;
                        playerScript7.restartStopwatch ();
                        playerScript7.flag++;
                        self.SetActive (false);
                        return;
                    } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20 && scorevar_4 == 110) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }

                }

            } else if (alphabet == "abc") {
                if (indicator == 1) {
                    if (scorevar3 == 20 && scorevar4 < 20) {
                        indicator = 4;
                    } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_4 < 110) {
                        down.SetActive (true);
                        GameObject thePlayer8 = GameObject.Find ("LevelManager2");
                        SortingS2 playerScript8 = thePlayer8.GetComponent<SortingS2> ();
                        playerScript8.scorevar = scorevar;
                        playerScript8.scorevar_1 = scorevar_1;
                        playerScript8.scorevar_2 = scorevar_2;
                        playerScript8.scorevar_3 = scorevar_3;
                        playerScript8.scorevar_4 = scorevar2;
                        playerScript8.scorevar_5 = scorevar3;
                        playerScript8.scorevar_6 = scorevar4;
                        playerScript8.restartStopwatch ();
                        playerScript8.flag++;
                        self.SetActive (false);
                        return;
                    } else if (scorevar3 == 20 && scorevar4 == 20 && scorevar_4 == 110) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }
                } else if (indicator == 3) {
                    if (scorevar4 == 20 && scorevar_4 < 110) {
                        down.SetActive (true);
                        GameObject thePlayer8 = GameObject.Find ("LevelManager2");
                        SortingS2 playerScript8 = thePlayer8.GetComponent<SortingS2> ();
                        playerScript8.scorevar = scorevar;
                        playerScript8.scorevar_1 = scorevar_1;
                        playerScript8.scorevar_2 = scorevar_2;
                        playerScript8.scorevar_3 = scorevar_3;
                        playerScript8.scorevar_4 = scorevar2;
                        playerScript8.scorevar_5 = scorevar3;
                        playerScript8.scorevar_6 = scorevar4;
                        playerScript8.restartStopwatch ();
                        playerScript8.flag++;
                        self.SetActive (false);
                        return;
                    } else if (scorevar4 == 20 && scorevar_4 == 110) {
                        StartCoroutine (ShowMessage1 (0.5f));
                        return;
                    }
                }

            }

            // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
            // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            // reference.Child ("users").Child (username).Child ("sorting").Child ("s" + session).Child ("savedState").Child ("onquestionNo").SetValueAsync ((indicator + qh).ToString ());
            // // Debug.Log(indicator);
            // if (indicator > 3)
            // {
            //     randomize();
            //     var++;
            // }
            animation ();
            indicator++;
            if (number == 123) {
                if (indicator == 2) {
                    StartCoroutine (ShowMessage (0, 1f));
                } else if (indicator == 4) {
                    StartCoroutine (ShowMessage (1, 1f));
                }
            } else {
                if (indicator == 2) {
                    StartCoroutine (ShowMessage (3, 1f));
                } else if (indicator == 4) {
                    StartCoroutine (ShowMessage (4, 1f));
                }
            }
            setInitialPosition ();
            Debug.Log (indicator);
            Invoke ("render", 0.5f);

            // render();
        }

    }
    public void onclickleft () {
        //change
        timer.Restart ();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/sorting/s" + session + "/q" + (indicator + qh))
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
        ////

        if (DOTween.TotalPlayingTweens () == 0) {
            if (indicator == 0 || (indicator >= 0 && indicator <= 1 && scorevar2 == 20)) {
                if (number != 123) {
                    up.SetActive (true);
                    GameObject thePlayer = GameObject.Find ("LevelManager1");
                    SortingS1 playerScript = thePlayer.GetComponent<SortingS1> ();
                    if (scorevar_3 < 20) {
                        playerScript.flag++;
                    }
                    if (scorevar_3 == 20 && scorevar_2 < 20) {
                        if (playerScript.indicator != 3) {
                            playerScript.indicator = 4;
                            playerScript.onclickleft ();
                            playerScript.flag += 3;
                        }

                    } else if (scorevar_3 == 20 && scorevar_2 == 20 && scorevar_1 < 20) {
                        if (playerScript.indicator != 1) {
                            playerScript.indicator = 2;
                            playerScript.onclickleft ();
                            playerScript.flag += 4;
                        }
                    }
                    playerScript.restartStopwatch ();
                    playerScript.scorevar = scorevar;
                    playerScript.scorevar_1 = scorevar2;
                    playerScript.scorevar_2 = scorevar3;
                    playerScript.scorevar_3 = scorevar4;
                    playerScript.scorevar_4 = scorevar_4;

                    self.SetActive (false);
                    return;
                } else {
                    SceneManager.LoadScene ("Game Selection");

                }

            }
            if (number != 123) {
                if (indicator >= 4) {
                    if ((indicator == 4 || (indicator >= 4 && indicator <= 5 && scorevar4 == 20)) && scorevar3 == 20 && scorevar2 == 20 && (scorevar_1 < 20 || scorevar_2 < 20 || scorevar_3 < 20)) {
                        up.SetActive (true);
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

                        } else if (scorevar_3 == 20 && scorevar_2 == 20 && scorevar_1 < 20) {
                            if (playerScript1.indicator != 1) {
                                playerScript1.indicator = 2;
                                playerScript1.onclickleft ();
                                playerScript1.flag += 4;
                            }
                        }
                        playerScript1.restartStopwatch ();
                        playerScript1.scorevar = scorevar;
                        playerScript1.scorevar_1 = scorevar2;
                        playerScript1.scorevar_2 = scorevar3;
                        playerScript1.scorevar_3 = scorevar4;
                        playerScript1.scorevar_4 = scorevar_4;

                        self.SetActive (false);
                        return;
                    }
                }
                if (indicator >= 2 && indicator <= 3) {
                    if ((indicator == 2 || (indicator >= 2 && indicator <= 3 && scorevar3 == 20)) && scorevar2 == 20 && (scorevar_1 < 20 || scorevar_2 < 20 || scorevar_3 < 20)) {
                        up.SetActive (true);
                        GameObject thePlayer2 = GameObject.Find ("LevelManager1");
                        SortingS1 playerScript2 = thePlayer2.GetComponent<SortingS1> ();
                        if (scorevar_3 < 20) {
                            playerScript2.flag++;
                        }
                        if (scorevar_3 == 20 && scorevar_2 < 20) {
                            // playerScript2.indicator = 4;
                            playerScript2.onclickleft ();
                        } else if (scorevar_3 == 20 && scorevar_2 == 20 && scorevar_1 < 20) {
                            playerScript2.indicator = 2;
                            playerScript2.onclickleft ();
                        }
                        playerScript2.restartStopwatch ();
                        playerScript2.scorevar = scorevar;
                        playerScript2.scorevar_1 = scorevar2;
                        playerScript2.scorevar_2 = scorevar3;
                        playerScript2.scorevar_3 = scorevar4;
                        playerScript2.scorevar_4 = scorevar_4;

                        self.SetActive (false);
                        return;
                    }
                }
            }

            if (number == 123) {
                if ((indicator == 3 && scorevar2 == 20) || (indicator == 5 && scorevar2 == 20 && scorevar3 == 20)) {
                    leftarrow.SetActive (false);
                }
            } else {
                if ((indicator == 3 && scorevar2 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20) || (indicator == 5 && scorevar3 == 20 && scorevar2 == 20 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20) || (indicator == 1 && scorevar_1 == 20 && scorevar_2 == 20 && scorevar_3 == 20)) {
                    leftarrow.SetActive (false);
                }
            }
            if (number == 123) {
                if (indicator == 2) {
                    StartCoroutine (ShowMessage (2, 1f));
                } else if (indicator == 4) {
                    StartCoroutine (ShowMessage (0, 1f));
                }
            } else {
                if (indicator == 2) {
                    StartCoroutine (ShowMessage (1, 1f));
                } else if (indicator == 4) {
                    StartCoroutine (ShowMessage (3, 1f));
                }
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

            var --;

            if (indicator != 0) {
                anf = 270;
                a = 1;

                animation ();

                gobackword ();

                Debug.Log (indicator);
                // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tinyhands-fyp.firebaseio.com/");
                // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                // reference.Child ("users").Child (username).Child ("sorting").Child ("s" + session).Child ("savedState").Child ("onquestionNo").SetValueAsync ((indicator + qh).ToString ());
                // if (indicator == 3)
                // {
                //     randomize();
                // }

                setInitialPosition ();
                Invoke ("render", 0.5f);
                // render();

            }
            for (int i = 0; i < 30; i++) {
                if (objects[i].transform.position == objects2[i].transform.position) {
                    objects2[i].SetActive (false);
                }
            }
        }

    }

    public void gobackword () {

        if ((indicator >= 4 && scorevar4 == 20 || indicator == 4) && scorevar3 < 20) {
            indicator = 3;
            return;
        }
        if ((indicator >= 4 && scorevar4 == 20 || indicator == 4) && scorevar3 == 20 && scorevar2 < 20) {
            indicator = 1;
            return;
        }
        if ((indicator >= 2 && indicator <= 3 && scorevar3 == 20 || indicator == 2) && scorevar2 < 20) {
            indicator = 1;
            return;
        }
        indicator--;

    }

    public void animation () {
        for (int i = 0; i < cards.Length; i++) {
            cards[i].transform.DORotate (new Vector3 (0, cards[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
            objects[i + ((indicator) * 5)].transform.DORotate (new Vector3 (0, objects[i + ((indicator) * 5)].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
            cards2[i].transform.DORotate (new Vector3 (0, cards2[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
            objects2[i + ((indicator) * 5)].transform.DORotate (new Vector3 (0, objects2[i + ((indicator) * 5)].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);

        }
    }

    public void levelanimation () {
        GameObject level = GameObject.Find ("animation");
        // level.PLay();

    }
    public void render () {
        for (int i = 0; i < 30; i++) {
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
                int x = (p % 5) + 1;
                destination = ((p % 5) + 1).ToString ();
                if (indicator > 3) {
                    if (x == 1)
                        destination = "5";
                    if (x == 2)
                        destination = "4";
                    if (x == 4)
                        destination = "2";
                    if (x == 5)
                        destination = "1";
                }
            }
        }
        float Distance = Vector3.Distance (objects[a].transform.position, objects2[a].transform.position);
        if (Distance < 10) {
            int y = (a % 5) + 1;
            destination = ((a % 5) + 1).ToString ();
            if (indicator > 3) {
                if (y == 1)
                    destination = "5";
                if (y == 2)
                    destination = "4";
                if (y == 4)
                    destination = "2";
                if (y == 5)
                    destination = "1";
            }
            objects[a].transform.position = objects2[a].transform.position;
            //  objects2[a].GetActive
            // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
            // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

            scorevar = scorevar + 2;
            // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child("scorevar").SetValueAsync(scorevar);

            dropped[indicator]++;
            ///////////////////////////////
            if (indicator >= 0 && indicator <= 1) {
                scorevar2 = scorevar2 + 2;
                // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child(gType).Child("scorevar2").SetValueAsync(scorevar2);

            } else if (indicator >= 2 && indicator <= 3) {
                scorevar3 = scorevar3 + 2;
                // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child(gType).Child("scorevar3").SetValueAsync(scorevar3);

            } else {
                scorevar4 = scorevar4 + 2;
                // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child(gType).Child("scorevar4").SetValueAsync(scorevar4);

            }

            /////////////////////////////////////////
            // successflag();

            SettingsPenal.playeffect (0);

            objects2[a].SetActive (false);

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
            StartCoroutine (updateScore (username, gType, scorevar, scorevar2, scorevar3, scorevar4, solved));

        }

    }
    /////

    void Update () {

        if (indicator <= 3) {
            header.text = "" + headertext;
        } else {
            header.text = "" + headertext1;

        }

        if (datacheck == 3) {
            resumePop.SetActive (false);
            jump ();
            datacheck++;
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

        // Cursor.lockState = CursorLockMode.Confined;

        if (flag == 1) {
            StartCoroutine (ShowMessage (1, 0.5f));
            flag--;
        }
        if (flag == 2) {
            StartCoroutine (ShowMessage (4, 0.5f));
            flag -= 2;
        }
        if (flag == 4) {
            StartCoroutine (ShowMessage (2, 0.5f));
            flag -= 4;
        }
        if (flag == 3) {
            StartCoroutine (ShowMessage (3, 0.5f));
            flag -= 3;
        }
        // newText.text = "" + scorevar;

    }
}

//change
class Atempt {
    public string reposnseTime;
    public string dragTime;
    public string status;
    public string dragDistance;
    public string dropDistance;
    public string source;
    public string destination;
    public string timeStamp;

    public Atempt () { }

    public Atempt (string reposnseTime, string dragTime, string status, string dragDistance, string dropDistance, string source, string destination) {
        this.reposnseTime = reposnseTime;
        this.dragTime = dragTime;
        this.status = status;
        this.dragDistance = dragDistance;
        this.dropDistance = dropDistance;
        this.source = source;
        this.destination = destination;
        this.timeStamp = DateTime.Now.ToString ("MM/dd/yyyy HH:mm:ss");

    }

}
/////

// class Resume
// {
//     public string positions;
//     public string solved;
//     public int score;
//     public int score2;
//     public int score3;
//     public int score4;
// }