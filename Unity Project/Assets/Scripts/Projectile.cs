using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour {

	public float speed = 35f;
	public int damage = 2;
	public float lifeTime = 5f;
	public LayerMask canHit;

	void Start () {
		Destroy (gameObject, lifeTime);
	}

	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
		transform.Translate (Vector2.up * moveDistance);
	}

	void CheckCollisions (float moveDistance) {

		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up, moveDistance, canHit);
		if (hit.collider != null) {
			OnHitObject (hit);
		}

	}
		
	void OnHitObject (RaycastHit2D hit) {
		if (hit.collider.tag == "Player") {
			CmdPlayerShot (hit.collider.name);
		}
		Destroy (gameObject);
	}

	[Command]
	void CmdPlayerShot(string playerID) {
		GameManager.GetPlayer (playerID).TakeDamage(damage);
	}


}
 