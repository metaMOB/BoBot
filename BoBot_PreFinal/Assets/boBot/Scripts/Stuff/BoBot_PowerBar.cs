using UnityEngine;
using System.Collections;

public class BoBot_PowerBar : MonoBehaviour {
	
	public float timeBetween = 5f;
	public float varianz = 0.2f;
	public float particleOnTime = 0.5f;
	
	private float timer;
	private float nextTime;
	public GameObject flickerObject;
	public ParticleSystem [] emitters;
	public AudioClip [] sounds;
	public float volume = 0.5f;
	
	private bool running = false;
	private Collider collider;
	private AudioSource snd;
	private Color clr;
	// Use this for initialization
	void Start () {
		timer = 0f;
		collider = gameObject.GetComponentInChildren<Collider>();
		foreach (ParticleSystem emitter in emitters){
			emitter.Stop();				
		}
		snd = gameObject.AddComponent<AudioSource>();		
		snd.volume = volume;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer == 0){
			nextTime = timeBetween + Random.value * ((timeBetween * varianz * 2)) - timeBetween * varianz;
			AudioClip clip;
			do {
				clip = sounds[Mathf.FloorToInt(Random.value * sounds.Length)];
			} while (clip == snd.clip);
			snd.clip = clip;
			snd.PlayDelayed (nextTime);
			particleOnTime = clip.length / 2;
			clr = new Color (1f,1f,1f,1f);
			flickerObject.renderer.material.color = clr;
		} 
	
		timer += Time.deltaTime;	
		
		if (timer > nextTime){ 		
			clr = new Color (1f,1f,1f, Random.value);
			flickerObject.renderer.material.color = clr;
			
			
			if (!running){
				foreach (ParticleSystem emitter in emitters){
					Vector3 pos = emitter.transform.position;
					// this.renderer.bounds.size.x;
					pos.x = collider.bounds.min.x + Random.value * collider.bounds.size.x;
					emitter.transform.position = pos;
					emitter.Play();
					running = true;					
				}
			}
			
			
			
			if (timer > nextTime+particleOnTime){
				foreach (ParticleSystem emitter in emitters){
					emitter.Stop();
					emitter.transform.Rotate(new Vector3( 0f, 0f, Random.value * 100f -50f) );					
				}
				running = false;
				timer = 0f;
				
			}
		} 
	}
}
