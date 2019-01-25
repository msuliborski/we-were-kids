using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menuScript : MonoBehaviour
{
	[SerializeField] private AudioClip SatanLaugh;
	[SerializeField] private AudioClip Stab;
	
	[SerializeField] private AudioSource source;

	IEnumerator Sleep(float time)
	{
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene("newMain");
		Debug.Log("game_temp");
	}

	private void Start()
	{
		source = gameObject.GetComponent<AudioSource>();
	}

	public void PlayStab()
	{
		source.clip = Stab;
		source.PlayOneShot(Stab);
	}

	public void PlayGame()
	{
		source.clip = SatanLaugh;
		source.PlayOneShot(SatanLaugh);

		StartCoroutine(Sleep(6));
	}

	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}
}