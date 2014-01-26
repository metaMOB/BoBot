using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.L)){
			
			this.rigidbody.MovePosition ( this.rigidbody.transform.position + Vector3.right * ((1 * Time.deltaTime) / 1f) * 2.5f);
		}
		
		if (Input.GetKeyDown(KeyCode.K)){
			this.rigidbody.AddForce(-Vector3.right);	
		}
	}
}
