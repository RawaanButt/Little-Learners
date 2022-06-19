using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPenal : MonoBehaviour {
    public GameObject sound;
    public GameObject effects;
    public GameObject gamelabel;
    public GameObject levelcomplete;
    public GameObject one, four, five, six;
    public GameObject two;
    public GameObject three;
    public GameObject changespeed;
    public GameObject changespeed1;
    public GameObject changespeed2;
    public GameObject dontdes;
    public GameObject panel, panel1;

    public Sprite gamesoundoff;
    public Sprite gamesoundon;

    public Sprite audioOff;
    public Sprite audioOn;
    public Sprite effectoff;
    public Sprite effecton;
    public static bool effectflag;
    public static bool lvlflag;

    public AudioSource audioSource;
    public static AudioSource lvlcomplete;
    public AudioSource lvlcomplete2;
    public static AudioSource congra;
    public AudioSource congra1;

    public AudioSource[] audioeffects;
    public static AudioSource[] audioeffects2;
    public bool hidebar = true;

    public Sprite gamelabeloff;
    public Sprite gamelabelon;

    public int flag;

    public bool pause;

    private static SettingsPenal playerInstance;
    void Awake () {
        audioeffects2 = new AudioSource[2];
        for (int i = 0; i < audioeffects.Length; i++) {
            audioeffects2[i] = audioeffects[i];
            DontDestroyOnLoad (audioeffects[i]);
        }
        lvlcomplete = lvlcomplete2;
        congra = congra1;
        DontDestroyOnLoad (this);
        DontDestroyOnLoad (audioSource);
        DontDestroyOnLoad (lvlcomplete);
        DontDestroyOnLoad (congra);
        DontDestroyOnLoad (dontdes);
        DontDestroyOnLoad (panel);
        DontDestroyOnLoad (panel1);

        if (playerInstance == null) {
            playerInstance = this;
        } else {
            DestroyObject (gameObject);
            DestroyObject (audioSource);
            DestroyObject (lvlcomplete);
            DestroyObject (congra);
            DestroyObject (dontdes);
            DestroyObject (panel);
            DestroyObject (panel1);

            for (int i = 0; i < audioeffects.Length; i++) {
                DestroyObject (audioeffects[i]);
            }

        }
    }

    public static void playeffect (int i) {
        if (!effectflag) {
            audioeffects2[i].Play ();

        }
    }
    public static void playanimation () {
        if (!lvlflag) {
            lvlcomplete.Play ();

        }
    }
    public static void gameover () {
        if (!lvlflag) {
            congra.Play ();

        }
    }

    public void topbarhide () {
        if (hidebar) {
            gamelabel.GetComponent<Image> ().sprite = gamelabelon;

        } else {
            gamelabel.GetComponent<Image> ().sprite = gamelabeloff;

        }
        hidebar = !hidebar;
    }

    // void Awake()
    // {
    //     DontDestroyOnLoad(transform.gameObject);
    //     DontDestroyOnLoad(sound);

    // }

    // Start is called before the first frame update

    void start () {
        // Screen.SetResolution (1280, 800);

        pause = true;
        if (AudioListener.pause == true) {
            sound.GetComponent<Image> ().sprite = audioOff;

        } else {
            sound.GetComponent<Image> ().sprite = audioOn;
        }

    }

    public void mute () {

        if (pause) {
            // AudioListener.pause = false;
            audioSource.Play ();

            sound.GetComponent<Image> ().sprite = audioOn;
        } else {
            // AudioListener.pause = true;
            audioSource.Pause ();
            sound.GetComponent<Image> ().sprite = audioOff;
        }
        pause = !pause;
    }

    public void soundeffect () {
        if (effectflag) {
            effects.GetComponent<Image> ().sprite = effecton;

        } else {
            effects.GetComponent<Image> ().sprite = effectoff;

        }
        effectflag = !effectflag;
    }
    public float newspeed1;
    public void changespeedfeature (float newspeed) {
        try {
            // Debug.Log ("Callesd");

            newspeed1 = newspeed;

        } catch {
            Debug.Log ("Nothing found");
        }

    }
    public void lvlanimation () {
        if (lvlflag) {
            levelcomplete.GetComponent<Image> ().sprite = gamesoundon;

        } else {
            levelcomplete.GetComponent<Image> ().sprite = gamesoundoff;

        }
        lvlflag = !lvlflag;
    }

    // Update is called once per frame
    void Update () {

        if (hidebar) {

            try {
                if (GameObject.FindWithTag ("bar")) {
                    if (one != null) {
                        one.SetActive (true);
                        two.SetActive (true);
                        three.SetActive (true);
                    }
                    one = GameObject.FindWithTag ("bar");
                    one.SetActive (false);
                    two = GameObject.FindWithTag ("scoreboard");
                    two.SetActive (false);
                    three = GameObject.FindWithTag ("topbar");
                    three.SetActive (false);
                }

            } catch {
                Debug.Log ("NOt found1111");
            }
        } else {

            try {
                // if (GameObject.FindWithTag ("bar")) {
                //     one = GameObject.FindWithTag ("bar");
                //     two = GameObject.FindWithTag ("scoreboard");
                //     three = GameObject.FindWithTag ("topbar");
                one.SetActive (true);
                two.SetActive (true);
                three.SetActive (true);
                // }

            } catch {
                // Debug.Log ("NOt found");
            }

        }

        try {
            // Debug.Log ("Callesd");
            changespeed = GameObject.FindWithTag ("floating");
            changespeed1 = GameObject.FindWithTag ("floating1");
            Float playerScript1 = changespeed1.GetComponent<Float> ();
            Float playerScript = changespeed.GetComponent<Float> ();
            playerScript.speed = newspeed1;
            playerScript1.speed = newspeed1;
            Debug.Log ("worked");

        } catch {
            // Debug.Log ("Nothing found");
        }

    }
}