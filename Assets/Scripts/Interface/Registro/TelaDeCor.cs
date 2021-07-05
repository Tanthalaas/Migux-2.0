using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaDeCor : MonoBehaviour
{
    [SerializeField] SeletorDeCor seletor1, seletor2;
    private void FixedUpdate() 
    {
        Color cor1 = seletor1.GetCor();
        Color cor2 = seletor2.GetCor();

        cor1.a = 1f;
        cor2.a = 1f;

        Selecao.Instance.SetCor1(cor1);
        Selecao.Instance.SetCor2(cor2);
    }
}
