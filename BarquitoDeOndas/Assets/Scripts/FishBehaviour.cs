using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FishBehaviour : MonoBehaviour {

	public GameObject[] emitters;

	public GameObject[] peces;

	private float speed;
	private int selected, selectedEmitter;

	void Start() {
		speed = Random.Range (50.0f,100.0f);
		StartCoroutine (FishGenerator());
		selected = Random.Range (0,5);
		selectedEmitter = Random.Range (0,5);
	} 

	private IEnumerator FishGenerator() {
		while (true) {
			yield return new WaitForSeconds (2.5f);

			GameObject Temporary_Bullet_Handler;

			Temporary_Bullet_Handler = Instantiate (peces[selected], emitters[selectedEmitter].transform.position, emitters[selectedEmitter].transform.rotation) as GameObject;
			Rigidbody temporary_Rigidbody01 = Temporary_Bullet_Handler.GetComponent<Rigidbody> ();
			temporary_Rigidbody01.AddForce (emitters[selectedEmitter].transform.forward * speed);
			Destroy (Temporary_Bullet_Handler, 30.0f);

			speed = Random.Range (50.0f,100.0f);
			selected = Random.Range (0,5);
			selectedEmitter = Random.Range (0,5);
		}
	}
}