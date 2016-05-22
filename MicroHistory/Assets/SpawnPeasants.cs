using UnityEngine;
using System.Collections;

public class SpawnPeasants : MonoBehaviour {

	private GameObject peasant;
	private VillageCreation creator;

	void Awake(){
		creator = GameObject.FindGameObjectWithTag("GameController").GetComponent<VillageCreation>();
	}

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
			GameObject newPeasant = (GameObject)Instantiate (peasant, transform.position, Quaternion.identity);
			if(newPeasant.GetComponent<MoveAround>().job == Job.Noble){
				creator.UpgradeVillageToTower(transform.position, this.gameObject);
			} else if(newPeasant.GetComponent<MoveAround>().job == Job.Priest){
				creator.UpgradeVillageToChurch(transform.position, this.gameObject);
			}
		}
	}
}
