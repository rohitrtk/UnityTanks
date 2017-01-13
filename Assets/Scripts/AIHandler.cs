using UnityEngine;
using System.Collections;

public class AIHandler : MonoBehaviour {

    private Transform _transform;

    private NavMeshAgent _nav;

	// Use this for initialization
	void Start ()
    {
        _nav = GetComponent<NavMeshAgent>();
        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; 
        foreach (GameObject go in gos)
        {
            if (go.layer == 9 && Vector3.Distance(go.transform.position, transform.position) != 0)
            {
                _transform = go.transform;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        _nav.SetDestination(_transform.position);
	}
}
