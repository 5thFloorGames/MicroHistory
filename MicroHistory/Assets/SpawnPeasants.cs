using UnityEngine;
using System.Collections;

public class SpawnPeasants : MonoBehaviour {

	private GameObject peasant;

	// Use this for initialization
	void Start () {
		peasant = Resources.Load<GameObject>("Peasant");
		StartCoroutine (Spawn ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Spawn(){
		while (true) {
			yield return new WaitForSeconds(10f);
			Instantiate (peasant, transform.position, Quaternion.identity);
		}
	}
}
