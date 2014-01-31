using UnityEngine;
using System.Collections;

public class AudioFade : MonoBehaviour {
	
	public static bool FadeIn = true;
	public static bool FadeOut =false;
	
	enum Fade {In, Out}
	public float fadeTime=4.0f;
	
	// Use this for initialization
	void Start () {
	
		if(!FadeIn){
			
			StartCoroutine(FadeAudio(fadeTime, Fade.In));
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!FadeIn){
			
			StartCoroutine(FadeAudio(fadeTime, Fade.In));
			FadeIn = false;
		}
		
		if (!FadeOut){
	
	    	StartCoroutine(FadeAudio(fadeTime, Fade.Out));
			FadeOut =true;
		}
	}
	
	 IEnumerator FadeAudio(float timer, Fade fadeType) {


	    float start = fadeType == Fade.In? 1.0f : 0.0f;
	    float end = fadeType == Fade.In? 0.0f : 1.0f;
	
	    float i = 0.0f;
	    float step = 1.0f/timer;

    	while (i <= 1.0) {

        	i += step * Time.deltaTime;
        	audio.volume = Mathf.Lerp(start, end, i);

    		yield return new WaitForSeconds(step * Time.deltaTime);

 		}
	}
	
	IEnumerator FadeAudio2 (float timer, Fade fadeType) {

	    float start = fadeType == Fade.In? 0.0f : 1.0f;
	    float end = fadeType == Fade.In? 1.0f : 0.0f;
	    float i = 0.0f;
	    float step = 1.0f/timer;

   		 while (i <= 1.0f) {

 			i += step * Time.deltaTime;
			audio.volume = Mathf.Lerp(start, end, i);

    		yield return new WaitForSeconds(step * Time.deltaTime);

 		}
	}
	
	public static void MenueIsActive(bool active){
			
		if(active){
			FadeIn = true;
			FadeOut =false;
		}//if
		else{
			FadeIn = false;
			FadeOut =true;
		}
		
	
	}
}
