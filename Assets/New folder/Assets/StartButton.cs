using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartButton : MonoBehaviour {

	public GameObject set;
	public GameObject set1;

	void Start () {
		set1.SetActive (false);
		set.SetActive (false);
	}

	//public GameObject loadingScreen;
	//public Slider slider;
	//public Text progressText;

	//public void Start_Button (int sceneIndex)
	//{

	//	StartCoroutine(LoadAsynchronously(sceneIndex));
	//}

	//IEnumerator LoadAsynchronously (int sceneIndex)
	//{
	//	AsyncOperation operation =	SceneManager.LoadSceneAsync (sceneIndex);
	//
	//	loadingScreen.SetActive (true);
	//
	//	while (!operation.isDone) {

	//		float progress = Mathf.Clamp01 (operation.progress / .9f);

	//		slider.value = progress;
	//		progressText.text = progress * 100f + "%";

	//		yield return null ;

	//	}

	//}

	[SerializeField]
	private float delayBeforeLoaing = 10f;
	[SerializeField]

	private string sceneNameToLoad;

	private float timeElapsed;

	private void Update () {

		timeElapsed += Time.deltaTime;
		if (timeElapsed > delayBeforeLoaing) {
			set1.SetActive (true);
			set.SetActive (true);
			SceneManager.LoadScene (sceneNameToLoad);
		}

	}
}