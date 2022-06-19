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

public class game6l3 : MonoBehaviour
{
    public GameObject[] cards;

    public GameObject[] objects;

    private int indicator;
    public static System.Random r = new System.Random();

    public GameObject self;
    public GameObject right;
    public GameObject right1;

    public GameObject wrong;
    public GameObject up;
    public GameObject up_1;
    public GameObject down;
    public GameObject rightarrow;

    public int scorevar = 0;
    public int scorevar1 = 0;
    public int scorevar_1 = 0;
    public int scorevar_2 = 0;

    public Text newText;

    public bool[] record;
    public bool[,] arr;
    public string[] title;
    public Text fortitle;
    public int var;
    public GameObject[] stars;
    public int anf;
    public int a;
    public GameObject Gameover;
    public Text Stagecomplete;
    public string[] Stagetext;
    public GameObject ltext;
    public GameObject finaltext, leftarrow;
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
    /////

    public string[] answers;
    public GameObject resumePop;

    private string postions = "";
    private string solved = "";
    string sPostions = "";
    string sSolved = "";

    // public Score other;

    async void Start()
    {
        //change
        qh = 10;

        answers[0] = "surprised face";
        answers[1] = "weeping face";
        answers[2] = "sad face";
        answers[3] = "happy face";
        answers[4] = "angry face";
        answers[5] = "angry face";
        answers[6] = "surprised face";
        answers[7] = "happy face";
        answers[8] = "sad face";
        answers[9] = "weeping face";
        answers[10] = "angry face";
        answers[11] = "sad face";
        answers[12] = "weeping face";
        answers[13] = "surprised face";
        answers[14] = "happy face";
        answers[15] = "weeping face";
        answers[16] = "happy face";
        answers[17] = "surprised face";
        answers[18] = "angry face";
        answers[19] = "sad face";
        answers[20] = "angry face";
        answers[21] = "sad face";
        answers[22] = "happy face";
        answers[23] = "surprised face";
        answers[24] = "weeping face";

        username = File.ReadAllText(Application.dataPath + "/user.txt");
        // timer = new Stopwatch();
        // timer.Start();
        /////
        // self.SetActive(false);
        arr = new bool[14, 2];
        // Debug.Log(arr[0, 0]);
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

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        //change

        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/" + game)
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
        //     .GetReference("users/" + username + "/" + game + "/s" + session + "/q" + (indicator + 1 + qh))
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
        //      Debug.Log(attemptNo);

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
    IEnumerator updateScore(String user, String game, String gameMode, int score, int score1, int score2, int score3, String solved)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", user);
        form.AddField("game", game);
        form.AddField("gameMode", gameMode);
        form.AddField("score", score);
        form.AddField("score1", score1);
        form.AddField("score2", score2);
        form.AddField("score3", score3);
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
        Debug.Log("6l3");

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
                    scorevar_1 = myObject.score2;
                    scorevar_2 = myObject.score3;

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
                Game6l1 playerScript = thePlayer.GetComponent<Game6l1>();
                playerScript.scorevar_1 = scorevar1;
                playerScript.datacheck++;
                self.SetActive(false);
                timer = new Stopwatch();
                timer.Start();
                resumePop.SetActive(false);
                waiting();
            }
        }
    }

    public void onStarting(bool resume, int sess)
    {
        session = sess;
        Debug.Log("star mode 2");

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

    IEnumerator ShowMessage(int number, float delay)
    {
        Stagecomplete.text = "" + Stagetext[number];

        yield return new WaitForSeconds(delay);
        ltext.SetActive(true);
        yield return new WaitForSeconds(2f);
        ltext.SetActive(false);
    }
    IEnumerator ShowMessage1(float delay)
    {
        yield return new WaitForSeconds(delay);
        finaltext.SetActive(true);
        yield return new WaitForSeconds(2f);
        finaltext.SetActive(false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
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
            scorevar1 = scorevar1 + 2;
            SettingsPenal.playeffect(0);
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
        else if ((((index + 1) % 5 == 4)))
        {
            right1.transform.position = objects[index].transform.position;
            right1.SetActive(true);
            scorevar = scorevar + 2;
            scorevar1 = scorevar1 + 2;
            SettingsPenal.playeffect(0);
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
            // StartCoroutine(Example(false));
            stat = false;
            SettingsPenal.playeffect(1);
            // return;
        }
        // record[indicator] = true;
        if (!(saveData1 && saveData2))
        {
            saveAttempt(reposnseTime, status, title[indicator], answers[index]);
        }
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
            StartCoroutine(updateScore(username, game, "2", scorevar, scorevar1, scorevar_1, scorevar_2, solved));

        }
    }
    /////
    IEnumerator Example(bool flag)
    {

        yield return new WaitForSeconds(0.5f);
        timer.Restart();
        if (flag)
        {
            if (arr[indicator, 0] && arr[indicator, 1])
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
        for (int i = 0; i < 25; i += 5)
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
            if (indicator == 4 && scorevar < 40)
            {
                StartCoroutine(ShowMessage1(0.5f));
                return;
            }
            else if ((indicator >= 0 && indicator <= 4) && scorevar >= 40)
            {
                SettingsPenal.gameover();
                Gameover.SetActive(true);
                Invoke("endanimation", 6.5f);
                return;
            }
            anf = 90;
            a = -1;
            animation();
            indicator++;

            // right.SetActive (arr[indicator, 0]);
            // right1.SetActive (arr[indicator, 1]);

            // right.transform.position = objects[((indicator + 1) * 5) - 1].transform.position;
            // right1.transform.position = objects[((indicator + 1) * 5) - 2].transform.position;
            Invoke("render", 0.5f);
            Invoke("waiting", 0.5f);

        }
        // render();
    }

    async public void onclickleft()
    {
        //change
        // timer.Restart();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/" + game + "/s" + session + "/q" + (indicator + qh))
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
        // ////
        if (DOTween.TotalPlayingTweens() == 0)
        {
            if ((indicator == 0 && scorevar_1 < 10) || (indicator >= 0 && scorevar1 == 20 && scorevar_1 < 10))
            {
                up.SetActive(true);
                if (var == 12)
                {
                    GameObject thePlayer = GameObject.Find("LevelManager1");
                    Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                    playerScript.scorevar = scorevar;
                    playerScript.scorevar_2 = scorevar1;
                    playerScript.restartStopwatch();
                    playerScript.flag++;
                }
                else
                {
                    GameObject thePlayer = GameObject.Find("LevelManager");
                    Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                    playerScript.scorevar = scorevar;
                    playerScript.restartStopwatch();
                }

                self.SetActive(false);
                return;
            }
            if ((indicator == 0 && scorevar_1 == 10 && scorevar_2 < 10) || (indicator >= 0 && scorevar1 == 20 && scorevar_1 == 10 && scorevar_2 < 10))
            {
                up_1.SetActive(true);
                if (var == 12)
                {
                    GameObject thePlayer = GameObject.Find("LevelManager");
                    Game6l1 playerScript = thePlayer.GetComponent<Game6l1>();
                    playerScript.scorevar = scorevar;
                    playerScript.scorevar_1 = scorevar1;
                    playerScript.scorevar_2 = scorevar_1;
                    playerScript.flag++;
                    playerScript.restartStopwatch();
                }
                self.SetActive(false);
                return;
            }
            if (indicator == 1 && scorevar_2 == 10 && scorevar_1 == 10)
            {
                leftarrow.SetActive(false);
            }

            anf = 270;
            a = 1;
            animation();
            if (indicator != 0)
            {
                indicator--;

                Invoke("render", 0.5f);

                // render();

            }

            // Debug.Log("ye wala");
            // Debug.Log(((indicator + 1) * 5) - 1);

            // right.SetActive (arr[indicator, 0]);
            // right1.SetActive (arr[indicator, 1]);

            // right.transform.position = objects[((indicator + 1) * 5) - 1].transform.position;
            // right1.transform.position = objects[((indicator + 1) * 5) - 2].transform.position;
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

    private void render()
    {
        for (int i = 0; i < 25; i++)
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

            if (indicator >= 0)
            {
                objects[i].transform.eulerAngles = new Vector3(objects[i].transform.eulerAngles.x, anf, objects[i].transform.eulerAngles.z);
                right.transform.eulerAngles = new Vector3(right.transform.eulerAngles.x, anf, right.transform.eulerAngles.z);
                right1.transform.eulerAngles = new Vector3(right1.transform.eulerAngles.x, anf, right1.transform.eulerAngles.z);
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
    public void restartTimer()
    {
        timer.Restart();
    }
    void Update()
    {

        fortitle.text = "" + title[indicator];

        if (flag == 1)
        {
            StartCoroutine(ShowMessage(0, 0.5f));
            flag--;
        }
        newText.text = "" + scorevar;
        if (scorevar >= 10)
        {
            stars[0].SetActive(true);
        }
        if (scorevar >= 20)
        {
            stars[1].SetActive(true);
        }
        if (scorevar >= 30)
        {
            stars[2].SetActive(true);
        }
        if (scorevar >= 40)
        {
            stars[3].SetActive(true);
        }

    }
}