using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimationHandler : MonoBehaviour
{

    private Animator gunAnimator;


    // Start is called before the first frame update
    void Awake()
    {
        gunAnimator = GetComponent<Animator>();

    }

    public void AnimateGunFire()
    {
        gunAnimator.SetTrigger("Shoot");
    }

    public void AnimateGunReload()
    {
        gunAnimator.SetTrigger("Reload");
    }

}
