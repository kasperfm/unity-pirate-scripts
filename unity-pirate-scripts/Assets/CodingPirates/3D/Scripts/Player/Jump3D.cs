/*
    Dette script får spilleren til at hoppe ved tryk på en valgfri tast.
    Skrevet af: Kasper Færch Mikkelsen - Coding Pirates Viborg
*/

using UnityEngine;

// Der skal være et Rigidbody komponent på objektet for at dette script kan fungere.
[RequireComponent(typeof(Rigidbody))]

public class Jump3D : MonoBehaviour {

    /// <summary>
    /// Den knap der får spilleren til at hoppe.
    /// </summary>
	[Header("Knapper")]
	public KeyCode jumpButton = KeyCode.Space;

    /// <summary>
    /// Højden på et enkelt hop.
    /// </summary>
	[Header("Hoppe indstillinger")]
	public float jumpHeight = 5.0f;

    /// <summary>
    /// Hvor langt spilleren skal bevæge sig fremad når man hopper.
    /// </summary>
    public float jumpLength = 2.5f;

    // Variabel til at gemme objektets rigidbody i.
    private Rigidbody rb;

    // Renderer komponentet på spilleren.
    private Renderer rendererComponent;

    // Dette bliver kørt når spillet starter.
    void Start(){
		rb = GetComponent<Rigidbody>();
        rendererComponent = GetComponent<Renderer>();
    }

    // Køre denne funktion hele tiden i spillet (hver frame).
    void Update(){
        // Tjekker om spilleren står på jorden.
        RaycastHit hit;
        float distanceToGround = 0;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit)) {
            distanceToGround = hit.distance;
        }

        // Tjekker om der er trykket på hoppe-knappen.
        if (Input.GetKeyDown(jumpButton) && distanceToGround <= 0.1f + (rendererComponent.bounds.size.y / 2)) {
            Jump();
        }
	}

    /// <summary>
    /// Får spilleren til at hoppe.
    /// </summary>
	public void Jump(){
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);

        if (jumpLength != 0) {
            rb.AddForce(transform.forward * jumpLength, ForceMode.Impulse);
        }
	}
}
