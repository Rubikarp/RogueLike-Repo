using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartRun : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}	
}
