using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
    public AudioSource m_MovementAudio;    
    public AudioClip m_EngineIdling;       
    public AudioClip m_EngineDriving;      
    public float m_PitchRange = 0.2f;

    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;

    private bool _mineOnCooldown;
    public float MinePlaceCooldownTime;
    public float CurrentMinePlaceCooldownTime;
    
    public Rigidbody Mine;
    public Transform MinePlaceTransform;

    [HideInInspector]
    public List<Rigidbody> Mines;

    [HideInInspector]
    public bool IsPlayer;

    [HideInInspector]
    public int CurrentGameType;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;

        _mineOnCooldown = false;
        CurrentMinePlaceCooldownTime = MinePlaceCooldownTime;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        Mines = new List<Rigidbody>();

        m_OriginalPitch = m_MovementAudio.pitch;
    }

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
        
        EngineAudio();

        if (_mineOnCooldown) Cooldown();
        else CheckPlaced();
        
    }

    private void CheckPlaced()
    {
        if (!Input.GetButtonUp("PlaceMine" + m_PlayerNumber) || _mineOnCooldown) return;

        Rigidbody mineInstance = Instantiate(Mine, MinePlaceTransform.position, MinePlaceTransform.rotation)
            as Rigidbody;
        Mines.Add(mineInstance);

        _mineOnCooldown = true;
    }

    private void Cooldown()
    {
        CurrentMinePlaceCooldownTime -= Time.deltaTime;

        if (CurrentMinePlaceCooldownTime <= 0)
        {
            _mineOnCooldown = false;
            CurrentMinePlaceCooldownTime = MinePlaceCooldownTime;
        }
    }

    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
        if(Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if(m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        Turn();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
        // Creates a vector in the tanks forward direction * by the movement value * by the speed * by delta time
        // Delta time makes movements per second rather than by frame
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        // # of degrees to turn
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}