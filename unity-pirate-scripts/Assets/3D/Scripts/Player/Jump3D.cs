using UnityEngine;
using System.Collections;

public class Jump3D : MonoBehaviour {

	[Header("Knapper")]
	public KeyCode jumpButton = KeyCode.Space;

	[Header("Hoppe indstillinger")]
	public float jumpHeight = 5.0f;
	
	private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void Update(){
		Jump ();
	}

	void Jump(){
		if (Input.GetKeyDown (jumpButton)) {
			rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
		}
	}
}
