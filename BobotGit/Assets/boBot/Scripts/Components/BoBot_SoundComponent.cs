using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoBot_BasicPhysicsComponent))]
public class BoBot_SoundComponent : MonoBehaviour {
	
	public AudioClip horizontalSound;
	public AudioClip verticalHitSound;
	
	public float verticalHitGate = 0.3f;
	
	private AudioSource audioSourceMove;
	private AudioSource audioSourceHit;
	
	private BoBot_BasicPhysicsComponent basicPhysics;
	
//	private Transform rigid;
//	
//	private Vector3 lastPos = Vector3.zero;
//	private Vector3 lastDelta = Vector3.zero;
//	private Vector3 deltaTwo;
	private BoBot_DebugComponent debugInfo;

	// Use this for initialization
		
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		//rigid = gameObject.rigidbody.transform;
		//lastPos = rigid.position;
		
		audioSourceMove = gameObject.AddComponent<AudioSource>();
		audioSourceHit = gameObject.AddComponent<AudioSource>();
		
		audioSourceMove.clip = horizontalSound;
		audioSourceHit.clip = verticalHitSound;
		audioSourceHit.volume = 10;
	//	Debug.Log ("ssadsd "+gameObject.name);
		basicPhysics = gameObject.GetComponent<BoBot_BasicPhysicsComponent>();
		//Debug.Log (basicPhysics);
	}
	
	// Update is called once per frame
	void Update(){
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("SoundComponent");
			//debugInfo.addText ("> Delta2 "+(deltaTwo.x).ToString("#0.##")+ "/"+(deltaTwo.y).ToString("#0.##"));
			
			debugInfo.addText ("> Ply Move "+audioSourceMove.isPlaying);
			debugInfo.addText ("> Vol Move "+(audioSourceMove.volume).ToString("0.00"));
			debugInfo.addText ("> Ply Move "+audioSourceHit.isPlaying);
			debugInfo.addText ("> Vol Move "+(audioSourceHit.volume).ToString("0.00"));
		}
	}
	
	void FixedUpdate () {
		//Vector3 delta = rigid.position - lastPos;
		//deltaTwo = delta - lastDelta;
		try {
			audioSourceMove.volume = Mathf.Abs(basicPhysics.delta.x*50);
		}
		
		catch {
			Debug.Log ("error "+gameObject.name);	
		}
			
		if (basicPhysics.deltaTwo.y > verticalHitGate && !audioSourceHit.isPlaying){
			
			audioSourceHit.loop = false;
			audioSourceHit.Play();
		} 
		//Debug.Log ("vh "+ gameObject.name +"  "+basicPhysics.deltaTwo.y);
		
		if (basicPhysics.delta.x < -0.01f || basicPhysics.delta.x > 0.01f){
			if (!audioSourceMove.isPlaying && gameObject.rigidbody.useGravity){
			//Debug.Log ("play");
				audioSourceMove.loop = false;				
				audioSourceMove.Play();
			} 
		} else if (audioSourceMove.isPlaying){
			//Debug.Log ("stop");
			audioSourceMove.Stop();
		}
		//lastPos = rigid.position;
		//lastDelta = delta;		
	}
	
	
}
