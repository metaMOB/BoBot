using UnityEngine;
using System.Collections;

public class BoBot_DestroyComponent : BoBot_OnOffComponent {
	
	private bool done = false;
	
	public bool justTheJoint = false;
		
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
