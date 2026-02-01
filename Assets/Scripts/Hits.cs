using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hits : MonoBehaviour
{
    private bool isHitting;

    private bool softHit;
    private bool hardHit;
    private bool parry;
    private bool grab;

    private Animator animator;

    private void Awake() => animator = GetComponent<Animator>();

    private void Update()
    {
        hardHit = Gamepad.current.buttonNorth.wasPressedThisFrame;
        softHit = Gamepad.current.buttonWest.wasPressedThisFrame;
        parry = Gamepad.current.buttonEast.wasPressedThisFrame;
        grab = Gamepad.current.buttonSouth.wasPressedThisFrame;

        if (!isHitting)
        {
            if (softHit) ActiveSoftHit();
            else if (hardHit) ActiveHardHit();
            else if (parry)ActiveParry();
            else if (grab) ActiveGrab();
        }
      
    }

    private void ActiveSoftHit()
    { 
        isHitting = !isHitting;
        AnimationHandler.SoftAttackAnim(animator);
        isHitting = !isHitting;
    }

    private void ActiveHardHit()
    {
        isHitting = !isHitting;
        AnimationHandler.HardAttackAnim(animator);
        isHitting = !isHitting;
    }

    private void ActiveParry()
    {
        isHitting = !isHitting;
        AnimationHandler.ParryAnim(animator);
        isHitting = !isHitting;
    }

    private void ActiveGrab()
    {
        isHitting = !isHitting;
        AnimationHandler.CatchAnim(animator);
        isHitting = !isHitting;
    }
}
