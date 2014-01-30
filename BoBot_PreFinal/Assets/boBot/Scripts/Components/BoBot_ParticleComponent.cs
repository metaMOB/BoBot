using UnityEngine;
using System.Collections;

public class BoBot_ParticleComponent : BoBot_OnOffComponent {
	
	private ParticleSystem part;
	private float target = 0;
	
	private float intensity;
	private float delta;
	private float timer = 0;
	
	private float maxEmits;
	
	void Start () {
		part = this.gameObject.GetComponent<ParticleSystem>();
		maxEmits = part.emissionRate;
	}
	
	void Update () {
		if (state){
			target = 1;
			timer = fadeInOutTime;
		} 
		
		if (timer >0){
			part.Play();
			intensity = Mathf.SmoothDamp(intensity, target, ref delta, fadeInOutTime);	
			part.emissionRate = maxEmits * intensity;
			timer -= Time.deltaTime;
		}
	}
}
