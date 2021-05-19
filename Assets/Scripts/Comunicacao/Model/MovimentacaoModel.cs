using System;
using UnityEngine;

[Serializable]
public class MovimentacaoModel: Id
{
    public Vector2 origem, destino;

    public MovimentacaoModel(Vector2 origem, Vector2 destino)
    {
        this.origem = origem;
        this.destino = destino;
        id = "";
    }
}