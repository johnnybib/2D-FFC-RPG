using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private PlayerController PlayerCon;
    private Animator Anim;
    private SpriteRenderer SpriteRend;


    // Use this for initialization
    void Start () {
        PlayerCon = gameObject.GetComponent<PlayerController>();
        Anim = gameObject.GetComponent<Animator>();
        SpriteRend = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Anim.SetInteger("currentState", PlayerCon.currentState);
        Anim.SetFloat("speedX", System.Math.Abs(PlayerCon.GetSpeedX() * 0.1f));
        Anim.SetBool("slash", PlayerCon.slash);
        if((PlayerCon.GetSpeedX() > 0 && SpriteRend.flipX) || (PlayerCon.GetSpeedX() < 0 && !SpriteRend.flipX) && (PlayerCon.currentState != (int)PlayerController.State.FALLING && PlayerCon.currentState != (int)PlayerController.State.JUMPING))
        {
            SpriteRend.flipX = !SpriteRend.flipX;
            PlayerCon.SlashHitBox.transform.localPosition = new Vector2(-PlayerCon.SlashHitBox.transform.localPosition.x, PlayerCon.SlashHitBox.transform.localPosition.y);
            PlayerCon.SlashHitBox.transform.localScale = new Vector2(-PlayerCon.SlashHitBox.transform.localScale.x, PlayerCon.SlashHitBox.transform.localScale.y);
        }
    }
}
