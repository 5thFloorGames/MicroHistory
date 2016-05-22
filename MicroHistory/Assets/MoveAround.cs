using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveAround : MonoBehaviour {

	private NavMeshAgent agent;
	private bool founder = false;
	private int layerMask = 0;
	private VillageCreation creator;
	private int maxAge;
	private int age = 0;
	public Job job;
	private Dictionary<Job, Color> jobToColor = new Dictionary<Job, Color>();
	private string name;
	private List<string> history = new List<string>();

	void Awake(){
		creator = GameObject.FindGameObjectWithTag("GameController").GetComponent<VillageCreation>();
		job = randomJob ();

	}

	// Use this for initialization
	void Start () {
		name = creator.randomPersonName ();
		history.Add ("Born as " + name + " in " + creator.year);
		maxAge = Random.Range (1, creator.currentMaxAge()) + Random.Range(-5, 6);
		history.Add ("Became a " + job + " in " + (creator.year + Random.Range (0, maxAge/2)));
		jobToColor.Add (Job.Mason, Color.red);
		jobToColor.Add (Job.Peasant, Color.cyan);
		jobToColor.Add (Job.Carpenter, Color.yellow);
		jobToColor.Add (Job.Farmer, Color.blue);
		jobToColor.Add (Job.Noble, Color.magenta);
		jobToColor.Add (Job.Priest, Color.white);

		GetComponentInChildren<Renderer> ().material.color = jobToColor [job];

		agent = GetComponent<NavMeshAgent> ();
		layerMask |= 1 << LayerMask.NameToLayer ("Village");
		agent.SetDestination(new Vector3 (Random.Range(-4.5f, 4.5f), 0.55f, Random.Range(-4.5f, 4.5f)));
		StartCoroutine(NewDestination());
	}

	public void setHometown(string name){
		history.Add ("Started living in " + name);
	}

	public void setFounder(string name){
		history.Add ("Founded " + name + " in " + creator.year);
	}

	
	// Update is called once per frame
	void Update () {

	}

	public IEnumerator NewDestination(){
		while (true) {
			age += 3; 
			if(age > maxAge){
				break;
			}
			agent.SetDestination(new Vector3 (Random.Range(-4.5f, 4.5f), 0.55f, Random.Range(-4.5f, 4.5f)));
			yield return new WaitForSeconds(3f);
		}
		history.Add ("Died in " + creator.year);
		PrintHistory ();
		Destroy (gameObject);
	}

	public void PrintHistory(){
		string writtenHistory = "";
		foreach (string piece in history) {
			writtenHistory += piece + '\n';
		}
		print (writtenHistory);
	}

	public void OnCollisionEnter(Collision col){
		if (col.collider.tag == "Peasant" && !VillageNearby () && !founder) {
			creator.FoundVillage(col.transform.position, this);
			founder = true;
		} else {
			//print ("No village");
		}
	}
	
	private bool VillageNearby(){
		return Physics.OverlapSphere (transform.position, 0.5f + Random.Range(0f,1f), layerMask).Length > 0;
	}

	private Job randomJob(){
		int jobNumber = Random.Range (0, 100);
		if (jobNumber < 50) {
			return Job.Peasant;
		} else if (jobNumber < 65){
			return Job.Farmer;
		} else if (jobNumber < 80){
			return Job.Carpenter;
		} else if (jobNumber < 90){
			return Job.Priest;
		} else if (jobNumber > 98){
			return Job.Noble;
		} else {
			return Job.Mason;
		}
	}
}
