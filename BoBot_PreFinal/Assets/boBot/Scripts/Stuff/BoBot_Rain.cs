using UnityEngine;
using System.Collections;

public class BoBot_Rain : MonoBehaviour {

	// Use this for initialization
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
		//snd.Play();
		actIntensity = intesity;
		
		snd.volume = actIntensity * maxVolume;
		rain.emissionRate = actIntensity * maxParticles;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("rain "+intesity+ "   "+intensityDelta+"   "+actIntensity);
		
		
		//if (intensityDelta != intesity){
		
		intensityDelta = Mathf.SmoothDamp( intensityDelta, intesity, ref actIntensity, deltaTime);
		snd.volume = intensityDelta * maxVolume;
		rain.emissionRate = intensityDelta * maxParticles;
	
		
		if ( Mathf.Abs(intensityDelta) < 0.01){
			if (snd.isPlaying){
				snd.Stop();
			}
			
			if (rain.isPlaying){
				rain.Stop();	
			}
		} else {
			if (!snd.isPlaying){
				snd.Play();
			}
			
			if (!rain.isPlaying){
				rain.Play();	
			}
		}
	}
	
	public void setIntensity (float newIntensity, float newDeltaTime){
		Debug.Log ("new rain "+newIntensity);
		intesity = newIntensity;
		deltaTime = newDeltaTime;
	}
}
