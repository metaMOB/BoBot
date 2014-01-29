using UnityEngine;
using System.Collections;

public class BoBot_AmbienceControl : MonoBehaviour {
	
	public AudioClip sound;
	public float volume = 0.5f;
	
	private AudioSource sndA;
	private AudioSource sndB;
	
	private float volA;
	private float volB;
	
	private float fadeVolume;
	private float currentVelocity;
	private float timeFade = 1f;
	private float target = 1;
	
	// Use this for initialization
	void Start () {
		sndA = gameObject.AddComponent<AudioSource>();
		sndB = gameObject.AddComponent<AudioSource>();
		
		sndA.loop = true;
		sndB.loop = true;
		sndA.volume = 0;
		sndB.volume = 0;
		
		volA = 0;
		volB = 0;
		
		if (sound){
			setNewSound (sound, 2, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		fadeVolume = Mathf.SmoothDamp (fadeVolume, target, ref currentVelocity, timeFade);
		
		if ( Mathf.Abs(currentVelocity) > 0.001){
			sndB.volume = volume * fadeVolume * volB;
			sndA.volume = volume * (1f-fadeVolume) * volA;
		} else {
			if (sndA.isPlaying && sndA.volume < 0.001f){
				sndA.Stop();	
			} else if (sndB.isPlaying && sndB.volume < 0.001f){
				sndB.Stop();	
			}
		}
		
	}
	
	public void setNewSound (AudioClip newSound, float timeToFade, float vol){
		this.timeFade = timeToFade;
		if (fadeVolume < 0.1f ){
			sndB.clip = newSound;
			sndB.Play();
			volB = vol;
			target = 1f;
		} else if (fadeVolume > 0.9f ){
			sndA.clip = newSound;
			sndA.Play();
			volA = vol;
			target = 0;
		}
	}
}
