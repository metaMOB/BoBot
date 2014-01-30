using UnityEngine;
using System.Collections;

public class MoveKiste : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
  		this.rigidbody.AddForce(new Vector3 ( (Random.value * 2 -1), 0f, 0f));

	}
}
