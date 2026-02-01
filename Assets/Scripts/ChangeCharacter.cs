using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCharacter : MonoBehaviour
{
    [SerializeField] MovementScript player1Script;
    [SerializeField] MovementScript player2Script;

    private void Start()
    {
        player1Script.enabled = true;
       //player2Script.enabled = false;
    }

    private void Update()
    {
        bool gamepadSwitch = Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame;

        if (gamepadSwitch) SwitchPlayer();
    }

    private void SwitchPlayer()
    {
        player1Script.enabled = !player1Script.enabled;
       // player2Script.enabled = !player2Script.enabled;

        if (player1Script.enabled) Debug.Log("Controlando al JUGADOR 1");
        else Debug.Log("Controlando al JUGADOR 2");
    }
}
