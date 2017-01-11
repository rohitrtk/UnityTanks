using UnityEngine;

public struct Types
{
    public const int BonusDamage = 0;
    public const int BonusHealth = 1;
    public const int BonusSpeed = 2;
}

public class PowerUp : MonoBehaviour {

    private Rigidbody _rb;
    private const float _maxDamage = 200f;
    private const float _healthAdd = 50f;
    private const float _speedAdd = 4f;
    private const float _turnAdd = 2f;
    private int _currentPowerUp;

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
        var r = Mathf.Round(Random.Range(0f, 2f));
        _currentPowerUp = Types.BonusSpeed;

        if(_currentPowerUp == Types.BonusDamage) GetComponent<Renderer>().material.color = Color.red;
        else if(_currentPowerUp == Types.BonusHealth) GetComponent<Renderer>().material.color = Color.green;
        else if(_currentPowerUp == Types.BonusSpeed) GetComponent<Renderer>().material.color = Color.blue;
    }

    private void BonusDamage()
    {        
        if (!_rb) return;
        TankShooting ts = _rb.GetComponent<TankShooting>();
        ShellExplosion se = ts.m_Shell.GetComponent<ShellExplosion>();
        se.m_MaxDamage = _maxDamage;
        
        Destroy(gameObject);
    }

    private void BonusHealth()
    {
        if (!_rb) return;
        TankHealth th = _rb.GetComponent<TankHealth>();
        th.AddHealth(_healthAdd);
        
        Destroy(gameObject);
    }

    private void BonusSpeed()
    {
        if (!_rb) return;
        TankMovement tm = _rb.GetComponent<TankMovement>();
        tm.m_Speed += _speedAdd;
        //tm.m_TurnSpeed *= _turnAdd;
        
        Destroy(gameObject);
    }
}