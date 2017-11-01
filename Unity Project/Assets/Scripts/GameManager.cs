using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	static Dictionary<string, Player> players = new Dictionary<string, Player>();

	public static void RegisterPlayer (string netID, Player player) {
		string playerId = "Player " + netID;
		players.Add (playerId, player);
		player.transform.name = playerId;
	}

	public static Player GetPlayer (string playerID) {
		return players [playerID];
	}

}
