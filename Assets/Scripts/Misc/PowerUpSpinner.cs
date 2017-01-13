using UnityEngine;

public class PowerUpSpinner : MonoBehaviour {

	void Update ()
    {
        transform.Rotate(new Vector3(15, 38, 45) * Time.deltaTime);
	}
}
