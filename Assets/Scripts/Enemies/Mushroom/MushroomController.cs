using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : BaseController {

    protected override void Start()
    {
        RBody = GetComponent<Rigidbody2D>();
    }
    protected override void Update()
    {
        JumpUpdate(Input.GetButtonDown("Jump"), Input.GetButtonUp("Jump"), Input.GetButton("Jump"));
        FallUpdate(Input.GetAxisRaw("Vertical"));
        WalkUpdate(Input.GetAxisRaw("Horizontal"));
    }
}
