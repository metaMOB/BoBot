using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoBot_LightningControl : MonoBehaviour {

	public BoBot_LightningSky [] skies;
	public AudioClip [] lightningSounds;
	private  List<AudioSource> lightningSoundSources = new List<AudioSource>();
	
	public float timer = 0f;
	
	public float timeBetween = 15f;
	public float speed = 1f;
	public float varianz = 0f;
	public float lightning = 0f;
	
	private int actLayer = 0;
	
	private float rndVarianz;
	
	// Use this for initialization
	void Awake () {
		lightningSoundSources.Add (gameObject.AddComponent<AudioSource>());
		//skies = gameObject.GetComponentsInChildren<BoBot_LightningSky>();
		rndVarianz = Random.value * varianz - varianz/2;
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
			
			if (lightning > speed){
				if (actLayer < skies.Length){
					if ( actLayer == skies.Length-2 ){						
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
					}
			//		Debug.Log ("act "+actLayer);
					skies[actLayer].setLightning (1f);	
					actLayer++;	
					lightning = 0f;
				} 					
			}
			
			if (timer > timeBetween + rndVarianz){
				Debug.Log ("flash");
				timer = 0f;
				lightning = 0f;
				actLayer = 0;
				
				
				
			}
			
			
		}
	}
	
	public void setLightning (float timeBetween, float varianz){
		this.timeBetween = timeBetween;
		this.varianz = varianz;
	}	
}
