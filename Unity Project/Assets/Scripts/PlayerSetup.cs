using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

	public Behaviour[] componentsToEnable;


	void Start () {
		if (isLocalPlayer) {
			foreach (Behaviour component in componentsToEnable) {
				component.enabled = true;
			}

		}
	}

	public override void OnStartClient () {
		base.OnStartClient ();
		string myNetID = GetComponent <NetworkIdentity> ().netId.ToString ();
		Player player = GetComponent <Player> ();
		GameManager.RegisterPlayer (myNetID, player);
	}

}
