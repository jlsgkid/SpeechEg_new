using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum MoveStatus
{
	NONE,
	UP,
	DOWN,
	LEFT,
	RIGHT
}

public class PlayerMove : MonoBehaviour {
	
	public float moveSpeed = 5.0f; 
	public GameObject eye_dPos;
	public  MoveStatus status = MoveStatus.NONE;
	private CharacterController controller;
	private float gravity = 300f;

	void Start(){
		controller = this.GetComponent<CharacterController> ();
	}

	void Update(){
		
		if(GvrController.IsTouching){
			//Debug.Log ("x=" + pos.x + "y=" + pos.y);
			TrackTouch ();
			TrackMove ();
		}
		if(GvrController.TouchUp){
			status = MoveStatus.NONE;
		}
	}

	void TrackTouch(){
		Vector2 curPos = GvrController.TouchPos;
		if (GvrController.TouchPos.x > 0.8) {
			//Debug.Log ("RIGHT");
			status = MoveStatus.RIGHT;
		} else if (GvrController.TouchPos.x<0.2) { 
			//Debug.Log ("LEFt");
			status = MoveStatus.LEFT;
		} else if (GvrController.TouchPos.y < 0.2) {
			//Debug.Log ("UP");
			status = MoveStatus.UP;
		} else if(GvrController.TouchPos.y>0.8){
			//Debug.Log ("DOWN");
			status = MoveStatus.DOWN;
		}
	}

	void TrackMove(){
		
		Vector3 transformValue = new Vector3(); 
		switch(status){  
		case MoveStatus.UP:  
			transformValue = eye_dPos.transform.forward * Time.deltaTime;  
			break;  
		case MoveStatus.DOWN:  
			transformValue = (-eye_dPos.transform.forward) * Time.deltaTime;  
			break;  
		case MoveStatus.LEFT:  
			transformValue = (-eye_dPos.transform.right )* Time.deltaTime;  
			break;  
		case MoveStatus.RIGHT:  
			transformValue = eye_dPos.transform.right * Time.deltaTime;  
			break;  
		} 
		//transform.forward = eye_dPos.transform.forward;
		//transform.position += new Vector3(transformValue.x * moveSpeed,
			//transformValue.y * moveSpeed,transformValue.z * moveSpeed) ;

		transformValue = transform.TransformDirection(transformValue);
		transformValue *= moveSpeed;
		transformValue.y -= gravity * Time.deltaTime;
		   //transform.position += new Vector3(transformValue.x,transformValue.y ,transformValue.z ) ;
		controller.Move(transformValue);
	}


}
