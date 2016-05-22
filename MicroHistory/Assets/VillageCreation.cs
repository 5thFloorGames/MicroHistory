using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageCreation : MonoBehaviour {

	private List<Vector3> newVillages = new List<Vector3>();
	private GameObject village;
	public float timeScale = 1f;
	public int maxAge = 40;

	void Awake(){
		village = Resources.Load<GameObject> ("Village");
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (UpdateWorld());
	}

	private IEnumerator UpdateWorld(){
		while (true) {
			yield return new WaitForSeconds(1f);
			maxAge += Random.Range(-2,4);
			Time.timeScale = timeScale;
			if (newVillages.Count > 0) {
				Instantiate (village, newVillages [0], Quaternion.identity);
				newVillages.Clear ();
			}
		}
	}

	public void FoundVillage(Vector3 location){
		newVillages.Add (new Vector3 (location.x,0.6f, location.z));
	}

	public int currentMaxAge(){
		return maxAge;
	}
}
