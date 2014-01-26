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
	// Use this for initialization
	private Vector3 pos = Vector3.zero;
	private Quaternion rot;
	//private float angle = 0;
	//private float distance = 5f;
	//private Rigidbody rigid;
	
	void Start () {
		//Vector3 pos = new Vector3 (localXPosition, localYPosition,0);
		//MeshRenderer mesh = GetComponent<MeshRenderer>().mesh;
		//this.transform.localPosition = pos;
		thisTransform = this.transform;
		text = gameObject.GetComponent<TextMesh>();
		text.fontSize = 10;
		text.alignment = TextAlignment.Left;
		text.anchor = TextAnchor.MiddleCenter;
		text.text = "Missing";
		parent = this.transform.parent.gameObject;
		staticText +=  parent.name + " | Layer "+parent.layer+"\n";
		//staticText += "Tag: "+this.transform.parent.tag + "\n";
		//staticText += "L: "+this.transform.parent.tag + "\n";
		/*collider = gameObject.AddComponent<BoxCollider>();
		collider.isTrigger = true;
		gameObject.layer = 11;
		rigid = gameObject.AddComponent<Rigidbody>();
		rigid.useGravity = false;
		rigid.isKinematic = true;
		
		/*Physics.IgnoreLayerCollision (11, 0);
		Physics.IgnoreLayerCollision (11, 1);
		Physics.IgnoreLayerCollision (11, 2);
		Physics.IgnoreLayerCollision (11, 3);
		Physics.IgnoreLayerCollision (11, 4);
		Physics.IgnoreLayerCollision (11, 5);
		Physics.IgnoreLayerCollision (11, 6);
		Physics.IgnoreLayerCollision (11, 7);
		Physics.IgnoreLayerCollision (11, 8);
		Physics.IgnoreLayerCollision (11, 9);
		Physics.IgnoreLayerCollision (11, 10);*/
		rot = this.parent.transform.rotation;
		rot.z = 0;
		//text.text = staticText;
	}
	
	// Update is called once per frame
	void Update () {
		//Quaternion rot = this.parent.transform.rotation;
		//rot.z = 0;
		
		
		if (BoBotGlobal.debugging){
			Vector3 pos = new Vector3 (localXPosition, localYPosition + rows*text.characterSize,0);
			//Vector3 newPos = new Vector3(Mathf.Cos (angle), Mathf.Sin(angle), 0)*distance;
			thisTransform.localPosition  = pos;
			//collTransform.localPosition = pos;
			text.text = staticText + dynamicText;
			//collSize = text.renderer.bounds.size;		
			//collider.size = collSize ;
			//pos = Vector3.zero;
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
