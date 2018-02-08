using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

	public Slider slider;
	public GameObject loadingScreen;

	public void LoadLevel() {
		StartCoroutine("load", SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame() {
		Debug.Log("QUIT");
		Application.Quit();
	}

	IEnumerator load(int index) {

		AsyncOperation operation = SceneManager.LoadSceneAsync(index);

		loadingScreen.SetActive(true);

		while (!operation.isDone) {

			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			slider.value = progress;
			yield return null;
		}
	}


	// public void PlayGame() {
	// 	SceneManager.LoadScene();
	// }

	

	
}
