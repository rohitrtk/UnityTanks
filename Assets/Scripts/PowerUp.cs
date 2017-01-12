using UnityEngine;

public struct Types
{
    public const int BonusDamage = 0;
    public const int BonusHealth = 1;
    public const int BonusSpeed = 2;
}

public class PowerUp : MonoBehaviour {

    private Rigidbody _rb;
    private const float _turnAdd = 2f;
    private int _currentPowerUp;

    public float _maxDamage;
    public float _healthAdd;
    public float _speedAdd;

    void Start ()
    {
        ChooseRandomPowerUp();
	}

    void OnEnable()
    {
        ChooseRandomPowerUp();
    }
	
	void Update ()
    {
	}

    void OnTriggerEnter(Collider col)
    {
        if (!(col.gameObject.CompareTag("Player"))) return;
      
        _rb = col.GetComponent<Rigidbody>();

        if (!_rb) return;

        switch(_currentPowerUp)
        {
            case (Types.BonusDamage):
                BonusDamage();
                break;
            case (Types.BonusHealth):
                BonusHealth();
                break;
            case (Types.BonusSpeed):
                BonusSpeed();
                break;
        }
    }

    private void ChooseRandomPowerUp()
    {
        _currentPowerUp = (int) Mathf.Round(Random.Range(0f, 2f));

        if(_currentPowerUp == Types.BonusDamage) GetComponent<Renderer>().material.color = Color.red;
        else if(_currentPowerUp == Types.BonusHealth) GetComponent<Renderer>().material.color = Color.green;
        else if(_currentPowerUp == Types.BonusSpeed) GetComponent<Renderer>().material.color = Color.blue;
    }

    private void BonusDamage()
    {        
        TankShooting ts = _rb.GetComponent<TankShooting>();
        ShellExplosion se = ts.m_Shell.GetComponent<ShellExplosion>();
        se.m_MaxDamage = _maxDamage;

        gameObject.SetActive(false);
    }

    private void BonusHealth()
    {
        TankHealth th = _rb.GetComponent<TankHealth>();

        if (th.GetHealth() == th.m_StartingHealth) return;

        th.AddHealth(_healthAdd);
        if (th.GetHealth() > th.m_StartingHealth) th.SetHealth(th.m_StartingHealth);
        th.SetHealthUI();

        gameObject.SetActive(false);
    }

    private void BonusSpeed()
    {
        TankMovement tm = _rb.GetComponent<TankMovement>();
        tm.m_Speed += _speedAdd;
        //tm.m_TurnSpeed *= _turnAdd;

        gameObject.SetActive(false);
    }
}