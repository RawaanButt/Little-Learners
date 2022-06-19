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

public class game3l2 : MonoBehaviour
{
    public GameObject[] cards;

    public GameObject[] objects;

    public int indicator;
    public static System.Random r = new System.Random();

    public GameObject self;
    public GameObject right;
    public GameObject right1;

    public GameObject wrong;
    public GameObject up;
    public GameObject down;
    public GameObject rightarrow;

    public int scorevar = 0;
    public int scorevar1 = 0;
    public int scorevar2 = 0;
    public int scorevar3 = 0;
    public int scorevar0 = 0;

    public Text newText;

    public bool[] record;
    public bool[,] arr;
    public string[] title;
    public Text fortitle;
    public GameObject[] stars;
    public int anf;
    public int a;
    public GameObject levelcomanim;
    public GameObject Gameover;
    public Text Stagecomplete;
    public string[] Stagetext;
    public GameObject ltext;
    public GameObject finaltext;
    public GameObject leftarrow;

    public int flag;
    //change
    public Stopwatch timer;
    // public bool dragLocker;
    private string reposnseTime;
    private string username;
    private int session;
    private int attemptNo;
    private int qh;
    public string game;

    public string[] answers;
    public GameObject resumePop;

    private string postions = "";
    private string solved = "";
    string sPostions = "";
    string sSolved = "";

    /////
    // public Score other;

    async void Start()
    {
        //change
        qh = 10;

        if (game == "classification")
        {
            answers[0] = "mango";
            answers[1] = "apple";
            answers[2] = "banana";
            answers[3] = "watermelon";
            answers[4] = "potato";
            answers[5] = "parrot";
            answers[6] = "car";
            answers[7] = "table";
            answers[8] = "baby";
            answers[9] = "dog";
            answers[10] = "fork";
            answers[11] = "peach";
            answers[12] = "bus";
            answers[13] = "cat";
            answers[14] = "football";
            answers[15] = "table";
            answers[16] = "book";
            answers[17] = "ladder";
            answers[18] = "box";
            answers[19] = "chair";
            answers[20] = "apple";
            answers[21] = "dog";
            answers[22] = "fish";
            answers[23] = "blue bird";
            answers[24] = "parrot";
            answers[25] = "beetroot";
            answers[26] = "tomato";
            answers[27] = "potato";
            answers[28] = "banana";
            answers[29] = "mango";
            answers[30] = "voilin";
            answers[31] = "shoe";
            answers[32] = "fan";
            answers[33] = "cycle";
            answers[34] = "car";
            answers[35] = "rhino";
            answers[36] = "zebra";
            answers[37] = "elephant";
            answers[38] = "dog";
            answers[39] = "cat";
            answers[40] = "ladder";
            answers[41] = "hammer";
            answers[42] = "voilin";
            answers[43] = "fork";
            answers[44] = "spoon";
            answers[45] = "ring";
            answers[46] = "book";
            answers[47] = "basketball";
            answers[48] = "tomato";
            answers[49] = "peach";
            answers[50] = "parrot";
            answers[51] = "dog";
            answers[52] = "shoe";
            answers[53] = "hammer";
            answers[54] = "cycle";
            answers[55] = "parrot";
            answers[56] = "dog";
            answers[57] = "baby";
            answers[58] = "rhino";
            answers[59] = "fish";
            answers[60] = "candy";
            answers[61] = "cup";
            answers[62] = "box";
            answers[63] = "fork & knife";
            answers[64] = "color pencils";
            answers[65] = "nailcutter";
            answers[66] = "caterpilar";
            answers[67] = "baby";
            answers[68] = "cat";
            answers[69] = "parrot";
        }
        else if (game == "selection")
        {
            answers[0] = "spoon";
            answers[1] = "banana";
            answers[2] = "ladder";
            answers[3] = "voilin";
            answers[4] = "pencil";
            answers[5] = "carrot";
            answers[6] = "bus";
            answers[7] = "dragon";
            answers[8] = "table";
            answers[9] = "aeroplane";
            answers[10] = "fork";
            answers[11] = "train";
            answers[12] = "sofa";
            answers[13] = "alarmclock";
            answers[14] = "cycle";
            answers[15] = "ball";
            answers[16] = "parrot";
            answers[17] = "watermelon";
            answers[18] = "strawberry";
            answers[19] = "apple";
            answers[20] = "book";
            answers[21] = "fan";
            answers[22] = "table";
            answers[23] = "chair";
            answers[24] = "chair";
            answers[25] = "beetroot";
            answers[26] = "butterfly";
            answers[27] = "dog";
            answers[28] = "bee";
            answers[29] = "bee";
            answers[30] = "box";
            answers[31] = "pencil";
            answers[32] = "scale";
            answers[33] = "book";
            answers[34] = "book";
            answers[35] = "spectula";
            answers[36] = "fish";
            answers[37] = "aeroplane";
            answers[38] = "parrot";
            answers[39] = "parrot";
            answers[40] = "bus";
            answers[41] = "aeroplane";
            answers[42] = "train";
            answers[43] = "car";
            answers[44] = "car";
            answers[45] = "box";
            answers[46] = "chair";
            answers[47] = "book";
            answers[48] = "blue t-shirt";
            answers[49] = "towel";
            answers[50] = "cycle";
            answers[51] = "alarmclock";
            answers[52] = "football";
            answers[53] = "ring";
            answers[54] = "fan";
            answers[55] = "elephant";
            answers[56] = "cat";
            answers[57] = "panda";
            answers[58] = "dog";
            answers[59] = "lion";
            answers[60] = "candy";
            answers[61] = "cup";
            answers[62] = "box";
            answers[63] = "door";
            answers[64] = "window";
            answers[65] = "nailcutter";
            answers[66] = "butterfly";
            answers[67] = "shoe";
            answers[68] = "bird";
            answers[69] = "fish";
        }
        else if (game == "adjective")
        {
            answers[0] = "ball";
            answers[1] = "apple";
            answers[2] = "banana";
            answers[3] = "icecream cup";
            answers[4] = "hot tea";
            answers[5] = "parrot";
            answers[6] = "car";
            answers[7] = "table";
            answers[8] = "scale";
            answers[9] = "football";
            answers[10] = "dog";
            answers[11] = "pizza";
            answers[12] = "icecream cup";
            answers[13] = "coat";
            answers[14] = "bricks";
            answers[15] = "teddy bear";
            answers[16] = "yello t-shirt";
            answers[17] = "sun";
            answers[18] = "hot tea";
            answers[19] = "ice";
            answers[20] = "lion";
            answers[21] = "icecream cup";
            answers[22] = "bricks";
            answers[23] = "pillow";
            answers[24] = "tissue";
            answers[25] = "fire";
            answers[26] = "hot tea";
            answers[27] = "football";
            answers[28] = "iceacream";
            answers[29] = "ice";
            answers[30] = "table";
            answers[31] = "bus";
            answers[32] = "elephant";
            answers[33] = "sharpener";
            answers[34] = "candy";
            answers[35] = "elephant";
            answers[36] = "pillow";
            answers[37] = "fire";
            answers[38] = "bricks";
            answers[39] = "wood";
            answers[40] = "teddy bear";
            answers[41] = "cycle";
            answers[42] = "table";
            answers[43] = "rhino";
            answers[44] = "bus";
            answers[45] = "candy";
            answers[46] = "pillow";
            answers[47] = "tissue";
            answers[48] = "icecream";
            answers[49] = "hot tea";
            answers[50] = "table";
            answers[51] = "fire";
            answers[52] = "pizza";
            answers[53] = "cat";
            answers[54] = "elephant";
            answers[55] = "icecream";
            answers[56] = "fire";
            answers[57] = "ring";
            answers[58] = "tissue";
            answers[59] = "wood";
            answers[60] = "sun";
            answers[61] = "pizza";
            answers[62] = "bus";
            answers[63] = "clean dishes";
            answers[64] = "dirty dishes";
            answers[65] = "hot tea";
            answers[66] = "cat";
            answers[67] = "pillow";
            answers[68] = "pencil";
            answers[69] = "bat";
        }

        username = File.ReadAllText(Application.dataPath + "/user.txt");
        /////

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
        arr = new bool[14, 2];

        // self.SetActive(false);

        indicator = 0;
        render();
        randomize();
        right.SetActive(false);
        right1.SetActive(false);

        wrong.SetActive(false);

        // setInitialPosition();

        // other = new Score();

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
        // Debug.Log(attemptNo);

        // timer = new Stopwatch();
        // timer.Start();

        /////

    }

    IEnumerator Upload(String user, String game, String gameMode, String positions)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", user);
        form.AddField("game", game);
        form.AddField("gameMode", gameMode);
        form.AddField("positions", positions);

        Debug.Log(positions);

        using (UnityWebRequest www = UnityWebRequest.Post("https://evening-fortress-14821.herokuapp.com/select", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("game mode 2 - save");
            }
        }
    }
    IEnumerator updateScore(String user, String game, String gameMode, int score, int score1, int score2, int score3, int score4, String solved)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", user);
        form.AddField("game", game);
        form.AddField("gameMode", gameMode);
        form.AddField("score", score);
        form.AddField("score1", score1);
        form.AddField("score2", score2);
        form.AddField("score3", score3);
        form.AddField("score4", score4);
        form.AddField("solved", solved);

        using (UnityWebRequest www = UnityWebRequest.Post("https://evening-fortress-14821.herokuapp.com/select/score", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("score upload complete!");
            }
        }
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Resume myObject = JsonUtility.FromJson<Resume>(webRequest.downloadHandler.text);

                // scorevar = sv;
                // scorevar1 = sv2;
                // scorevar2 = sv3;
                // scorevar3 = sv4;

                sPostions = myObject.positions;

                if (myObject.solved != null)
                {
                    sSolved = myObject.solved.Length > 0 ? myObject.solved : sSolved;
                }
                if (myObject.score != null)
                {
                    scorevar = myObject.score;
                }
                if (myObject.score1 != null)
                {
                    scorevar1 = myObject.score1;
                    scorevar2 = myObject.score2;
                    scorevar3 = myObject.score3;
                    scorevar0 = myObject.score4;

                }

                if (sPostions.Length > 0)
                {
                    string[] posPairs = sPostions.Split(',');
                    foreach (string posPair in posPairs)
                    {
                        string[] post = posPair.Split('-');
                        objects[Int32.Parse(post[0])].transform.position = cards[Int32.Parse(post[1])].transform.position;
                    }
                }
                if (sSolved.Length > 0)
                {
                    solved = sSolved;
                    string[] sols = sSolved.Split(',');
                    foreach (string sol in sols)
                    {

                        arr[Int32.Parse(sol[0].ToString()), Int32.Parse(sol[2].ToString())] = true;
                        // dropped[(int)Decimal.Truncate(Int32.Parse(sol) / 5)]++;
                        // objects[Int32.Parse(sol)].transform.position = objects2[Int32.Parse(sol)].transform.position;

                    }
                }
                GameObject thePlayer = GameObject.Find("LevelManager");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                playerScript.datacheck++;
                self.SetActive(false);
                timer = new Stopwatch();
                timer.Start();
                resumePop.SetActive(false);
                // waiting ();
            }
        }
    }

    public void onStarting(bool resume, int sess)
    {
        session = sess;

        if (resume)
        {
            StartCoroutine(GetRequest("https://evening-fortress-14821.herokuapp.com/select/" + username + "/" + game + "/2"));
        }
        else
        {
            StartCoroutine(Upload(username, game, "2", postions));

            self.SetActive(false);

            timer = new Stopwatch();
            timer.Start();
            resumePop.SetActive(false);
        }
    }

    public void restartTimer()
    {
        timer.Restart();
    }
    IEnumerator ShowMessage(int number, float delay)
    {
        Stagecomplete.text = "" + Stagetext[number];
        yield return new WaitForSeconds(delay);
        ltext.SetActive(true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds(2f);
        ltext.SetActive(false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }
    IEnumerator ShowMessage1(float delay)
    {
        yield return new WaitForSeconds(delay);
        finaltext.SetActive(true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds(2f);
        finaltext.SetActive(false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }

    public void endanim()
    {
        levelcomanim.SetActive(false);
    }
    public async void restartStopwatch()
    {
        try
        {
            timer.Restart();
        }
        catch
        {
            Debug.Log("re");
        }
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
    public void onclick(int index)
    {
        //change
        reposnseTime = timer.Elapsed.TotalSeconds.ToString("0.##");
        string status = "wrong";
        bool saveData1 = arr[indicator, 0];
        bool saveData2 = arr[indicator, 1];
        ////

        bool stat;
        if (((index + 1) % 5 == 0))
        {
            right.transform.position = objects[index].transform.position;
            right.SetActive(true);
            scorevar = scorevar + 2;
            SettingsPenal.playeffect(0);

            ////////////
            if (indicator >= 0 && indicator <= 3)
            {
                scorevar1 = scorevar1 + 2;
            }
            else if (indicator >= 4 && indicator <= 8)
            {
                scorevar2 = scorevar2 + 2;
            }
            else
            {
                scorevar3 = scorevar3 + 2;
            }
            ////////////
            arr[indicator, 0] = true;
            status = "right";
            if (solved.Length > 0)
            {
                solved = solved + "," + indicator.ToString() + "-0";
            }
            else
            {
                solved = indicator.ToString() + "-0";
            }
            stat = true;

        }
        else if ((((index + 1) % 5 == 4) && indicator > 3))
        {
            right1.transform.position = objects[index].transform.position;
            right1.SetActive(true);
            scorevar = scorevar + 2;
            SettingsPenal.playeffect(0);

            /////////////////
            if (indicator >= 0 && indicator <= 3)
            {
                scorevar1 = scorevar1 + 2;
            }
            else if (indicator >= 4 && indicator <= 8)
            {
                scorevar2 = scorevar2 + 2;
            }
            else
            {
                scorevar3 = scorevar3 + 2;
            }
            //////////////
            arr[indicator, 1] = true;
            status = "right";
            if (solved.Length > 0)
            {
                solved = solved + "," + indicator.ToString() + "-1";
            }
            else
            {
                solved = indicator.ToString() + "-1";
            }
            stat = true;

        }
        else
        {
            wrong.transform.position = objects[index].transform.position;
            wrong.SetActive(true);
            stat = false;
            // StartCoroutine(Example(false));
            SettingsPenal.playeffect(1);
            // return;
        }
        // record[indicator] = true;
        //change'
        if (indicator > 3)
        {
            if (!(saveData1 && saveData2))
            {
                saveAttempt(reposnseTime, status, title[indicator], answers[index]);
            }
        }
        else
        {
            if (!saveData1)
            {
                saveAttempt(reposnseTime, status, title[indicator], answers[index]);
            }
        }

        /////
        StartCoroutine(Example(stat));
    }

    //change
    IEnumerator updateAnalytics(String user, String game, int session, int questionNo, string reposnseTime, string status, string question, string answer)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", user);
        form.AddField("game", game);
        form.AddField("session", session);
        form.AddField("questionNo", questionNo);
        form.AddField("reposnseTime", reposnseTime);
        form.AddField("status", status);
        form.AddField("question", question);
        form.AddField("answer", answer);

        using (UnityWebRequest www = UnityWebRequest.Post("https://evening-fortress-14821.herokuapp.com/analytics", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("attemmpt upload complete!");
            }
        }
    }
    async void saveAttempt(string reposnseTime, string status, string question, string answer)
    {

        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // AtemptClassification atempt = new AtemptClassification(reposnseTime, status, question, answer);
        // string json = JsonUtility.ToJson(atempt);

        // reference.Child("users").Child(username).Child(game).Child("s" + session).Child("q" + (indicator + 1 + qh)).Child("a" + attemptNo).SetRawJsonValueAsync(json);
        // attemptNo++;

        int questionNo = indicator + 1 + qh;
        StartCoroutine(updateAnalytics(username, game, session, questionNo, reposnseTime, status, question, answer));

        if (status == "right")
        {
            // reference.Child("users").Child(username).Child("savedState").Child("sorting").Child(gType).Child("solved").SetValueAsync(solved);
            StartCoroutine(updateScore(username, game, "2", scorevar, scorevar1, scorevar2, scorevar3, scorevar0, solved));

        }

    }
    /////
    IEnumerator Example(bool flag)
    {

        yield return new WaitForSeconds(0.5f);
        timer.Restart();
        if (flag)
        {
            if (indicator > 3)
            {
                if (arr[indicator, 0] && arr[indicator, 1])
                {
                    onclickright();

                }
            }
            else
            {
                onclickright();
            }

            // right.SetActive(false);

        }
        else
        {
            wrong.SetActive(false);
        }
    }

    public void randomize()
    {
        for (int i = 0; i < 70; i += 5)
        {

            List<int> termsList = new List<int>();
            List<int> termsList2 = new List<int>();

            for (int j = 0; j < 5; j++)
            {

                int pos = random_except_list(5, termsList.ToArray());
                termsList.Add(pos);
                termsList.Sort();

                objects[i + j].transform.position = cards[pos].transform.position;
                if (postions.Length > 0)
                {
                    postions = postions + "," + (i + j).ToString() + "-" + pos.ToString();
                }
                else
                {
                    postions = (i + j).ToString() + "-" + pos.ToString();
                }

            }
        }
    }
    public void endanimation()
    {
        // Gameover.SetActive (false);
        SceneManager.LoadScene("Game Selection");
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        endanim();
        if ((indicator >= 0 && indicator <= 3 && (scorevar1 < 8 || scorevar0 < 20) && scorevar2 == 20 && scorevar3 == 20) || (indicator >= 4 && indicator <= 8 && (scorevar0 < 20 || scorevar1 < 8) && scorevar3 == 20))
        {
            goforward();
            // yield return 0;
            yield break;
        }
        animation();
        goforward();
        if (indicator == 4)
        {
            StartCoroutine(ShowMessage(1, 3.7f));
        }
        else if (indicator == 9)
        {
            StartCoroutine(ShowMessage(2, 3.7f));
        }
        Invoke("render", 0.5f);
        Invoke("waiting", 0.5f);
    }
    async public void onclickright()
    {
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
        if (DOTween.TotalPlayingTweens() == 0)
        {
            leftarrow.SetActive(true);
            //68
            if (indicator == 13 && scorevar < 68)
            {
                StartCoroutine(ShowMessage1(0.5f));
                return;
            }
            else if ((indicator >= 0 && indicator <= 13) && scorevar >= 68)
            {
                SettingsPenal.gameover();
                Gameover.SetActive(true);
                Invoke("endanimation", 6.5f);
                return;
            }
            anf = 90;
            a = -1;
            ///////////////////////////////////////////////////////
            if (((indicator >= 0 && indicator <= 3) && scorevar1 == 8) || ((indicator >= 4 && indicator <= 8) && scorevar2 == 20))
            {
                levelcomanim.SetActive(true);
                SettingsPenal.playanimation();

                StartCoroutine(wait());
                return;
            }
            ////////////////////////////////////

            if ((indicator == 3 && (scorevar1 < 8 || scorevar0 < 20) && scorevar2 == 20 && scorevar3 == 20) || (indicator == 8 && scorevar3 == 20))
            {
                Debug.Log("yoyoy");
                goforward();
                return;
            }
            animation();
            // indicator++;
            goforward();
            if (indicator == 4)
            {
                StartCoroutine(ShowMessage(1, 0.5f));
            }
            else if (indicator == 9)
            {
                StartCoroutine(ShowMessage(2, 0.5f));
            }

            // right.SetActive (arr[indicator, 0]);
            // right1.SetActive (arr[indicator, 1]);

            // right.transform.position = objects[((indicator + 1) * 5) - 1].transform.position;
            // right1.transform.position = objects[((indicator + 1) * 5) - 2].transform.position;
            Invoke("render", 0.5f);
            Invoke("waiting", 0.5f);

            // render();
        }

    }
    public void goforward()
    {

        if (((indicator >= 0 && indicator <= 3 && scorevar1 == 8) || (indicator == 3 && scorevar1 < 8)) && scorevar2 == 20 && scorevar3 == 20)
        {
            StartCoroutine(ShowMessage1(0.5f));
            return;
        }

        if (indicator >= 0 && indicator <= 3 && scorevar1 == 8 && scorevar2 < 20)
        {
            indicator = 4;
            if (scorevar0 == 20)
            {
                leftarrow.SetActive(false);
            }
            return;
        }
        if (((indicator >= 0 && indicator <= 3 && scorevar1 == 8) || (indicator == 3 && scorevar1 < 8)) && scorevar2 == 20 && scorevar3 <= 20)
        {
            indicator = 9;

            if (scorevar0 == 20 && scorevar1 == 8)
            {
                leftarrow.SetActive(false);
            }
            return;
        }
        if (indicator >= 4 && indicator <= 8 && scorevar2 == 20 && scorevar3 < 20)
        {
            indicator = 9;
            if (scorevar0 == 20 && scorevar1 == 8)
            {
                leftarrow.SetActive(false);
            }
            return;
        }
        if (((indicator >= 4 && indicator <= 8 && scorevar2 == 20) || (indicator == 8 && scorevar2 < 20)) && (scorevar1 < 8 || scorevar0 < 20) && scorevar3 == 20)
        {
            StartCoroutine(ShowMessage1(0.5f));
            return;
        }
        // if ((indicator == 3 && scorevar2 == 20 && scorevar3 == 20)||(indicator==)) {

        // }
        indicator++;

    }
    public void gobackword()
    {

        if (indicator >= 9 && scorevar3 == 20 && scorevar2 < 20)
        {
            indicator = 8;
            return;
        }
        if ((indicator >= 9 && scorevar3 == 20 && scorevar2 == 20 && scorevar1 < 8) || (indicator >= 4 && indicator <= 8 && scorevar2 == 20 && scorevar1 < 8))
        {
            indicator = 3;
            return;
        }
        if (indicator == 9 && scorevar2 == 20 && scorevar1 < 8)
        {
            indicator = 3;
            return;
        }
        indicator--;

    }
    async public void onclickleft()
    {
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
        if (DOTween.TotalPlayingTweens() == 0)
        {
            if ((indicator == 0) || (indicator == 4 && scorevar1 == 8 && scorevar0 < 20) || (indicator == 9 && scorevar1 == 8 && scorevar2 == 20 && scorevar0 < 20) || (indicator >= 0 && indicator <= 3 && scorevar1 == 8 && scorevar0 < 20))
            {
                up.SetActive(true);
                GameObject thePlayer = GameObject.Find("LevelManager");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                playerScript.scorevar = scorevar;
                playerScript.scorevar_1 = scorevar1;
                playerScript.scorevar_2 = scorevar2;
                playerScript.scorevar_3 = scorevar3;
                playerScript.restartStopwatch();
                playerScript.flag++;
                self.SetActive(false);
                return;
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

            if ((indicator == 10 && scorevar2 == 20 && scorevar1 == 8 && scorevar0 == 20) || (indicator == 5 && scorevar1 == 8 && scorevar0 == 20) || (indicator == 1 && scorevar0 == 20))
            {
                leftarrow.SetActive(false);
            }

            anf = 270;
            a = 1;
            animation();

            if (indicator != 0)
            {
                gobackword();

                Invoke("render", 0.5f);
                // render();

            }
            if (indicator == 8)
            {
                StartCoroutine(ShowMessage(1, 0.7f));
            }
            else if (indicator == 3)
            {
                StartCoroutine(ShowMessage(0, 0.7f));
            }

            // Debug.Log("ye wala");
            // Debug.Log(((indicator + 1) * 5) - 1);
            Invoke("waiting", 0.5f);

        }

    }
    public void waiting()
    {
        right.SetActive(arr[indicator, 0]);
        right1.SetActive(arr[indicator, 1]);

        right.transform.position = objects[((indicator + 1) * 5) - 1].transform.position;
        right1.transform.position = objects[((indicator + 1) * 5) - 2].transform.position;

    }
    public void animation()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.DORotate(new Vector3(0, cards[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);

            objects[i + ((indicator) * 5)].transform.DORotate(new Vector3(0, objects[i + ((indicator) * 5)].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
        }
        right.transform.DORotate(new Vector3(0, right.transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
        right1.transform.DORotate(new Vector3(0, right1.transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);

    }

    public void render()
    {
        for (int i = 0; i < 70; i++)
        {
            // if (i < 3)
            // {
            //     // cards2[i].SetActive(true);
            // }
            objects[i].SetActive(false);
            // objects2[i].SetActive(false);
            if ((indicator == 0) && (i >= 0 && i < 5))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);

            }
            else if ((indicator == 1) && (i >= 5 && i < 10))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 2) && (i >= 10 && i < 15))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 3) && (i >= 15 && i < 20))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 4) && (i >= 20 && i < 25))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 5) && (i >= 25 && i < 30))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 6) && (i >= 30 && i < 35))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 7) && (i >= 35 && i < 40))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 8) && (i >= 40 && i < 45))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 9) && (i >= 45 && i < 50))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 10) && (i >= 50 && i < 55))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 11) && (i >= 55 && i < 60))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 12) && (i >= 60 && i < 65))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }
            else if ((indicator == 13) && (i >= 65 && i < 70))
            {
                objects[i].SetActive(true);
                // objects2[i].SetActive(true);
            }

            if (indicator >= 0)
            {
                objects[i].transform.eulerAngles = new Vector3(objects[i].transform.eulerAngles.x, anf, objects[i].transform.eulerAngles.z);
                right.transform.eulerAngles = new Vector3(right.transform.eulerAngles.x, anf, right.transform.eulerAngles.z);
                right1.transform.eulerAngles = new Vector3(right1.transform.eulerAngles.x, anf, right.transform.eulerAngles.z);
            }

        }
        if (indicator >= 0)
        {
            animation();
        }

    }
    public static int random_except_list(int n, int[] x)
    {
        int result = r.Next(n - x.Length);

        for (int i = 0; i < x.Length; i++)
        {
            if (result < x[i])
                return result;
            result++;
        }
        return result;
    }

    void Update()
    {

        fortitle.text = "" + title[indicator];

        // if (flag == 5)
        // {
        //     flaag();
        // }
        newText.text = "" + scorevar;

        if (scorevar >= 17)
        {
            stars[0].SetActive(true);
        }
        if (scorevar >= 34)
        {
            stars[1].SetActive(true);
        }
        if (scorevar >= 51)
        {
            stars[2].SetActive(true);
        }
        if (scorevar >= 68)
        {
            stars[3].SetActive(true);
        }
        if (flag == 1)
        {
            StartCoroutine(ShowMessage(0, 0.5f));
            flag--;
        }
        if (flag == 2)
        {
            StartCoroutine(ShowMessage(1, 0.5f));
            flag -= 2;
        }
        if (flag == 3)
        {
            StartCoroutine(ShowMessage(2, 0.5f));
            flag -= 3;
        }
    }

}