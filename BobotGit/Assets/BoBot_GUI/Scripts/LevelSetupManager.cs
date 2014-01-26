using UnityEngine;
using System.Collections;

public class LevelSetupManager : MonoBehaviour {
	
	public GameObject playerPrefab;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("boBot");
		if (!player){
			player = (GameObject) Instantiate(playerPrefab, transform.position, transform.rotation);
			player.name = "boBot";
		}
		
		PlayerPrefs.SetInt("unlocked" + Application.loadedLevelName, 1);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
