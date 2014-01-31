using UnityEngine;
using System.Collections;

public class BoBot_SuccessTag : MonoBehaviour {
	public int nextRiddle;	
	public bool final = false;
	private string fileToLoad = "";
	private SceneFader sceneFader;
	
	void Start () {
		fileToLoad = static_holder.file_to_load;
		sceneFader = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneFader>();
	}
	
	void OnTriggerEnter (){
		if(final){
			sceneFader.SwitchScene("StartMenu");	
		}else {
			
			try {
				BoBotGlobal.environment.setActiveRiddle (nextRiddle);
				Save_Load.Write_Data_Player(fileToLoad, GameObject.Find("Player").transform.localPosition.x,GameObject.Find("Player").transform.localPosition.y,GameObject.Find("Player").transform.localPosition.z);
			}
			catch {
			}	
			
		}
	}
}
