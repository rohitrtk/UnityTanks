using UnityEngine;

/// <summary>
/// Handles AI movement and shooting
/// </summary>
public class AIHandler : MonoBehaviour {

    private Transform _transform;
    private UnityEngine.AI.NavMeshAgent _nav;

	private void Start ()
    {
        _nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Finds a gameobject on Layer 9 (Layer of players) and sets
        // the opposing tank as a the target
        GameObject[] gos = FindObjectsOfType(typeof(GameObject)) as GameObject[]; 
        foreach (GameObject go in gos)
        {
            if (go.layer == 9 && Vector3.Distance(go.transform.position, transform.position) != 0) _transform = go.transform;
        }
    }
	
	private void Update ()
    {
        _nav.SetDestination(_transform.position);
	}
}