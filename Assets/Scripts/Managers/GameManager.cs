using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class GameManager : MonoBehaviour
{
    #region _VARIABLES_
    public int m_NumRoundsToWin = 5;        
    public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;           
    public CameraControl m_CameraControl;   
    public Text m_MessageText;              
    public GameObject m_TankPrefab;         
    public TankManager[] m_Tanks;

    public Transform[] PowerupSpawnPoints;
    public GameObject PowerUp;

    public GameType CurrentGameType = 0;

    protected int m_RoundNumber;              
    protected WaitForSeconds m_StartWait;     
    protected WaitForSeconds m_EndWait;       
    protected TankManager m_RoundWinner;
    protected TankManager m_GameWinner;

    protected GameObject powerupRB;

    [HideInInspector] public enum GameType { Singleplayer, Twoplayer, Multiplayer }
    #endregion

    public abstract void Start();
    public abstract void SpawnAllTanks();
    public abstract void SetCameraTargets();
    public abstract IEnumerator GameLoop();
    public abstract IEnumerator RoundStarting();
    public abstract IEnumerator RoundPlaying();
    public abstract IEnumerator RoundEnding();
    public abstract bool OneTankLeft();
    public abstract TankManager GetRoundWinner();
    public abstract TankManager GetGameWinner();
    public abstract string EndMessage();
    public abstract void ResetAllTanks();
    public abstract void EnableTankControl();
    public abstract void DisableTankControl();
}