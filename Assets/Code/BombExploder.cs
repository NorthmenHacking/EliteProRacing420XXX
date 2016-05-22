using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BombExploder : NetworkBehaviour {
	
	[SyncVar]
	public float timeRemaining = 0F;
	
	public float maxDist = 1F;
	public float power = 1F;
	public float lift = 0F;
	public GameObject postEffects;
	
	void Update() {
		
		if (!this.isServer) return;
		
		// Try to explode.
		timeRemaining -= Time.deltaTime;
		if (timeRemaining <= 0) {
			this.CmdExplode();
		}
		
	}
	
	[Command]
	private void CmdExplode() {
		
		Collider[] cols = Physics.OverlapSphere(this.transform.position, this.maxDist);
		List<ForceCollector> appliedAlready = new List<ForceCollector>();
		
		foreach (Collider hit in cols) {
			
			if ((hit.gameObject.layer & LayerMask.NameToLayer("Terrain")) > 0) continue;
			
			if (hit.attachedRigidbody != null) {
				
				Debug.Log("hit " + hit.gameObject);
				
				NetworkIdentity ni = hit.GetComponentInParent<NetworkIdentity>();
				ForceCollector fc = hit.GetComponentInParent<ForceCollector>();
				if (ni != null && fc != null) {
					
					ScreenLog.Log("name:" + hit.name + " ni:" + ni + " fc:" + fc);
					
					if (appliedAlready.Contains(fc)) continue;
					appliedAlready.Add(fc);
					fc.RpcExplode(this.power, this.transform.position, this.maxDist, this.lift);
					
				} else {
					hit.attachedRigidbody.AddExplosionForce(this.power, this.transform.position, this.maxDist, this.lift);
				}
								
			}
			
		}
		
		NetworkServer.Destroy(this.gameObject);
		GameObject.Destroy(this.gameObject);
		
		// Pretty!
		if (this.postEffects == null) return;
		GameObject effects = (GameObject) GameObject.Instantiate(this.postEffects, this.transform.position, Quaternion.identity);
		NetworkServer.Spawn(effects);
		
	}
	
}
