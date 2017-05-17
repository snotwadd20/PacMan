using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacmanDie : MonoBehaviour 
{
	private PacmanMove pacmanMover = null;
	public bool isDead = false;
	void Awake()
	{
		pacmanMover = FindObjectOfType<PacmanMove>();
	}//Awake

	public void Die()
	{
		if (isDead)
			return;
		
		isDead = true;
		//Debug.Log("PACMAN IS DEED");
		pacmanMover.enabled = false;
		StartCoroutine(DieSequence());
		//gameObject.SetActive(false);
	}//Die

	public void ResetGame()
	{
		//Debug.Log("RESET");

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}//ResetGame

	IEnumerator DieSequence()
	{
		yield return new WaitForSeconds(2.0f);
		ResetGame();
		yield return new WaitForEndOfFrame();
	}//DieSequence
}//PacmanDie
