using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimationHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleParticle;

    private Animator gunAnimator;


    // Start is called before the first frame update
    void Awake()
    {
        gunAnimator = GetComponent<Animator>();

    }

    public void AnimateGunFire()
    {
        gunAnimator.SetTrigger("Shoot");

        muzzleParticle.Play();
    }

    public void AnimateGunReload()
    {
        gunAnimator.SetTrigger("Reload");
    }

}
