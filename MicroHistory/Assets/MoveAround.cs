using UnityEngine;
using System.Collections;

public class MoveAround : MonoBehaviour {

	private NavMeshAgent agent;
	private GameObject village;
	private bool founder = false;
	private int layerMask = 0;

	void Awake(){
		village = Resources.Load<GameObject> ("Village");
	}

	// Use this for initialization
	void Start () {
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
			agent.SetDestination(new Vector3 (Random.Range(-4.5f, 4.5f), 0.55f, Random.Range(-4.5f, 4.5f)));
			yield return new WaitForSeconds(3f);
		}
	}

	public void OnCollisionEnter(Collision col){
		if (col.collider.tag == "Peasant" && !VillageNearby () && !founder) {
			Instantiate (village, new Vector3 (transform.position.x, 0.6f, transform.position.z), Quaternion.identity);
			founder = true;
			agent.Stop ();
		} else {
			print ("No village");
		}
	}
	
	private bool VillageNearby(){
		return Physics.OverlapSphere (transform.position, 1f, layerMask).Length > 0;
	}
}
