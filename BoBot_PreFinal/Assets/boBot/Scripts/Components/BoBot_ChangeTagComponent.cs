﻿using UnityEngine;
using System.Collections;

public class BoBot_ChangeTagComponent : BoBot_OnOffComponent {

	private bool done = false;	
	public string newTag = "";
		
	void Update () {
		if (state && !done){
			done = true;
			foreach (Transform obj in gameObject.GetComponentsInChildren<Transform>()){
				obj.gameObject.tag = newTag;	
			}
			gameObject.tag = newTag;			
		}
	}
}
