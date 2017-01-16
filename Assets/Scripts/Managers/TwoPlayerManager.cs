using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TwoPlayerManager : GameManager
{
    #region _SETUP_
    public override void Start()
    {
        CurrentGameType = GameType.Twoplayer;

        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SpawnAllTanks();
        SetCameraTargets();

        // Start the game loop
        StartCoroutine(GameLoop());
    }

    public override void SpawnAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].m_Instance =
                Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation)
                    as GameObject;

            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();

            m_Tanks[i].CurrentGameType = (int)CurrentGameType;
        }
    }

    public override void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Tanks.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Tanks[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
    }
    #endregion

    #region _LOOP_CONTROL_
    public override IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (m_GameWinner != null)
        {
            //SceneManager.LoadScene(0);
            SceneManager.LoadScene("Main");
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    public override IEnumerator RoundStarting()
    {
        ResetAllTanks();
        DisableTankControl();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;

        yield return m_StartWait;
    }

    public override IEnumerator RoundPlaying()
    {
        EnableTankControl();

        // Instantiates a new power up at a random spawn location
        int sp = Random.Range(0, PowerupSpawnPoints.Length);
        powerupRB = Instantiate(PowerUp, PowerupSpawnPoints[sp].position, PowerupSpawnPoints[sp].rotation)
            as GameObject;

        m_MessageText.text = string.Empty;

        while (!OneTankLeft())
        {
            yield return null;
        }
    }

    public override IEnumerator RoundEnding()
    {
        DisableTankControl();

        // Destroy created round power up
        Destroy(powerupRB.gameObject);

        foreach (TankManager tank in m_Tanks)
        {
            foreach (Rigidbody rigidBody in tank.getTankMovement().Mines)
            {
                if (rigidBody == null) continue;
                Destroy(rigidBody.gameObject);
            }

            tank.getTankMovement().Mines.Clear();
        }

        m_RoundWinner = null;
        m_RoundWinner = GetRoundWinner();

        if (m_RoundWinner != null) m_RoundWinner.m_Wins++;

        m_GameWinner = GetGameWinner();

        string message = EndMessage();

        m_MessageText.text = message;

        yield return m_EndWait;
    }
    #endregion

    public override bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }

    public override TankManager GetRoundWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                return m_Tanks[i];
        }

        return null;
    }

    public override TankManager GetGameWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                return m_Tanks[i];
        }

        return null;
    }

    public override string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }

    public override void ResetAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].Reset();
        }
    }

    public override void EnableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].EnableControl();
        }
    }

    public override void DisableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].DisableControl();
        }
    }
}