using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_JonnoExit : MonoBehaviour
{
    private Camera Camera;
    private GameObject Jonno;
    public void Interact(Camera camera, GameObject jonno)
    {
        Camera = camera;
        Jonno = jonno;
        StartCoroutine("CloseView", Camera);
    }

    IEnumerator CloseView(Camera camera)
    {
        float OrigFOV = 60f;
        camera.DOFieldOfView(0, 1f);
        
        /*
         * AQU� VA EL C�DIGO PARA 
         * CUANDO EMPIEZA LA CINEM�TICA
         * FINAL DE JONNO
         * */

        yield return null;
    }
}
