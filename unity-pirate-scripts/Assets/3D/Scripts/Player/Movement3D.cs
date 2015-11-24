/*
    Formålet med dette script er at håndtere spillerens bevægelse.
    Skrevet af: Kasper Færch Mikkelsen - Coding Pirates Viborg
*/

using UnityEngine;

// Definerer de forskellige typer spilleren kan være.
public enum PlayerObjectType{
	Ball, Cube, Model
}

// Der skal være et Rigidbody komponent på objektet for at dette script kan fungere.
[RequireComponent(typeof(Rigidbody))]

public class Movement3D : MonoBehaviour {

    // Variabel til at gemme objektets rigidbody i.
	private Rigidbody rb;

    // Den originale bevægelses hastighed bliver gemt når spillet starter.
	private float origMoveSpeed;

    /// <summary>
    /// Den type dit spiller-objekt er.
    /// </summary>
    [Header("Indstillinger")]
	public PlayerObjectType playerObjectType = PlayerObjectType.Ball;

    /// <summary>
    /// Den hastighed du bevæger dig med.
    /// </summary>
    public float moveSpeed = 3.0f;

    /// <summary>
    /// Spillerens start position.
    /// </summary>
    public Vector3 startPosition = new Vector3();

    /// <summary>
    /// Når spilleren er under dette punkt på Y-aksen, starter man forfra.
    /// </summary>
    public float resetUnderYAxis = -4.0f;

    /// <summary>
    /// Skal man kunne bevæge sig i flere retninger samtidig?
    /// </summary>
    public bool multipleDirections = true;

    /// <summary>
    /// Gå fremad.
    /// </summary>
    [Header("Knapper")]
    public KeyCode moveForward	 = KeyCode.UpArrow;

    /// <summary>
    /// Gå baglæns.
    /// </summary>
    public KeyCode moveBackwards = KeyCode.DownArrow;

    /// <summary>
    /// Gå til venstre.
    /// </summary>
    public KeyCode moveLeft		 = KeyCode.LeftArrow;

    /// <summary>
    /// Gå til højre.
    /// </summary>
    public KeyCode moveRight	 = KeyCode.RightArrow;

    /// <summary>
    /// Hold denne knap nede for at løbe.
    /// </summary>
	public KeyCode runButton	 = KeyCode.LeftShift;

    // Dette bliver kørt når spillet starter.
	void Start () {
		rb = GetComponent<Rigidbody>();
		origMoveSpeed = moveSpeed;
	}

    /// <summary>
    /// Flytter spilleren til den valgte position.
    /// </summary>
    /// <param name="position">Position</param>
	public void ResetPosition(Vector3 position){
		rb.isKinematic = true;
		transform.position = position;
		rb.isKinematic = false;
	}

	/// <summary>
	/// Flytter spilleren til start positionen.
	/// </summary>
	public void ResetToStartPosition(){
		rb.isKinematic = true;
		transform.position = startPosition;
		rb.isKinematic = false;
	}
	
    // Køre denne funktion hele tiden i spillet (hver frame).
	void Update ()
	{
        // Tjekker om spilleren er under et bestemt punkt på Y-aksen.
		if (transform.position.y <= resetUnderYAxis) {
			ResetPosition(startPosition);
		}

        // Variabel til at gemme den retning du vil bevæge dig i.
		Vector3 movement = new Vector3 ();

        // Tryk på fremad knappen.
		if (Input.GetKey (moveForward)) {
			if(multipleDirections){
				movement += Vector3.forward;
			}else{
				movement = Vector3.forward;
			}
		}
		
        // Tryk på baglæns knappen.
		if (Input.GetKey (moveBackwards)) {
			if(multipleDirections){
				movement += Vector3.back;
			}else{
				movement = Vector3.back;
			}
		}
		
        // Tryk på venstre knappen.
		if (Input.GetKey (moveLeft)) {
			if(multipleDirections){
				movement += Vector3.left;
			}else{
				movement = Vector3.left;
			}
		}
		
        // Tryk på højre knappen.
		if (Input.GetKey (moveRight)) {
			if(multipleDirections){
				movement += Vector3.right;
			}else{
				movement = Vector3.right;
			}
		}

        // Tjekker om løbe-knappen er holdt nede.
        // Hvis ja, så ganger vi hastigheden med 6.
		if (Input.GetKey (runButton)) {
			moveSpeed = origMoveSpeed * 6;
		} else {
			moveSpeed = origMoveSpeed;
		}

        // Bruger force til at bevæge spilleren med hvis du er en bold.
        // Ellers bevæg dig normalt.
		if (playerObjectType == PlayerObjectType.Ball) {
			rb.AddForce (movement * moveSpeed, ForceMode.VelocityChange);
		} else if (playerObjectType == PlayerObjectType.Cube && movement != Vector3.zero) {
			transform.Translate((movement * moveSpeed) * Time.deltaTime, Space.World);
		}
	}
}
