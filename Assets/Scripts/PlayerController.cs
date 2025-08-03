using System;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpaceShip spaceShip;
    [SerializeField] private Skill heavyAttack;
    [SerializeField] private Skill specialAttack;
    [SerializeField] private Skill parrying;
    [SerializeField] private Skill heal;
    
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        
        // Initialize
        InputActionMap inputs = playerInput.actions.FindActionMap("Player");
        inputs.FindAction("Move").performed += Move;
        inputs.FindAction("Parry").performed += Parry;
        inputs.FindAction("Heal").performed += Heal;
        inputs.FindAction("Heavy Attack").performed += HeavyAttack;
        inputs.FindAction("Special Attack").performed += SpecialAttack;
        
        inputs.Enable();
    }

    private void OnDestroy()
    {
        InputActionMap inputs = playerInput.actions.FindActionMap("Player");
        inputs.FindAction("Move").performed -= Move;
        inputs.FindAction("Parry").performed -= Parry;
        inputs.FindAction("Heal").performed -= Heal;
        inputs.FindAction("Heavy Attack").performed -= HeavyAttack;
        inputs.FindAction("Special Attack").performed -= SpecialAttack;
        
        inputs.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
    }

    private void Parry(InputAction.CallbackContext context)
    {
        Debug.Log("Parry");
    }

    private void Heal(InputAction.CallbackContext context)
    {
        Debug.Log("Heal");
    }

    private void HeavyAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Heavy Attack");
    }

    private void SpecialAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Special Attack");
    }
}
