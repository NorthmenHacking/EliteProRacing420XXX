using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RacingNetworkManager : NetworkManager {
	
	public string playerSpawnTransformName;
	
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
		
		Vector3 spawnPos = GameObject.Find(this.playerSpawnTransformName).transform.position;
		
		GameObject player = (GameObject) GameObject.Instantiate(this.playerPrefab, spawnPos, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		
	}
	
}
