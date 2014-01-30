using UnityEngine;
using System.Collections;

public class BoBot_BridgePole : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.rigidbody.MovePosition(this.rigidbody.transform.position + Vector3.right * 2.2f);
	}
}
