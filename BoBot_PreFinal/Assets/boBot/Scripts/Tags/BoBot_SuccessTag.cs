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
	
	void OnTriggerEnter (Collider other){
		if(other.CompareTag("Player")){
		 	
		
			if(final){
				sceneFader.SwitchScene("StartMenu");	
			}else {
				
				try {
					BoBotGlobal.environment.setActiveRiddle (nextRiddle);
					Debug.Log("save1 ");
					
					Save_Load.Write_Data_Player(Save_Load.ar_Player[3].ToString(), GameObject.Find("Player").transform.localPosition.x,GameObject.Find("Player").transform.localPosition.y,GameObject.Find("Player").transform.localPosition.z);
					Debug.Log("save2 ");
				}
				catch {
				}	
				
			}
		}
	}
}
