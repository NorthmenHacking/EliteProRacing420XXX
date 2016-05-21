using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BombSpawner : NetworkBehaviour {

	public GameObject bombPrefab;
	public Transform spawnPos;
	public float bombSeconds;
	
	void Update () {
		
		if (!this.isLocalPlayer) return;
		
		if (Input.GetButtonDown("Fire1")) {
			this.CmdSpawnBomb();
		}
		
	}
	
	[Command(channel = 2)]
	private void CmdSpawnBomb() {
		
		GameObject bomb = (GameObject) GameObject.Instantiate(this.bombPrefab, this.spawnPos.position, Quaternion.identity);
		
		BombExploder exp = bomb.GetComponent<BombExploder>();
		exp.timeRemaining = bombSeconds;
		
		Rigidbody rb = bomb.GetComponent<Rigidbody>();
		rb.velocity = this.GetComponent<Rigidbody>().velocity;
		
		NetworkServer.Spawn(bomb);
		
	}
	
}
