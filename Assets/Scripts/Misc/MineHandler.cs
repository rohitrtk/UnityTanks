using UnityEngine;

/// <summary>
/// This class handles the explosions for the mines; each mine will have an instance of this class
/// </summary>
public class MineHandler : MonoBehaviour
{
    #region _VARIABLES_
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public LayerMask m_TankMask;
    public float m_ExplosionRadius = 5f;
    public TankMovement tankMovement;

    public float PrimeTime;
    public float CountTime;
    public bool Primed = false;
    #endregion

    private void Start()
    {
        CountTime = PrimeTime;
        if (CountTime > PrimeTime) CountTime = PrimeTime;
    }

    private void Update()
    {
        if (Primed) return;

        CountTime -= Time.deltaTime;
        if (CountTime <= 0)
        {
            Primed = true;
            CountTime = PrimeTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Primed) return;

        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody) continue;

            Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation)
                as Rigidbody;
            shellInstance.velocity = 1 * m_FireTransform.forward;
        }

        tankMovement.Mines.Remove(GetComponent<Rigidbody>());
        Destroy(gameObject);
    }
}