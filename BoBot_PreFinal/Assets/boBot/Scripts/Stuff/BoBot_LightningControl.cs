using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoBot_LightningControl : MonoBehaviour {

	public BoBot_LightningSky [] skies;
	public AudioClip [] lightningSounds;
	public AudioClip [] growlSounds;
	
	private  List<AudioSource> lightningSoundSources = new List<AudioSource>();
	
	public float timer = 0f;
	
	public float timeBetween = 15f;
	public float speed = 1f;
	public float varianz = 0f;
	
	public float growlVolume = 1f;
	public float thunderVolume = 0.7f;
	
	private float lightning = 0f;
	
	private int actLayer = -1;
	
	private float rndVarianz;
	private float workSpeed = 0;
	
	
	private int numOfFlashes = 1;
	private int flashNum = 0;
	
	private int minFlashes = 1;
	private int maxFlashes = 1;
	
	private int oldFlashNumber = 0;
	private int oldGrowlNumber = 0;
	
	public bool noSound = false;
	
	// Use this for initialization
	void Awake () {
		lightningSoundSources.Add (gameObject.AddComponent<AudioSource>());
		//skies = gameObject.GetComponentsInChildren<BoBot_LightningSky>();
		rndVarianz = Random.value * varianz - varianz/2;
		workSpeed = speed;
		numOfFlashes = Mathf.CeilToInt ( Random.value * 3) + 1;		
	}
	
	// Update is called once per frame
	void Update () {
		if (timeBetween > 0){
			/*if (timer == 0f){
				Debug.Log ("sound!!");
				int number = Mathf.FloorToInt ( Random.value * lightningSounds.Length);				
				bool running = false;
				foreach (AudioSource source in lightningSoundSources){
					if (!source.isPlaying){
						source.clip = lightningSounds[number];
						source.loop = false;
						source.Play();
						running = true;
						break;
					}
				}
				
				if (!running){
					 	AudioSource src = gameObject.AddComponent<AudioSource>();
						src.clip = lightningSounds[number];
						src.loop = false;
						src.Play();
						lightningSoundSources.Add (src);
				}
				
				
				
			}*/
			
			timer += Time.deltaTime;
			lightning += Time.deltaTime;
			
			if (lightning > workSpeed){
				//if (actLayer < skies.Length){
				
				if (actLayer >= 0 && flashNum < numOfFlashes){
					skies[actLayer].setLightning (1f);	
				}
				
					if ( actLayer >= 1 && flashNum < numOfFlashes){		
						int number;
						do {
							number = Mathf.FloorToInt ( Random.value * lightningSounds.Length);
						} while (number == oldFlashNumber); 
						oldFlashNumber = number;						
						if (!noSound){
							setSound (lightningSounds[number], thunderVolume);
						}
					
						workSpeed = speed + Mathf.FloorToInt ( Random.value * (speed*varianz) * 2) - (speed*varianz) ;
						flashNum++;
					}
					
					if ( actLayer == -1){		
						int number;
						do {
							number = Mathf.FloorToInt ( Random.value * growlSounds.Length);
						} while (number == oldGrowlNumber); 
						oldGrowlNumber = number;	
					
					//	setSound (lightningSounds[number]);
					
						
						setSound (growlSounds[number], growlVolume);
					}
					
				
					actLayer = Mathf.Clamp( actLayer+1, 0, skies.Length-1);	
					lightning = 0f;
			//	} 					
			}
			
			if (timer > timeBetween + rndVarianz){
				Debug.Log ("flash");
				timer = 0f;
				lightning = 0f;
				actLayer = -1;
				workSpeed = speed;				
				numOfFlashes = Mathf.CeilToInt ( Random.value * maxFlashes) +minFlashes;	
				flashNum = 0;
			}
			
			
		}
	}
	
	private void setSound (AudioClip clip, float vol){
			int number = Mathf.FloorToInt ( Random.value * lightningSounds.Length);				
			bool running = false;
			foreach (AudioSource source in lightningSoundSources){
				if (!source.isPlaying){
					source.clip = clip;
					source.loop = false;
					source.volume = vol;
					source.Play();
					running = true;
					break;
				}
			}
			
			if (!running){
				 	AudioSource src = gameObject.AddComponent<AudioSource>();
					src.clip = clip;
					src.loop = false;
					src.Play();
					lightningSoundSources.Add (src);
			}
		
	}
	
	public void setLightning (float timeBetween, float varianz, bool noSound, int minFlashes, int maxFlashes){
		this.timeBetween = timeBetween;
		this.varianz = varianz;		
		this.noSound = noSound;
		this.minFlashes = minFlashes;
		this.maxFlashes = maxFlashes;
		numOfFlashes = Mathf.CeilToInt ( Random.value * maxFlashes) +minFlashes;	
	}	
}
