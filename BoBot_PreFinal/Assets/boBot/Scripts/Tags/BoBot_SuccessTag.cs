using UnityEngine;
using System.Collections;

public class BoBot_SuccessTag : MonoBehaviour {
	public int nextRiddle;	
	public bool final = false;
	
	private string fileToLoad = "";
	
	void Start () {
		fileToLoad = static_holder.file_to_load;
	}
	
	void OnTriggerEnter (){
		try {
			if (!final){
				BoBotGlobal.environment.setActiveRiddle (nextRiddle);
				Save_Load.Write_Data_Player(fileToLoad, GameObject.Find("Player").transform.localPosition.x,GameObject.Find("Player").transform.localPosition.y,GameObject.Find("Player").transform.localPosition.z);
			} else {
				BoBotGlobal.input_menu = 1f;	
			}
		}
		catch {
		}
	}
}
