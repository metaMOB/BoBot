using UnityEngine;
using System.Collections;

public class BoBot_LightningSky : MonoBehaviour {
	private float thisZ;
	
	private float timer = 0f;
	private float duration = 0f;
	
	// Use this for initialization
	void Start () {
		thisZ = transform.position.z;
		gameObject.renderer.material.color = new Color (255, 255, 255, 0);
		scalePlane();
	}
	
	// Update is called once per frame
	void Update () {
		if (duration > 0f){
			timer += Time.deltaTime;
			
			//gameObject.renderer.material.color = new Color (255, 255, 255, Mathf.Lerp (1, 0, timer/duration));
			float intensity = Mathf.Lerp (1, 0, timer/duration);
			gameObject.renderer.material.color = new Color (255, 255, 255, Random.value * intensity);
			if (timer > duration){
				duration = 0f;	
				gameObject.renderer.material.color = new Color (255, 255, 255, 0);
			}
		}
	}
	
	public void setLightning(float duration){
		this.duration = duration;
		timer = 0f;
	}
	
	private void scalePlane (){
		var height = Screen.height;
  		var width = Screen.width;
    	transform.localScale = new Vector3(width, 0.1f, height);
	}
}
