using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [SerializeField]
    public GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destructable.DoDestroy(this.gameObject);
        }
    }

    [Header("Player Movement Input")]
    public float horizontalMoveAxis;
    public float verticalMoveAxis;

    public void ReadMovementInput(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontalMoveAxis = inputVector.x;
        verticalMoveAxis = inputVector.y;
    }

    [Header("Look Around input")]
    public float horizontalLookAxis;
    public float verticalLookAxis;
    
    public void ReadLookInput(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontalLookAxis = inputVector.x;
        verticalLookAxis = inputVector.y;
    }

    [Header("Interact input")]
    public bool interactPressed;

    public void ReadInteractInput(InputAction.CallbackContext context)
    {
        interactPressed = !context.canceled;
        StartCoroutine("ResetInteractPressed");
    }

    private IEnumerator ResetInteractPressed()
    {
        yield return new WaitForEndOfFrame();
        interactPressed = false;
    }

    [Header("left click input")]
    public bool leftClickPressed;

    public void ReadLeftClickInput(InputAction.CallbackContext context)
    {
        leftClickPressed = !context.canceled;
        StartCoroutine("ResetLeftClickPressed");
    }

    private IEnumerator ResetLeftClickPressed()
    {
        yield return new WaitForEndOfFrame();
        leftClickPressed = false;
    }

    [Header("flash toggle input")]
    public bool flashTogglePressed;

    public void ReadFlashToggleInput(InputAction.CallbackContext context)
    {
        flashTogglePressed = !context.canceled;
        StartCoroutine("ResetFlashTogglePressed");
    }

    private IEnumerator ResetFlashTogglePressed()
    {
        yield return new WaitForEndOfFrame();
        flashTogglePressed = false;
    }


    //public void ReadInteractInput(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        Debug.Log("Interact");
    //        player.GetComponent<Interact>().OnInteract();
    //    }
    //}
}
