using UnityEngine;
using System.Collections;

public class BoBot_DestroyComponent : BoBot_OnOffComponent {

	//private GameObject [] objectsToDestroy;
	
	private bool done = false;
	
	public bool justTheJoint = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (state && !done){
			done = true;
			if (!justTheJoint){
				foreach (Transform obj in gameObject.GetComponentsInChildren<Transform>()){
					Destroy(obj.gameObject);	
				}
				Destroy (gameObject);
			} else {
				try{
					Destroy (this.GetComponent<Joint>());	
				}
				catch {
				}				
			}
		}
	}
}
