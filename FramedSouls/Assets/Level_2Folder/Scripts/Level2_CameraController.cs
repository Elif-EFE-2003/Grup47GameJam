using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_CameraController : MonoBehaviour
{

	public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -3.5f);	
	}
}
