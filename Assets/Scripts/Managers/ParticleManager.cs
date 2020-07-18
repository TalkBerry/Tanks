using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomeStudio.Core;

public class ParticleManager : Singleton<ParticleManager>
{
    // Start is called before the first frame update
    [SerializeField] private ParticleSystem _expPref;
    [SerializeField] private ParticleSystem _tankExpPref;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayExplosionParticle(Vector3 playPosition)
    {
        ParticleSystem ps = Instantiate(_expPref, playPosition, Quaternion.identity) as ParticleSystem;
        ps.Play();
        Destroy(ps.gameObject, 2);

    }

    public void PlayTankExplosionParticle(Vector3 playPosition)
    {
        ParticleSystem ps = Instantiate(_expPref, playPosition, Quaternion.identity) as ParticleSystem;
        ps.Play();
        Destroy(ps.gameObject, 2);

    }
}
