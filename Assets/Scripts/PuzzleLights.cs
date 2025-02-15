using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PuzzleLights : PuzzlesFather
{
    Hud hud;
    [Header("Configuracion de las luces")]
    [SerializeField] private GameObject controladorLuces;
    [SerializeField] private GameObject controles;
    [SerializeField] private GameObject particulas;
    [SerializeField] private Animator animatorPuerta;

    public bool acceder = true;

    public override void Interact()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
        hud.activado = true;
        controladorLuces.SetActive(true);
    }

    public override void Exit()
    {
        hud.activado = false;
        controles.SetActive(false);

        if (!acceder)
        {
            particulas.SetActive(false);
            animatorPuerta.SetTrigger("Rotar");
            Destroy(GetComponent<PuzzleLights>());
        }

        if (controladorLuces.activeInHierarchy)
        {
            controladorLuces.SetActive(false);
        }
    }
}
