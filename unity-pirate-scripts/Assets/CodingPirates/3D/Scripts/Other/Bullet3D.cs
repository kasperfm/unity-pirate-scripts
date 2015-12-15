/*
    Dette script skal sidde på skud-prefab'en.
    Skrevet af: Kasper Færch Mikkelsen - Coding Pirates Viborg
*/

using UnityEngine;
using System.Collections;

public class Bullet3D : MonoBehaviour {

    /// <summary>
    /// Bestemmer om skudet bevæger sig.
    /// </summary>
    private bool moving = false;

    /// <summary>
    /// Den hastighed skudet bevæger sig med.
    /// </summary>
    public float speed = 5.0f;

    /// <summary>
    /// Den skade som skudet giver den det rammer.
    /// </summary>
    private int damageValue = 1;

    /// <summary>
    /// Den spiller der har skudt.
    /// </summary>
    private GameObject bulletOwner;

    // Køres så snart dette objekt bliver oprettet.
    void Start () {
		StartCoroutine(waitAndDestroy(4.0f));
	}
	
	// Update bliver kørt hver frame.
	void LateUpdate () {
		if (moving) {
			transform.Translate((transform.forward * speed) * Time.deltaTime, Space.World);
		}
	}

    /// <summary>
    /// Denne funktion skal køres for hvert skud.
    /// Dette er for at sætte de rigtige variabler.
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="damage"></param>
    public void SetupBullet(GameObject owner, int damage) {
        bulletOwner = owner;
        damageValue = damage;
        moving = true;
    }

    // Når dette objekt rammer en fjende der har et tag med navnet "Enemy".
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            bulletOwner.GetComponent<Combat3D>().OnEnemyHit.Invoke();
        }

        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Combat3D>().Hit(damageValue);
        }
        Destroy(this.gameObject);
    }

    // Ødelægger dette objekt når der er gået et bestemt antal sekunder.
    IEnumerator waitAndDestroy(float timer){
		yield return new WaitForSeconds(timer);
		Destroy(this.gameObject);
	}
}
