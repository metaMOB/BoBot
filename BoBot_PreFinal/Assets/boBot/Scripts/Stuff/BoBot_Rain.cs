using UnityEngine;
using System.Collections;

public class BoBot_Rain : MonoBehaviour {

	// Use this for initialization
	private AudioSource snd;
	public AudioClip rainSound;
	public float intesity;
	
	public float maxVolume = 0.5f;
	public float maxParticles = 500f;
	
	public float grassSpeed = 1;
	
	private float actIntensity;
	private ParticleSystem rain;
	private float intensityDelta = 0.5f;
	
	private float deltaTime;
	private e2dTerrain terrain;
	
	void Start () {
		snd = gameObject.AddComponent<AudioSource>();
		rain = gameObject.GetComponent<ParticleSystem>();
		snd.clip = rainSound;
		snd.volume = intesity;
		snd.loop = true;
		//snd.Play();
		intesity = 0;
		deltaTime = 0.5f;
		actIntensity = intesity;
		
		snd.volume = intensityDelta * maxVolume;
		rain.emissionRate = intensityDelta * maxParticles;
		terrain = GameObject.Find("Level").GetComponent<e2dTerrain>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("rain "+intesity+ "   "+intensityDelta+"   "+actIntensity);
		if (actIntensity > 0){
			if (!snd.isPlaying){
				snd.Play();
			}
			
			if (!rain.isPlaying){
				rain.Play();	
			}
		}
		
		//if (intensityDelta != intesity){
		if ( Mathf.Abs(actIntensity) > 0.01){
			intensityDelta = Mathf.SmoothDamp( intensityDelta, intesity, ref actIntensity, deltaTime);
			snd.volume = intensityDelta * maxVolume;
			rain.emissionRate = intensityDelta * maxParticles;
		}
		
		if (actIntensity == 0){
			if (snd.isPlaying){
				snd.Stop();
			}
			
			if (rain.isPlaying){
				rain.Stop();	
			}
		}
	}
	
	public void setIntensity (float newIntensity, float newDeltaTime){
		Debug.Log ("new rain "+newIntensity);
		this.intesity = newIntensity;
		this.deltaTime = newDeltaTime;
	}
}
