using UnityEngine;
using System.Collections;

public class PowerUpSpinner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 38, 45) * Time.deltaTime);
	}
}
