using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoBot_EnvironmentComponent : MonoBehaviour {
	
	GameObject [] riddleObjs;
	
	void Start () {
		riddleObjs = GameObject.FindGameObjectsWithTag("riddle");
		foreach (GameObject riddleObj in riddleObjs){
			riddleObj.SetActive(false);
			Rigidbody [] rigids = riddleObj.GetComponentsInChildren<Rigidbody>();
			foreach (Rigidbody rigid in rigids){
				rigid.Sleep();
			}
		}
	}
	
	public void setActiveRiddle(int nr){
		foreach (GameObject riddleObj in riddleObjs){
			BoBot_RiddleComponent riddle = riddleObj.GetComponent<BoBot_RiddleComponent>();
			if (riddle.riddleNr == nr || riddle.riddleNr == nr+1 || riddle.riddleNr == nr-1){
				riddleObj.SetActive(true);
				Rigidbody [] rigids = riddleObj.GetComponentsInChildren<Rigidbody>();
				foreach (Rigidbody rigid in rigids){
					rigid.WakeUp();
				}
			} else {
				riddleObj.SetActive(false);
				Rigidbody [] rigids = riddleObj.GetComponentsInChildren<Rigidbody>();
				foreach (Rigidbody rigid in rigids){
					rigid.Sleep();
				}			
			}
		}		
	}	
}
