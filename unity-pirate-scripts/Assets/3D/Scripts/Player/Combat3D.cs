using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movement3D))]
public class Combat3D : MonoBehaviour {

	public KeyCode shootButton = KeyCode.LeftControl;
	public int life = 10;
	public int attackDamage = 1;
	public GameObject BulletPrefab;

	public UnityEvent OnDie;
	public UnityEvent OnAttack;
	public UnityEvent OnHit;

	public void Attack(){
		Instantiate(BulletPrefab, this.transform.position + this.transform.forward, Quaternion.identity);
		OnAttack.Invoke();
	}

	public void Hit(int damage){
		if (life > 0) {
			life -= damage;
			OnHit.Invoke();
		} else {
			Die();
		}
	}

	public void Die(){
		FindObjectOfType<Movement3D> ().ResetToStartPosition();
		OnDie.Invoke();
	}

	void Update(){
		if (Input.GetKeyUp(shootButton)) {
			Attack();
		}
	}
}
