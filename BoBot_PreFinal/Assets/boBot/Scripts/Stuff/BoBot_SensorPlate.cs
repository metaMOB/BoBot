using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoBot_SensorPlate : MonoBehaviour {
	
	private List<BoBot_ControlComponent> controllers = new List<BoBot_ControlComponent>();
	public GameObject [] control = null;
	public int channel = 1;
	
	public bool onlyOn = true;
	public float delayOn = 1f;
	public float delayOff = 5f;
	public List<GameObject> objects = new List<GameObject>();
	private List<string> objectName = new List<string>();
		
	private int numElements = 0;
	private float timer = 0f;
	private float timerOff = 0f;
	private bool running = false;
	
	// Use this for initialization
	void Start () {
		foreach (GameObject obj in control){
			BoBot_ControlComponent [] cmps = obj.GetComponents<BoBot_ControlComponent>();
			foreach (BoBot_ControlComponent cmp in cmps){
				controllers.Add(cmp);
			}			
			
			cmps = obj.GetComponentsInChildren<BoBot_ControlComponent>();
			foreach (BoBot_ControlComponent cmp in cmps){
				controllers.Add(cmp);
			}		
		}
		
		foreach (GameObject obj in objects){
			objectName.Add(obj.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (running){
			if (numElements > 0){
				if (timer < delayOn){
					timer += Time.deltaTime;
				} else {
					foreach (BoBot_ControlComponent controller in controllers){
						controller.setValue (1, channel);
					}
				}
			} else {
				
				if (timer < delayOn+delayOff){
					timer += Time.deltaTime;
				} else {
					foreach (BoBot_ControlComponent controller in controllers){
						controller.setValue (0, channel);
					}
					timer = 0f;	
					running = false;
				}
			}
			
			numElements = 0;
		}
	}
	
	void OnTriggerStay (Collider other){
		if (objectName.Count == 0 ||  objectName.Contains(other.name)){
			numElements++;			
			running = true;
		}
	}
}
