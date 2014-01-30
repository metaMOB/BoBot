using UnityEngine;
using System.Collections;

public class BoBot_Rain : MonoBehaviour {

	private AudioSource snd;
	public AudioClip rainSound;
	public float intesity;
	
	public float maxVolume = 0.5f;
	public float maxParticles = 500f;
	
	private float actIntensity;
	private ParticleSystem rain;
	public float intensityDelta;
	
	public float deltaTime;
	
	void Start () {
		snd = gameObject.AddComponent<AudioSource>();
		rain = gameObject.GetComponent<ParticleSystem>();
		snd.clip = rainSound;
		snd.volume = intesity;
		snd.loop = true;
		actIntensity = intesity;
		
		snd.volume = actIntensity * maxVolume;
		rain.emissionRate = actIntensity * maxParticles;
	}
	
	void Update () {		
		intensityDelta = Mathf.SmoothDamp( intensityDelta, intesity, ref actIntensity, deltaTime);
		snd.volume = intensityDelta * maxVolume;
		rain.emissionRate = intensityDelta * maxParticles;
	}
	
	public void setIntensity (float newIntensity, float newDeltaTime){
		intesity = newIntensity;
		deltaTime = newDeltaTime;
		
		try {
			snd.Play();
			rain.Play();
		} catch {}
	}
}
