using UnityEngine;
using System.Collections;

public class BoBot_Chain_Element : MonoBehaviour {
	
	public static class Global_boBot_Chain_Element {
		private static int numElements = 0;
		
		public static int getID (){
			return numElements++;			
		}		
	}	
	
	private GameObject player;
	private BoBot_SidescrollControl playerScript;
	public float distance;
	public bool openEnd = false;
		
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		playerScript = player.GetComponent<BoBot_SidescrollControl>();
		name = "ID"+Global_boBot_Chain_Element.getID();
	}
}
