using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : TankMovement {

    private Transform _transform;
    private NavMeshAgent _nav;
    public bool IsMoving;

	public override void Awake()
    {
        base.Awake();
    }

    public override void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        IsMoving = false;
        _mineOnCooldown = false;
        CurrentMinePlaceCooldownTime = MinePlaceCooldownTime;
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void Start()
    {
        _nav = GetComponent<NavMeshAgent>();

        // Finds a gameobject on Layer 9 (Layer of players) and sets
        // the opposing tank as a the target
        GameObject[] gos = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in gos)
        {
            if (go.layer == 9 && Vector3.Distance(go.transform.position, transform.position) != 0) _transform = go.transform;
        }

        Mines = new List<Rigidbody>();
        m_OriginalPitch = m_MovementAudio.pitch;
    }

    public override void Update()
    {
        if(IsMoving) _nav.SetDestination(_transform.position);

        EngineAudio();

        if (_mineOnCooldown) Cooldown();
        else CheckPlaced();
    }

    public override void CheckPlaced()
    {
        base.CheckPlaced();
    }

    public override void Cooldown()
    {
        base.Cooldown();
    }

    public override void EngineAudio()
    {
        base.EngineAudio();
    }
}
