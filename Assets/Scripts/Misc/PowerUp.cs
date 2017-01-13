using UnityEngine;

/// <summary>
/// Ints that represent what bonus will be given when player picks up powerup
/// </summary>
public struct Types
{
    public const int BonusDamage = 0;
    public const int BonusHealth = 1;
    public const int BonusSpeed = 2;
}

public class PowerUp : MonoBehaviour {

    #region _VARIABLES_
    private Rigidbody _rb;
    private const float _turnAdd = 2f;
    private int _currentPowerUp;

    public float _maxDamage;
    public float _healthAdd;
    public float _speedAdd;
    #endregion

    private void Start ()
    {
        ChooseRandomPowerUp();
	}

    private void OnTriggerEnter(Collider col)
    {
        if (!(col.gameObject.CompareTag("Player"))) return;
      
        _rb = col.GetComponent<Rigidbody>();
        if (!_rb) return;

        if (_currentPowerUp == Types.BonusDamage) BonusDamage();
        else if(_currentPowerUp == Types.BonusHealth) BonusHealth();
        else if(_currentPowerUp == Types.BonusSpeed) BonusSpeed();
        else
        {
            // Will probably add more power ups
        }
    }

    /// <summary>
    /// Chooses a random bonus that this power up will contain
    /// </summary>
    private void ChooseRandomPowerUp()
    {
        _currentPowerUp = (int) Mathf.Round(Random.Range(0f, 2f));

        if(_currentPowerUp == Types.BonusDamage) GetComponent<Renderer>().material.color = Color.red;
        else if(_currentPowerUp == Types.BonusHealth) GetComponent<Renderer>().material.color = Color.green;
        else if(_currentPowerUp == Types.BonusSpeed) GetComponent<Renderer>().material.color = Color.blue;
    }

    /// <summary>
    /// Rework this method!
    /// </summary>
    private void BonusDamage()
    {        
        TankShooting ts = _rb.GetComponent<TankShooting>();
        ShellExplosion se = ts.m_Shell.GetComponent<ShellExplosion>();
        se.m_MaxDamage = _maxDamage;

        Destroy(gameObject);
    }

    /// <summary>
    /// Acts as a med pack for the player, recovers lost health and caps at their max health
    /// </summary>
    private void BonusHealth()
    {
        TankHealth th = _rb.GetComponent<TankHealth>();

        if (th.GetHealth() == th.m_StartingHealth) return;

        th.AddHealth(_healthAdd);
        if (th.GetHealth() > th.m_StartingHealth) th.SetHealth(th.m_StartingHealth);
        th.SetHealthUI();

        Destroy(gameObject);
    }

    /// <summary>
    /// Gives the player bonus movespeed and turn speed;
    /// </summary>
    private void BonusSpeed()
    {
        TankMovement tm = _rb.GetComponent<TankMovement>();
        tm.m_Speed += _speedAdd;
        tm.m_TurnSpeed += _turnAdd;

        Destroy(gameObject);
    }
}