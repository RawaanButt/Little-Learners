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

public class Game6l1 : MonoBehaviour
{
    private int indicator;
    public GameObject[] objects;
    public GameObject right;
    public GameObject wrong;
    public bool[] record;
    public GameObject up;
    public GameObject down;
    public GameObject down_1;
    public GameObject self;
    public string[] title;
    public Text fortitle;
    public int scorevar = 0, flag;
    public int scorevar2 = 0;
    public int scorevar_1 = 0;
    public int scorevar_2 = 0;
    public GameObject Gameover;
    public GameObject finaltext;
    public Text newText;
    public GameObject[] stars;
    public int anf;
    public int a;
    public GameObject levelcomanim;
    public Text Stagecomplete;
    public string Stagetext;
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
    private string solved = "";
    string sSolved = "";
    public GameObject resumePop;
    public int datacheck;
    /////
    public string[] options;

    async void Start()
    {

        //change
        qh = 0;

        options[0] = "happy face";
        options[1] = "weepin/g face";
        options[2] = "angry face";
        options[3] = "surprised face";
        options[4] = "sad face";

        username = File.ReadAllText(Application.dataPath + "/user.txt");
        /////

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
        indicator = 0;
        right.SetActive(false);
        wrong.SetActive(false);
        render();

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

        // timer = new Stopwatch ();
        // timer.Start ();

        /////
    }

    IEnumerator Upload(String user, String game, String gameMode)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", user);
        form.AddField("game", game);
        form.AddField("gameMode", gameMode);
        // form.AddField("positions", positions);

        using (UnityWebRequest www = UnityWebRequest.Post("https://evening-fortress-14821.herokuapp.com/select", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    public void jump()
    {
        Debug.Log("in jump");
        if (scorevar2 == 10)
        {
            if (scorevar_2 < 10)
            {
                down.SetActive(true);
                GameObject thePlayer = GameObject.Find("LevelManager1");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                playerScript.leftarrow.SetActive(false);
                playerScript.scorevar = scorevar;
                playerScript.scorevar_1 = scorevar2;
                playerScript.restartStopwatch();
                playerScript.flag++;
                self.SetActive(false);
            }
            else if (scorevar_2 == 10 && scorevar_1 < 20)
            {
                down_1.SetActive(true);
                GameObject thePlayer1 = GameObject.Find("LevelManager2");
                game6l3 playerScript1 = thePlayer1.GetComponent<game6l3>();
                playerScript1.leftarrow.SetActive(false);
                playerScript1.scorevar = scorevar;
                playerScript1.scorevar_2 = scorevar2;
                playerScript1.scorevar_1 = scorevar_2;
                playerScript1.restartStopwatch();
                playerScript1.flag++;
                self.SetActive(false);

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

                // sPostions = myObject.positions;

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
                    scorevar2 = myObject.score1;
                    scorevar_1 = myObject.score2;
                    scorevar_2 = myObject.score3;
                }

                // if (sPostions.Length > 0)
                // {
                //     string[] posPairs = sPostions.Split(',');
                //     foreach (string posPair in posPairs)
                //     {
                //         string[] post = posPair.Split('-');
                //         objects[Int32.Parse(post[0])].transform.position = cards[Int32.Parse(post[1])].transform.position;
                //     }
                // }
                if (sSolved.Length > 0)
                {
                    solved = sSolved;
                    string[] sols = sSolved.Split(',');
                    foreach (string sol in sols)
                    {
                        record[Int32.Parse(sol)] = true;
                    }
                }
                // if (var == 12 || alphabet == "abc")
                // {
                //     self.SetActive(false);
                // }

                timer = new Stopwatch();
                timer.Start();
                datacheck++;

            }
        }
    }

    public void onStarting(bool resume, int sess)
    {
        session = sess;

        if (resume)
        {
            StartCoroutine(GetRequest("https://evening-fortress-14821.herokuapp.com/select/" + username + "/" + game + "/3"));
        }
        else
        {
            Debug.Log("Cal,,4");

            StartCoroutine(Upload(username, game, "3"));

            timer = new Stopwatch();
            timer.Start();
            resumePop.SetActive(false);
        }
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
            // Debug.Log(indicator);
            if (indicator == 0)
            {
                SceneManager.LoadScene("Game Selection");
                return;

            }
            anf = 270;
            a = 1;
            animation();
            indicator--;

            // right.SetActive (record[indicator]);
            Invoke("render", 0.6f);
            Invoke("waiting", 0.6f);

            // render();
        }

    }
    ////////////////////////////Level complete animation ////////////////////
    public void endanim()
    {
        levelcomanim.SetActive(false);
    }
    public void ddown()
    {
        if (scorevar_2 == 10)
        {
            down_1.SetActive(true);
            GameObject thePlayer1 = GameObject.Find("LevelManager2");
            game6l3 playerScript1 = thePlayer1.GetComponent<game6l3>();
            playerScript1.scorevar = scorevar;
            playerScript1.scorevar_2 = scorevar2;
            playerScript1.scorevar_1 = scorevar_2;
            playerScript1.restartStopwatch();
            playerScript1.flag++;
            playerScript1.leftarrow.SetActive(false);
            self.SetActive(false);
            return;

        }
        down.SetActive(true);
        GameObject thePlayer = GameObject.Find("LevelManager1");
        Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
        playerScript.scorevar = scorevar;
        playerScript.scorevar_1 = scorevar2;
        playerScript.restartStopwatch();
        playerScript.leftarrow.SetActive(false);
        playerScript.flag++;
        self.SetActive(false);
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
    public void endanimation()
    {
        // Gameover.SetActive (false);
        SceneManager.LoadScene("Game Selection");
    }
    IEnumerator ShowMessage1(float delay)
    {
        yield return new WaitForSeconds(delay);
        finaltext.SetActive(true);
        yield return new WaitForSeconds(2f);
        finaltext.SetActive(false);
    }
    IEnumerator ShowMessage(float delay)
    {
        Debug.Log("i am here");
        Stagecomplete.text = "" + Stagetext;
        yield return new WaitForSeconds(delay);
        ltext.SetActive(true);
        // Stagecomplete.DOFade (0f, 5f);
        yield return new WaitForSeconds(2f);
        ltext.SetActive(false);
        // StartCoroutine (FadeTextToFullAlpha (1f, Stagecomplete.text.text));
    }
    async public void onclickright()
    {
        //change
        // timer.Restart();
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // await FirebaseDatabase.DefaultInstance
        //     .GetReference("users/" + username + "/" + game + "/s" + session + "/q" + (indicator + 2 + qh))
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
        if (DOTween.TotalPlayingTweens() == 0)
        {
            if (scorevar == 40)
            {
                SettingsPenal.gameover();
                Gameover.SetActive(true);
                Invoke("endanimation", 6.5f);
                return;
            }
            if (scorevar2 == 10)
            {
                levelcomanim.SetActive(true);
                SettingsPenal.playanimation();
                Invoke("endanim", 3f);
                Invoke("ddown", 3.3f);
                return;

            }
            if (indicator == 4)
            {
                if (scorevar_2 == 10 && scorevar_1 < 20)
                {
                    down_1.SetActive(true);
                    GameObject thePlayer1 = GameObject.Find("LevelManager2");
                    game6l3 playerScript1 = thePlayer1.GetComponent<game6l3>();
                    playerScript1.scorevar = scorevar;
                    playerScript1.scorevar_2 = scorevar2;
                    playerScript1.scorevar_1 = scorevar_2;
                    playerScript1.restartStopwatch();
                    playerScript1.flag++;
                    self.SetActive(false);
                    return;
                }
                else if (scorevar_2 == 10 && scorevar_1 == 20)
                {
                    StartCoroutine(ShowMessage1(0.5f));
                    return;
                }
                down.SetActive(true);
                GameObject thePlayer = GameObject.Find("LevelManager1");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                playerScript.scorevar = scorevar;
                playerScript.scorevar_1 = scorevar2;
                playerScript.restartStopwatch();
                playerScript.flag++;
                self.SetActive(false);
                return;
            }
            anf = 90;
            a = -1;
            animation();
            indicator++;
            // Debug.Log(indicator);

            Invoke("render", 0.6f);
            Invoke("waiting", 0.6f);

            // render();

        }

    }

    public void waiting()
    {
        right.SetActive(record[indicator]);
    }
    public void animation()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            // cards[i].transform.DORotate(new Vector3(0, cards[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);

            objects[i].transform.DORotate(new Vector3(0, objects[i].transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);
        }
        right.transform.DORotate(new Vector3(0, right.transform.eulerAngles.y + a * 90, 0), 0.5f, RotateMode.Fast);

    }
    public void leftIcon()
    {
        solve(indicator == 0 || indicator == 2 || indicator == 4, "yes");

    }

    public void rightIcon()
    {
        solve(indicator == 1 || indicator == 3, "no");

    }
    //change
    IEnumerator updateAnalytics(String user, String game, int session, int questionNo, string reposnseTime, string status, string question, string option, string answer)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", user);
        form.AddField("game", game);
        form.AddField("session", session);
        form.AddField("questionNo", questionNo);
        form.AddField("reposnseTime", reposnseTime);
        form.AddField("status", status);
        form.AddField("question", question);
        form.AddField("option", option);
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
    async void saveAttempt(string reposnseTime, string status, string question, string option, string answer)
    {

        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tinyhands-fyp.firebaseio.com/");
        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // AtemptEmotion atempt = new AtemptEmotion(reposnseTime, status, question, option, answer);
        // string json = JsonUtility.ToJson(atempt);

        // reference.Child("users").Child(username).Child(game).Child("s" + session).Child("q" + (indicator + 1 + qh)).Child("a" + attemptNo).SetRawJsonValueAsync(json);
        // attemptNo++;

        int questionNo = indicator + 1 + qh;
        StartCoroutine(updateAnalytics(username, game, session, questionNo, reposnseTime, status, question, option, answer));


        if (status == "right")
        {
            StartCoroutine(updateScore(username, game, "3", scorevar, scorevar2, scorevar_1, scorevar_2, solved));

        }
    }
    /////
    void solve(bool answer, string ans)
    {
        //change
        reposnseTime = timer.Elapsed.TotalSeconds.ToString("0.##");
        /////

        if (record[indicator])
        {
            return;
        }
        wrong.SetActive(!answer);
        right.SetActive(answer);
        if (answer)
        {
            scorevar = scorevar + 2;
            SettingsPenal.playeffect(0);
            scorevar2 = scorevar2 + 2;
        }

        record[indicator] = answer;
        string status = answer ? "right" : "wrong";
        if (answer)
        {
            if (solved.Length > 0)
            {
                solved = solved + "," + indicator.ToString();
            }
            else
            {
                solved = indicator.ToString();
            }
        }
        saveAttempt(reposnseTime, status, title[indicator], options[indicator], ans);
        // SettingsPenal.playeffect (1);
        StartCoroutine(Example(answer));
    }
    public void restartTimer()
    {
        timer.Restart();
    }
    IEnumerator Example(bool flag)
    {

        // yield return new WaitForSeconds (0.5f);
        if (flag)
        {
            yield return new WaitForSeconds(0.5f);
            timer.Restart();
            onclickright();
        }
        else

        {
            SettingsPenal.playeffect(1);

            // Debug.Log("ïn else");
            yield return new WaitForSeconds(0.5f);
            timer.Restart();
            wrong.SetActive(false);
        }

    }

    void render()
    {
        for (int i = 0; i < 5; i++)
        {
            objects[i].SetActive(false);
            objects[indicator].SetActive(true);
            if (indicator >= 0)
            {
                objects[i].transform.eulerAngles = new Vector3(objects[i].transform.eulerAngles.x, anf, objects[i].transform.eulerAngles.z);
                right.transform.eulerAngles = new Vector3(right.transform.eulerAngles.x, anf, right.transform.eulerAngles.z);

            }
        }
        if (indicator >= 0)
        {
            animation();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (flag == 1)
        {
            StartCoroutine(ShowMessage(0.5f));
            flag--;
        }
        if (datacheck == 3)
        {
            resumePop.SetActive(false);
            waiting();
            jump();
            datacheck++;

        }
        fortitle.text = "" + title[indicator];
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

//change
class AtemptEmotion
{
    public string reposnseTime;
    public string status;
    public string question;
    public string option;
    public string answer;
    public string timeStamp;

    public AtemptEmotion() { }

    public AtemptEmotion(string reposnseTime, string status, string question, string option, string answer)
    {
        this.reposnseTime = reposnseTime;
        this.status = status;
        this.question = question;
        this.option = option;
        this.answer = answer;
        this.timeStamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

    }
    // public AtemptEmotion(string reposnseTime, string status)
    // {
    //     this.reposnseTime = reposnseTime;
    //     this.status = status;
    // }
}
/////