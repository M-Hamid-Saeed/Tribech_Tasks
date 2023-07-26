using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{

	[SerializeField] string sceneName;

	public void LoadScene()
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			return;
		}

		SceneManager.LoadSceneAsync(sceneName);
	}

	public void ReloadScene()
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			sceneName = SceneManager.GetActiveScene().name;
		}

		SceneManager.LoadSceneAsync(sceneName);
	}

}