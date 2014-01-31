using UnityEngine;
using System.Collections;

public class BoBot_SuccessTag : MonoBehaviour {
	public int nextRiddle;	
	public bool final = false;
	
	private SceneFader sceneFader;
	private string fileToLoad = "";
	
	void Start () {
		fileToLoad = static_holder.file_to_load;
	}
	
	void OnTriggerEnter (Collider other){
		//try {
			//if (!final){
			if (other.CompareTag("Player")){
				if(!final){
				BoBotGlobal.environment.setActiveRiddle (nextRiddle);
				Save_Load.Write_Data_Player(static_holder.file_to_load, GameObject.Find("Player").transform.localPosition.x,GameObject.Find("Player").transform.localPosition.y,GameObject.Find("Player").transform.localPosition.z);
				} else {
				sceneFader = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SceneFader> ();
				sceneFader.SwitchScene("StartMenu");
			}
			
		}
		/*}
		catch {
		}*/
	}
}
