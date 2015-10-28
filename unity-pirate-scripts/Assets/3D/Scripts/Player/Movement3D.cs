using UnityEngine;
using System.Collections;

public enum PlayerObjectType{
	Ball, Cube, Model
}

public class Movement3D : MonoBehaviour {

	private Rigidbody rb;
	private float origMoveSpeed;

	[Header("Indstillinger")]
	public PlayerObjectType playerObjectType = PlayerObjectType.Ball;
	public float moveSpeed = 3.0f;
	public Vector3 startPosition = new Vector3();
	public float resetUnderYAxis = -4.0f;
	public bool multipleDirections = true;

	[Header("Knapper")]
	public KeyCode moveForward	 = KeyCode.UpArrow;
	public KeyCode moveBackwards = KeyCode.DownArrow;
	public KeyCode moveLeft		 = KeyCode.LeftArrow;
	public KeyCode moveRight	 = KeyCode.RightArrow;
	public KeyCode runButton	 = KeyCode.LeftShift;

	void Start () {
		rb = GetComponent<Rigidbody>();
		origMoveSpeed = moveSpeed;
	}

	public void ResetPosition(Vector3 position){
		rb.isKinematic = true;
		transform.position = position;
		rb.isKinematic = false;
	}
	
	void Update ()
	{
		if (transform.position.y <= resetUnderYAxis) {
			ResetPosition(startPosition);
		}

		Vector3 movement = new Vector3 ();

		if (Input.GetKey (moveForward)) {
			if(multipleDirections){
				movement += Vector3.forward;
			}else{
				movement = Vector3.forward;
			}
		}
		
		if (Input.GetKey (moveBackwards)) {
			if(multipleDirections){
				movement += Vector3.back;
			}else{
				movement = Vector3.back;
			}
		}
		
		if (Input.GetKey (moveLeft)) {
			if(multipleDirections){
				movement += Vector3.left;
			}else{
				movement = Vector3.left;
			}
		}
		
		if (Input.GetKey (moveRight)) {
			if(multipleDirections){
				movement += Vector3.right;
			}else{
				movement = Vector3.right;
			}
		}

		if (Input.GetKey (runButton)) {
			moveSpeed = origMoveSpeed * 6;
		} else {
			moveSpeed = origMoveSpeed;
		}

		if (playerObjectType == PlayerObjectType.Ball) {
			rb.AddForce (movement * moveSpeed, ForceMode.VelocityChange);
		} else if (playerObjectType == PlayerObjectType.Cube && movement != Vector3.zero) {
			transform.Translate((movement * moveSpeed) * Time.deltaTime, Space.World);
		}
	}
}
