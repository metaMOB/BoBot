using UnityEngine;
using System.Collections;

public class on_Off_Shower : MonoBehaviour {
	
	private bool showerOn = false;
	private string shower = "Shower";
	private string player = "Player";
	private GameObject rain;
	
	void Start(){
		rain = GameObject.FindGameObjectWithTag(shower);
		
	}

	void Update(){
		ShowerOnOff(showerOn);
	}
	void OnTriggerEnter(Collider other){
		
		if(other.gameObject.CompareTag(player)){
			showerOn = true;
		}
	}
	void OnTriggerExit(Collider other){
		
		if(other.gameObject.CompareTag(player)){
			showerOn = false;
		}
	}
	
	void ShowerOnOff(bool shower){
		
		if(shower == false){
			rain.particleSystem.Stop();
			//Debug.Log(rain.gameObject.particleSystem.isStopped);
		}
		else if (shower == true){
			rain.particleSystem.Play();
		}
	}
}
