using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_JonnoExit : MonoBehaviour
{
    private Camera Camera;
    public void Interact(Camera camera, GameObject jonno)
    {
        Camera = camera;
        StartCoroutine("CloseView", Camera);
    }

    IEnumerator CloseView(Camera camera)
    {
        camera.DOFieldOfView(0, 1f);
        
        /*
         * AQU� VA EL C�DIGO PARA 
         * CUANDO EMPIEZA LA CINEM�TICA
         * FINAL DE JONNO
         * */

        yield return null;
    }
}
