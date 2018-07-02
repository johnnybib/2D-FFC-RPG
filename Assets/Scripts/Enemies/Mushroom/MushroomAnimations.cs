using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAnimations : MonoBehaviour
{
    private MushroomController MushroomCon;
    private Animator Anim;
    private SpriteRenderer SpriteRend;


    // Use this for initialization
    void Start()
    {
        MushroomCon = gameObject.GetComponent<MushroomController>();
        Anim = gameObject.GetComponent<Animator>();
        SpriteRend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Anim.SetFloat("absSpeedX", Math.Abs(MushroomCon.GetSpeedX() * 0.1f));
        Anim.SetFloat("speedX", MushroomCon.GetSpeedX());
    }
}
