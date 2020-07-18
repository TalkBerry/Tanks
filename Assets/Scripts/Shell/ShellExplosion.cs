using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;
    [SerializeField] private GameObject _expPref;


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {

        // Find all the tanks in an 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);
        // Go through all the colliders...
        // for 
        for(int i = 0; i<hitColliders.Length;i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRb = hitColliders[i].GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRb)
                continue;

            // Add an explosion force.
            targetRb.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
            // Find the TankHealth script associated with the rigidbody.
            TankHealth th = targetRb.GetComponent<TankHealth>();
            if (!th)
                continue;

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.

            // Calculate the amount of damage the target should take based on it's distance from the shell
            float damage = CalculateDamage(targetRb.position);

            // Deal this damage to the tank.
            th.TakeDamage(damage);

        }

        // Unparent the particles from the shell.

        // Play the particle system.
        ParticleManager.Instance.PlayExplosionParticle(transform.position);

        // Play the explosion sound effect.

        // Destroy the shell.
        Destroy(this.gameObject);

    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target.
        Vector3 distance =  targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float expDistance = distance.magnitude;


        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float normalizedDamage = (m_ExplosionRadius - expDistance) / m_ExplosionRadius;


        // Calculate damage as this proportion of the maximum possible damage.
        float damage = normalizedDamage * m_MaxDamage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max(0, damage);

        return damage;
    }

    private void InitExpPref()
    {

    }
}