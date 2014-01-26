using UnityEngine;
using System.Collections;

public class Light_Flicker : MonoBehaviour {

public float time = 0.05f;
public float min= 0.5f;
public float max= 1.0f;
public bool useSmooth = false;
public float smoothTime = 30.0f;
private GameObject feuer;

void Start () {
		
	if(useSmooth == false && light){
	InvokeRepeating("OneLightChange", time, time);
	}
}
void OneLightChange () {
	light.intensity = Random.Range(min,max);

}

void Update () {
	if(useSmooth == true && light){
		light.intensity = Mathf.Lerp(light.intensity,Random.Range(min,max),Time.deltaTime*smoothTime);
	}
	if(light == false){
		print("Please add a light component for light flicker");
	}
}
}
