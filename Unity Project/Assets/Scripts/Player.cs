using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour {

	PlayerController controller;

	public float speed = 7f;
	public Transform weaponHolder;

	void Start () {
		controller = GetComponent <PlayerController> (); 
	}

	void Update () {
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 velocity = input.normalized * speed;
		controller.Move (velocity);

		Vector3 difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - weaponHolder.position).normalized;
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		weaponHolder.rotation = Quaternion.Euler (0f, 0f, rotZ - 90);

	}


}
