using System;
using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun.UtilityScripts;

public class Interaction : MonoBehaviourPunCallbacks
{
    private InputControl control;
    private Rigidbody rb;
    private Animator animator;
    [SerializeField] private float InteractRange;
    [SerializeField] private float InteractView;
    [SerializeField] private GameObject objetoRay;
    private Player player;

    [SerializeField] Canvas Provisional;
    private void Awake()
    {
        control = new InputControl();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }
    new private void OnEnable()
    {
        control.PlayerActions.Interact.performed += Interact;
        control.PlayerActions.Interact.Enable();
        control.PlayerActions.Uninteract.performed += ExitInteraction;
        control.PlayerActions.Uninteract.Enable();
        //control.PlayerActions.Crouch.performed += isCrouching;
        control.PlayerActions.Crouch.Enable();
    }


    new private void OnDisable()
    {
            control.PlayerActions.Interact.performed -= Interact;
            control.PlayerActions.Uninteract.performed -= ExitInteraction;
            control.PlayerActions.Crouch.performed -= ExitInteraction;
    }

    private void Interact(InputAction.CallbackContext A)
    {
        Vector3 rayOrigin = objetoRay.transform.position;
        Vector3 rayDirection = objetoRay.transform.forward;

        Ray r = new Ray(rayOrigin, rayDirection);

        if (Physics.Raycast(r, out RaycastHit hit, InteractRange))
        {
            if (hit.collider.gameObject.TryGetComponent(out Vent_JonnoEntrance interact))
            {
                interact.Interact(player.MyCamera, this.gameObject);
            }
            else if (hit.collider.gameObject.TryGetComponent(out Vent_JonnoExit interactable))
            {
                interactable.Interact(player.MyCamera, this.gameObject);
            }

            if (hit.collider.gameObject.TryGetComponent(out MonoBehaviour interactObj))
            {
                if (interactObj is PuzzlesFather puzzlesFather)
                {
                    StartCoroutine("StartInteraction", puzzlesFather);
                }
                else if (interactObj is PuzzleFather2 puzzlesFather2)
                {
                    StartCoroutine("StartInteraction", puzzlesFather2);
                }
            }
        }
    }

    private void ExitInteraction(InputAction.CallbackContext context)
    {
        if (player.isInteracting)
        {
            Vector3 rayOrigin = objetoRay.transform.position;
            Vector3 rayDirection = objetoRay.transform.forward;

            Ray r = new Ray(rayOrigin, rayDirection);

            if (Physics.Raycast(r, out RaycastHit hit, InteractRange))
            {
                if (hit.collider.gameObject.TryGetComponent(out MonoBehaviour interactObj))
                {
                    if (interactObj is PuzzlesFather puzzlesFather)
                    {
                        puzzlesFather.ChangeCamera(false);
                        puzzlesFather.Exit();
                        StartCoroutine("BackToNormalView");
                    }
                    else if (interactObj is PuzzleFather2 puzzlesFather2)
                    {
                        puzzlesFather2.ChangeCamera(false);
                        puzzlesFather2.Exit();
                        StartCoroutine("BackToNormalView");
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {

        // Configurar el color del gizmo
        Gizmos.color = Color.red;

        // Dibujar el rayo desde objetoRay en la direcci�n hacia adelante
        Vector3 rayOrigin = objetoRay.transform.position;
        Vector3 rayDirection = objetoRay.transform.forward;
        Gizmos.DrawRay(rayOrigin, rayDirection * InteractRange);
    }

    private IEnumerator BackToNormalView()
    {
        this.enabled = false;
        player.isInteracting = false;
        yield return new WaitForSeconds(1.8f);
        player.enabled = true;
        this.enabled = true;
    }

    private IEnumerator StartInteraction(PuzzlesFather puzzlesFather)
    {
        this.enabled = false;
        puzzlesFather.enabled = true;
        puzzlesFather.ChangeCamera(true);
        player.isInteracting = true;
        player.enabled = false;
        animator.SetFloat("MovX", 0);
        animator.SetFloat("MovY", 0);
        yield return new WaitForSeconds(1f);
        this.enabled = true;
    }

    private IEnumerator StartInteraction(PuzzleFather2 puzzlesFather2)
    {
        this.enabled = false;
        puzzlesFather2.enabled = true;
        puzzlesFather2.ChangeCamera(true);
        player.isInteracting = true;
        player.enabled = false;
        animator.SetFloat("MovX", 0);
        animator.SetFloat("MovY", 0);
        yield return new WaitForSeconds(1f);
        this.enabled = true;
    }

}