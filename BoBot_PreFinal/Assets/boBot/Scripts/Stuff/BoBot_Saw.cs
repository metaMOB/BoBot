using UnityEngine;
using System.Collections;

public class BoBot_Saw : MonoBehaviour {

	public ParticleSystem [] particles;
	public AudioClip sawingSound;
	public float volume = 0.5f;
	
	private BoBot_BasicPhysicsComponent phys;
	private Vector3 partPos = Vector3.zero;
	private AudioSource snd;
	private float intensity = 0;
	
	private float delta;
	
	void Start () {
		phys = gameObject.GetComponent<BoBot_BasicPhysicsComponent>();
		snd = gameObject.AddComponent<AudioSource>();
		snd.clip = sawingSound;
		/*snd.minDistance = 1f;
		snd.maxDistance = 10f;*/
		snd.volume = 0f;
		snd.Play();
		foreach (ParticleSystem particle in particles){
			particle.Stop();
		}
	}	
	
	void Update(){
		snd.volume = Mathf.SmoothDamp(snd.volume, intensity, ref delta, 0.5f);
	}
	
	void FixedUpdate () {
		if (partPos != Vector3.zero){
			intensity = 1;
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
			intensity = 0;
			foreach (ParticleSystem particle in particles){
				particle.Stop();
			}			
		}
		
		partPos = Vector3.zero;
	}
	
	void OnTriggerStay(Collider other){
		if (!other.name.Equals("_collider")){
			partPos = (other.ClosestPointOnBounds (this.transform.position));
		}
	}
}
