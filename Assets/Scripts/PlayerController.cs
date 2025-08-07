using System;
using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpaceShip spaceShip;
    [SerializeField] private Skill basicAttack;
    [SerializeField] private Skill heavyAttack;
    [SerializeField] private Skill specialAttack;
    [SerializeField] private Skill parrying;
    [SerializeField] private Skill heal;

    [Header("Status")]
    [SerializeField] private float basicAttackInterval = 0.3f;

    [SerializeField] private bool autoFireLock = false;
    [SerializeField] private float autoFireLockTime = 8f;

    [SerializeField] private float targetEnergyAmount = 1f;
    [SerializeField] private float fillEnergyPerSecond = 0.2f;

    private float _time = 0f;
    
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        
        // Initialize
        InputActionMap inputs = playerInput.actions.FindActionMap("Player");
        inputs.FindAction("Move").performed += Move;
        inputs.FindAction("Move").canceled += Move;

        inputs.FindAction("Parry").performed += Parry;
        inputs.FindAction("Heal").performed += Heal;
        inputs.FindAction("Heavy Attack").performed += HeavyAttack;
        inputs.FindAction("Special Attack").performed += SpecialAttack;
        
        inputs.Enable();

        spaceShip.OnDead += () => autoFireLock = true;
        
        // Spaceship status
        previousHP = spaceShip.health;
        previousSP = spaceShip.Energy;
    }

    private void OnDestroy()
    {
        InputActionMap inputs = playerInput.actions.FindActionMap("Player");
        inputs.FindAction("Move").performed -= Move;
        inputs.FindAction("Move").canceled -= Move;
        
        inputs.FindAction("Parry").performed -= Parry;
        inputs.FindAction("Heal").performed -= Heal;
        inputs.FindAction("Heavy Attack").performed -= HeavyAttack;
        inputs.FindAction("Special Attack").performed -= SpecialAttack;
        
        inputs.Disable();
    }

    private void Update()
    {
        // Fill Energy
        if (spaceShip.Energy < 1)
        {
            float energyAddition = fillEnergyPerSecond * Time.deltaTime;
            spaceShip.Energy = spaceShip.Energy + energyAddition > targetEnergyAmount ? targetEnergyAmount : spaceShip.Energy + energyAddition;
        }
        
        // Basic Attack
        if (autoFireLock) return;
        
        _time += Time.deltaTime;
        
        if (_time >= basicAttackInterval)
        {
            _time -= basicAttackInterval;
            basicAttack.Use();
        }
    }

    private float previousHP;
    private float previousSP;
    private void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name == "StartScene") return;
        
        float hpDiff = 100 * (spaceShip.health - previousHP) / spaceShip.maxHealth;
        float spDiff = 100 * (spaceShip.Energy - previousSP) / spaceShip.MaxEnergy;
        
        UIManager.Instance.ChangeStatusValue("HP", hpDiff);
        UIManager.Instance.ChangeStatusValue("SP", spDiff);
        
        previousHP = spaceShip.health;
        previousSP = spaceShip.Energy;
    }

    private void Move(InputAction.CallbackContext context) => spaceShip.Move(context.ReadValue<Vector2>());
    private void Parry(InputAction.CallbackContext context) => parrying.Use();
    private void Heal(InputAction.CallbackContext context) => heal.Use();
    private void HeavyAttack(InputAction.CallbackContext context) => heavyAttack.Use();

    private void SpecialAttack(InputAction.CallbackContext context)
    {
        if (specialAttack.Use() == false) return;
        autoFireLock = true;
        StartCoroutine(UnlockAutoFire());
    }

    IEnumerator UnlockAutoFire()
    {
        yield return new WaitForSeconds(autoFireLockTime);
        if (SceneManager.GetActiveScene().name != "StartScene") autoFireLock = false;
    }
}
