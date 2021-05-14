using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoComFilho : Botao
{
    [SerializeField] GameObject filho;
    
    public override void Clicar()
    {
        filho.SetActive(!filho.activeSelf);
        base.Clicar();
    }

    public void Fechar()
    {
        filho.SetActive(false);
    }
}
