using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetData
{
    public float[] posicao;
    public int vida;

    public PlayerGetData(PlayerBehaviour player)
    {
        posicao = new float[3];
        posicao[0] = player.transform.position.x;
        posicao[1] = player.transform.position.y;
        posicao[2] = player.transform.position.z;

        vida = player.vida;
    }
}
