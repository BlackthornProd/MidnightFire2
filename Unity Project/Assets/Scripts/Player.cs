using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : NetworkBehaviour {

	Rigidbody2D rb;

	[Header("General")]
	public float speed = 7f;
	[SyncVar] public int health = 10;

	[Header("Weapon")]
	public Transform weaponHolder;
	public Weapon startingWeapon;
	public Transform shotPoint;
	public Weapon[] weapons;

	Vector2 velocity;
	Weapon equipedWeapon;
	float nextShotTime;


	void Start () {
		rb = GetComponent <Rigidbody2D> ();
	}

	public override void OnStartClient ()
	{
		base.OnStartClient ();
		EquipWeapon (0);
	}
		
		
	void Update () {
		#region MOVEMENT INPUT
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		velocity = input.normalized * speed;
		#endregion

		#region WEAPON TURNING
		Vector3 difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - weaponHolder.position).normalized;
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		weaponHolder.rotation = Quaternion.Euler (0f, 0f, rotZ - 90);
		#endregion

		#region SHOOTING INPUT
		if (equipedWeapon != null) {
			if (Input.GetMouseButtonDown (0) && !equipedWeapon.isAutomatic && Time.time > nextShotTime) {
				nextShotTime = Time.time + equipedWeapon.secondsBetweenShots;
				CmdShoot ();
			} else if (Input.GetMouseButton (0) && equipedWeapon.isAutomatic && Time.time > nextShotTime) {
				nextShotTime = Time.time + equipedWeapon.secondsBetweenShots;
				CmdShoot ();
			}
		}
		#endregion
			
		/* if (Input.GetKeyDown (KeyCode.U)) {
			CmdEquipWeapon (1);
		}

		if (Input.GetKeyDown (KeyCode.I)) {
			CmdEquipWeapon (0);
		} */
	}

	void FixedUpdate () {
		rb.position += velocity * Time.fixedDeltaTime;
	}

	#region EQUIP WEAPON FUNCTIONS
	[Command]
	void CmdEquipWeapon (int index) {
		RpcEquipWeapon (index);
	}

	[ClientRpc]
	void RpcEquipWeapon (int index) {
		EquipWeapon (index);
	}

	void EquipWeapon (int index) {
		if (equipedWeapon != null) {
			equipedWeapon.gameObject.SetActive (false);	
		}
		equipedWeapon = weapons [index];
		equipedWeapon.gameObject.SetActive (true);
	}
	#endregion

	#region SHOOT FUNCTION
	[Command]
	void CmdShoot() {
		Projectile newProjectile = (Projectile)Instantiate (equipedWeapon.projectile, shotPoint.position, weaponHolder.rotation);
		NetworkServer.Spawn (newProjectile.gameObject);
	}
	#endregion

	#region TAKE DAMAGE FUNCTION
	public void TakeDamage (int amount) {
		health -= amount;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
	#endregion


}
