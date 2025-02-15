using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TurningPipes : PuzzleFather2
{ 
    Hud hud;
    [SerializeField] GameObject[] Owners;
    Image[] RotatingPipes;
    Pipes[] pipes;
    [SerializeField] Color[] Colors;
    private int index = 0;
    bool _isUsing = false;

    private void Awake()
    {
        RotatingPipes = new Image[Owners.Length];
        pipes = new Pipes[Owners.Length];
        int j = 0;
        foreach(GameObject SoCalled in Owners)
        {
            RotatingPipes[j] = SoCalled.transform.GetComponent<Image>();
            j++;
        }
        int i = 0;

        foreach (GameObject pipe in Owners)
        {
            pipes[i] = pipe.GetComponent<Pipes>();
            i++;
        }
    }
    public override void Interact()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
        hud.activado = true;
        this.PuzzleHolder.SetActive(true);
        _isUsing = true;
        RotatingPipes[index].color = Color.red;
    }

    public override void Exit()
    {
        hud.activado = false;
        _isUsing = false;
        this.PuzzleHolder.SetActive(false);
    }

    [PunRPC]
    private void ChangePipe(int dir)
    {
            RotatingPipes[index].color = Color.white;
            if(dir < 0)
            {
                if(index > 0)
                {
                    index--;
                }
            }
            else
            {
                if (index < RotatingPipes.Length-1) 
                {
                    index++;
                }
            }
            RotatingPipes[index].color = Color.red;

    }

    [PunRPC]
    void RPC_RotatePipe()
    {
        pipes[index].Rotate();
    }

    private void Update()
    {
        if(_isUsing)
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                photonView.RPC("ChangePipe", RpcTarget.All, 1);
            }
            else if(Input.GetKeyDown(KeyCode.A)) 
            {
                photonView.RPC("ChangePipe", RpcTarget.All, -1);
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                photonView.RPC("RPC_RotatePipe", RpcTarget.All);
            }
        }
    }

}
