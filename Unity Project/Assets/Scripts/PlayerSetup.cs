using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	public Behaviour[] componentsToEnable;

	void Start () {
		if (isLocalPlayer) {
			foreach (Behaviour component in componentsToEnable) {
				component.enabled = true;
			}
		}
	}

}
