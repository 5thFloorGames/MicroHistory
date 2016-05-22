using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageCreation : MonoBehaviour {

	private List<Vector3> newVillages = new List<Vector3>();
	private List<MoveAround> founders = new List<MoveAround>();
	private GameObject village;
	private GameObject church;
	private GameObject tower;
	public float timeScale = 1f;
	public int maxAge = 40;
	private int churchCount = 0;
	private int towerCount = 0;
	private int maxChurches = 3;
	private int maxTowers = 1;
	private List<string> nameList;
	private List<string> surnameList;
	private List<string> villageList;
	public int year = 1;
	
	void Awake(){
		church = Resources.Load<GameObject> ("Church");
		village = Resources.Load<GameObject> ("Village");
		tower = Resources.Load<GameObject> ("Tower");
		TextAsset textFile = Resources.Load<TextAsset>("etunimet"); //C#
		nameList = new List<string> ();
		nameList.AddRange (textFile.text.Split ('\n'));
		textFile = Resources.Load<TextAsset>("sukunimet"); //C#
		surnameList = new List<string>();
		surnameList.AddRange (textFile.text.Split ('\n'));
		textFile = Resources.Load<TextAsset>("kunnat"); //C#
		villageList = new List<string>();
		villageList.AddRange (textFile.text.Split ('\n'));
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (UpdateWorld());
	}

	private IEnumerator UpdateWorld(){
		while (true) {
			yield return new WaitForSeconds(1f);
			maxAge += Random.Range(-2,3);
			year++;
			Time.timeScale = timeScale;
			if (newVillages.Count > 0) {
				GameObject newVillage = (GameObject)Instantiate (village, newVillages [0], Quaternion.identity);
				string newVillageName = randomVillageName();
				newVillage.GetComponent<SpawnPeasants>().setName(newVillageName);
				founders[0].setFounder(newVillageName);
				newVillages.Clear ();
				founders.Clear();
			}
		}
	}

	public void FoundVillage(Vector3 location, MoveAround founder){
		newVillages.Add (new Vector3 (location.x,0.6f, location.z));
		founders.Add (founder);
	}

	public int currentMaxAge(){
		return maxAge;
	}

	public void UpgradeVillageToTower(Vector3 position, GameObject village){
		if (towerCount < maxTowers) {
			string villageName = village.GetComponent<SpawnPeasants>().name;
			Destroy(village);
			GameObject towerObject = (GameObject) Instantiate (tower, position, Quaternion.identity);
			towerObject.GetComponent<SpawnPeasants>().setName(villageName);
			towerCount++;
		}
	}

	public void UpgradeVillageToChurch(Vector3 position, GameObject village){
		if (churchCount < maxChurches) {
			string villageName = village.GetComponent<SpawnPeasants>().name;
			Destroy(village);
			GameObject churchObject = (GameObject) Instantiate (church, position, Quaternion.identity);
			churchObject.GetComponent<SpawnPeasants>().setName(villageName);
			churchCount++;
		}
	}

	public string randomPersonName(){
		return nameList [Random.Range (0, nameList.Count)] + " " + surnameList [Random.Range (0, surnameList.Count)]; 
	}

	public string randomVillageName(){
		return villageList [Random.Range(0, villageList.Count)];
	}
}
