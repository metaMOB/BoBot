using UnityEngine;
using System.Collections;

public class BoBot_Rain : MonoBehaviour {

	// Use this for initialization
	private AudioSource snd;
	public AudioClip rainSound;
	public float intesity = 0.5f;
	
	public float maxVolume = 0.5f;
	public float maxParticles = 500f;
	
	private float actIntensity;
	private ParticleSystem rain;
	private float intensityDelta = 0.5f;
	
	void Start () {
		snd = gameObject.AddComponent<AudioSource>();
		rain = gameObject.GetComponent<ParticleSystem>();
		snd.clip = rainSound;
		snd.volume = intesity;
		snd.loop = true;
		snd.Play();
		actIntensity = intesity;
		
		snd.volume = intensityDelta * maxVolume;
		rain.emissionRate = intensityDelta * maxParticles;
	}
	
	// Update is called once per frame
	void Update () {
		if (actIntensity != intesity){
			intensityDelta = Mathf.SmoothDamp( intensityDelta, intesity, ref actIntensity, 0.5f);
			snd.volume = intensityDelta * maxVolume;
			rain.emissionRate = intensityDelta * maxParticles;
		}
	}
}
