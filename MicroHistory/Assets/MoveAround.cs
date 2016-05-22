using UnityEngine;
using System.Collections;

public class MoveAround : MonoBehaviour {

	private NavMeshAgent agent;
	private bool founder = false;
	private int layerMask = 0;
	private VillageCreation creator;
	private int maxAge;
	private int age = 0;

	void Awake(){
		creator = GameObject.FindGameObjectWithTag("GameController").GetComponent<VillageCreation>();
	}

	// Use this for initialization
	void Start () {
		maxAge = Random.Range (1, creator.currentMaxAge());
		agent = GetComponent<NavMeshAgent> ();
		layerMask |= 1 << LayerMask.NameToLayer ("Village");
		agent.SetDestination(new Vector3 (Random.Range(-4.5f, 4.5f), 0.55f, Random.Range(-4.5f, 4.5f)));
		StartCoroutine(NewDestination());
	}

	// Update is called once per frame
	void Update () {

	}

	public IEnumerator NewDestination(){
		while (true) {
			age++; 
			if(age > maxAge){
				break;
			}
			agent.SetDestination(new Vector3 (Random.Range(-4.5f, 4.5f), 0.55f, Random.Range(-4.5f, 4.5f)));
			yield return new WaitForSeconds(3f);
		}
		print ("Dying");
		Destroy (gameObject);
	}

	public void OnCollisionEnter(Collision col){
		if (col.collider.tag == "Peasant" && !VillageNearby () && !founder) {
			creator.FoundVillage(col.transform.position);
			founder = true;
		} else {
			//print ("No village");
		}
	}
	
	private bool VillageNearby(){
		return Physics.OverlapSphere (transform.position, 2f, layerMask).Length > 0;
	}
}
