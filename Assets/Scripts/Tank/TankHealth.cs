using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;

   [SerializeField] private float _currentHealth;

    private void OnEnable()
    {
        ResetHealth();
    }

    private void Start()
    {
        ResetHealth();
    }

    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        _currentHealth -= amount;
        SetHealthUI();
        if (_currentHealth <= 0)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        m_Slider.value = _currentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, _currentHealth/m_StartingHealth);

    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        ParticleManager.Instance.PlayTankExplosionParticle(transform.position);
        this.gameObject.SetActive(false);
    }

    private void ResetHealth()
    {
        m_Slider.maxValue = _currentHealth = m_StartingHealth;

        SetHealthUI();
    }
}