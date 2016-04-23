/*
    Her styres indstillingerne for kamp systemet.
    Skrevet af: Kasper Færch Mikkelsen - Coding Pirates Viborg
*/

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movement3D))]
public class Combat3D : MonoBehaviour {

    /// <summary>
    /// Den knap der skal trykkes på for at skyde.
    /// </summary>
    [Header("Kamp-system")]
    public KeyCode shootButton = KeyCode.LeftControl;

    /// <summary>
    /// Den prefab som indeholder selve skudet.
    /// </summary>
    public GameObject BulletPrefab;

    /// <summary>
    /// Antal liv som spilleren har.
    /// </summary>
    public int life = 10;

    /// <summary>
    /// Værdien af skade som hvert skud giver.
    /// </summary>
	public int attackDamage = 1;

    [Header("Hændelser")]
    public UnityEvent OnDie;
	public UnityEvent OnAttack;
    public UnityEvent OnEnemyHit;
	public UnityEvent OnHit;

    /// <summary>
    /// Når man affyre et skud, køres dette.
    /// </summary>
	public void Attack(){
        GameObject bullet = Instantiate(BulletPrefab, this.transform.position + this.transform.forward, transform.rotation) as GameObject;
        bullet.GetComponent<Bullet3D>().SetupBullet(this.gameObject, attackDamage);
		OnAttack.Invoke();
	}


    /// <summary>
    /// Når man bliver ramt, trækkes skade-værdien fra spillerens liv.
    /// </summary>
    /// <param name="damage">Skade du modtager</param>
	public void Hit(int damage){
		if (life > 0) {
			life -= damage;
			OnHit.Invoke();
		} else {
			Die();
		}
	}

    /// <summary>
    /// Køres når man dør.
    /// </summary>
	public void Die(){
		FindObjectOfType<Movement3D> ().ResetToStartPosition();
		OnDie.Invoke();
	}

    /// <summary>
    /// Afspiller en lyd på objektets AudioSource komponent.
    /// </summary>
    /// <param name="sound">Lyden der skal afspilles</param>
    public void PlaySound(AudioClip sound) {
        if (GetComponent<AudioSource>()) {
            GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }

    // Update udføres hele tiden i spillet.
	void Update(){
		if (Input.GetKeyUp(shootButton)) {
			Attack();
		}
	}
}
