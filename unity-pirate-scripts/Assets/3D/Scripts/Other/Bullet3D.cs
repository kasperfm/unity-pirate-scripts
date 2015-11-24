using UnityEngine;
using System.Collections;

public class Bullet3D : MonoBehaviour {

	public bool moving = true;
	public float speed = 5.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(waitAndDestroy(4.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			transform.Translate((transform.forward * speed) * Time.deltaTime, Space.World);
		}
	}

	IEnumerator waitAndDestroy(float timer){
		yield return new WaitForSeconds(timer);
		Destroy(this.gameObject);
	}
}
