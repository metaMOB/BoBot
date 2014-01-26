using UnityEngine;
using System.Collections;

public class BoBot_Saw : MonoBehaviour {

	// Use this for initialization
	public ParticleSystem [] particles;
	public AudioClip sawingSound;
	
	private BoBot_BasicPhysicsComponent phys;
	private Vector3 partPos = Vector3.zero;
	
	void Start () {
		phys = gameObject.GetComponent<BoBot_BasicPhysicsComponent>();
		//Physics.IgnoreLayerCollision(0, 12);
		foreach (ParticleSystem particle in particles){
			particle.Stop();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (partPos != Vector3.zero){
			foreach (ParticleSystem particle in particles){
				Transform partTransform = particle.transform;
				if (phys.delta.x <0f){
					partTransform.rotation = new Quaternion(330, 90, 0, 0);
				} else {
					partTransform.rotation = new Quaternion(150, 90, 0, 0);
				}
				partTransform.position = partPos;
				particle.Play();
			}
		} else {
			foreach (ParticleSystem particle in particles){
				particle.Stop();
			}
		}
		
		partPos = Vector3.zero;
	}
	
	void OnTriggerStay(Collider other){
		if (!other.name.Equals("_collider")){
			Debug.Log ("lkjlkjlkjlk "+other.gameObject.name + "  "+gameObject.GetComponent<BoBot_BasicPhysicsComponent>().delta);	
			partPos = (other.ClosestPointOnBounds (this.transform.position));// - this.transform.position);
			
			//foreach (ParticleSystem particle in particles){
				//particle.transform.position = otherDistance;
				//particle.Play();
			//}
		}
	}
}
