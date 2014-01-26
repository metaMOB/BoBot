using UnityEngine;
using System.Collections;

public class FireOnOff : MonoBehaviour {
	
	private string shower = "Shower";
	private string fireLight = "Feuerlicht";
	private GameObject rain;
	private bool fireOn = true;
	private GameObject light;
	
	// Use this for initialization
	void Start () {
		rain = GameObject.FindGameObjectWithTag(shower);
		light = GameObject.FindGameObjectWithTag(fireLight);
	}
	
	// Update is called once per frame
	void Update () {
		Fire(fireOn);
		
	}
	
	void Fire(bool fire){
		
		if(fire == false){
			light.light.enabled = false;
			if(rain.particleSystem.isStopped == true){
				this.gameObject.particleSystem.Play();
				fireOn = true;
			}
		}
		else if (fire == true){
			light.light.enabled = true;
				if(rain.gameObject.particleSystem.isPlaying == true){
					this.gameObject.particleSystem.Stop();
					
					fireOn = false;
				
				}
		}
		
	}
	
}
