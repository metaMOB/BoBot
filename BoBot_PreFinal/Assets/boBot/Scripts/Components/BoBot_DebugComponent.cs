using UnityEngine;
using System.Collections;

public class BoBot_DebugComponent : MonoBehaviour {
	
	private TextMesh text;
	private string staticText;
	private string dynamicText;
	private GameObject parent;
	
	public float localXPosition = 0f;
	public float localYPosition = 2.5f;
	private int rows = 0;
	private BoxCollider collider;
	private Vector3 collSize;
	private Transform collTransform;
	private Transform thisTransform;
	private Vector3 pos = Vector3.zero;
	private Quaternion rot;
	//private float angle = 0;
	//private float distance = 5f;
	//private Rigidbody rigid;
	
	void Start () {
		thisTransform = this.transform;
		text = gameObject.GetComponent<TextMesh>();
		text.fontSize = 10;
		text.alignment = TextAlignment.Left;
		text.anchor = TextAnchor.MiddleCenter;
		text.text = "Missing";
		parent = this.transform.parent.gameObject;
		staticText +=  parent.name + " | Layer "+parent.layer+"\n";
		rot = this.parent.transform.rotation;
		rot.z = 0;
	}
	
	void Update () {
		if (BoBotGlobal.debugging){
			Vector3 pos = new Vector3 (localXPosition, localYPosition + rows*text.characterSize,0);
			thisTransform.localPosition  = pos;
			text.text = staticText + dynamicText;
			dynamicText = "";
			rows = 0;
		} 
	}
	
	void FixedUpdate(){
		if (BoBotGlobal.debugging){
			this.transform.rotation = rot;
		}
	}
	
	public void addText(string text){
		dynamicText += text+"\n";
		rows++;
	}	
}
