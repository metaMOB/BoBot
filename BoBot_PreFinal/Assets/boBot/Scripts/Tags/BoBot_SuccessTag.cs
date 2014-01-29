﻿using UnityEngine;
using System.Collections;

public class BoBot_SuccessTag : MonoBehaviour {
	public int nextRiddle;
	
	private string fileToLoad = "";
	
	
	// Use this for initialization
	void Start () {
		fileToLoad = static_holder.file_to_load;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (){
		//SAVE!!
		try {
			Debug.Log ("Success!!! "+static_holder.file_to_load);
			BoBotGlobal.environment.setActiveRiddle (nextRiddle);
			Save_Load.Write_Data_Player(fileToLoad, GameObject.Find("Player").transform.localPosition.x,GameObject.Find("Player").transform.localPosition.y,GameObject.Find("Player").transform.localPosition.z);
		}
		catch {
		}
	}
}
