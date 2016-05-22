using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class CamCycler : NetworkBehaviour {

	[SerializeField]
	public List<CameraConfiguration> camTargets;
	public int currentCamera = 0;
	public float jumpDistance = 10F;
	
	void Start () {
		
	}
	
	void Update () {
		
		if (this.isLocalPlayer) {
			
			if (Input.GetButtonDown("Fire2")) {
				
				// Next camera.
				this.CycleCamera(1);
				
			}
			
			CameraConfiguration conf = this.camTargets[this.currentCamera];
			Transform cam = Camera.main.transform;
			
			Vector3 diff = conf.target.position - cam.position;
			Debug.Log(diff.magnitude);
			
			if (diff.sqrMagnitude <= Mathf.Pow(this.jumpDistance, 2)) {
				
				Vector3 delta = Vector3.Lerp(cam.position, conf.target.position, conf.moveSpeed * Time.deltaTime) - cam.position;
				cam.GetComponent<CharacterController>().Move(delta);
				cam.rotation = Quaternion.Slerp(cam.rotation, conf.target.rotation, conf.rotateSpeed * Time.deltaTime);
				
			} else {
				cam.position = conf.target.position;
			}
			
		}
		
	}
	
	public void CycleCamera(int diff) {
		this.currentCamera = (this.currentCamera + diff) % this.camTargets.Count;
	}
	
	[System.Serializable]
	public struct CameraConfiguration {
		
		public Transform target;
		public float moveSpeed;
		public float rotateSpeed;
		
	}
	
}
