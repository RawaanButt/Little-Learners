using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class startButton : MonoBehaviour
{


    /*	public GameObject loadingScreen;
        public Slider slider;
        public Text progressText;


        public void Start_Button (int sceneIndex)
        {

            StartCoroutine(LoadAsynchronously(sceneIndex));
        }


        IEnumerator LoadAsynchronously (int sceneIndex)
        {
            AsyncOperation operation =	SceneManager.LoadSceneAsync (sceneIndex);

            loadingScreen.SetActive (true);

            while (!operation.isDone) {


                float progress = Mathf.Clamp01 (operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";

                yield return null ;

            }



    }
    }
     */


    [SerializeField]
    private float delayBeforeLoaing = 10f;
    [SerializeField]

    private string sceneNameToLoad;

    private float timeElapsed;

    private void Update()
    {

        timeElapsed += Time.deltaTime;
        if (timeElapsed > delayBeforeLoaing)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
