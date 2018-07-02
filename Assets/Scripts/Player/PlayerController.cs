
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController {

    public bool slash;
    public GameObject SlashHitBox;
    private int slashFrames = 9;
    private int slashFramesCount = 0;
    protected override void Start()
    {
        RBody = GetComponent<Rigidbody2D>();
    }
    protected override void Update()
    {
        JumpUpdate(Input.GetButtonDown("Jump"), Input.GetButtonUp("Jump"), Input.GetButton("Jump"));
        FallUpdate(Input.GetAxisRaw("Vertical"));
        WalkUpdate(Input.GetAxisRaw("Horizontal"));
        SlashUpdate();
    }

    private void SlashUpdate()
    {
        if(Input.GetButtonDown("Slash") && !slash)
        {
            slashFramesCount = 0;
            slash = true;
            SlashHitBox.SetActive(true);
        }
        if(slashFramesCount < slashFrames)
        {
            slashFramesCount++;
        }
        else
        {
            slash = false;
            SlashHitBox.SetActive(false);
        }
    }

}
