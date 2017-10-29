using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;

	Vector2 velocity;

	void Start () {
		rb = GetComponent <Rigidbody2D> ();
	}

	public void Move (Vector2 newVelocity) {
		velocity = newVelocity;
	}

	void FixedUpdate () {
		rb.position += velocity * Time.fixedDeltaTime;
	}

}
