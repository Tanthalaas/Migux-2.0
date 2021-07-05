using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TelaDeNome : MonoBehaviour
{
    [SerializeField] TMP_InputField campoDeNome;

    public void ValidarNome()
    {
        string nome = campoDeNome.text.Trim();
        if(nome != "")
        {
            Selecao.Instance.SetNome(nome);
        }
    }
}
