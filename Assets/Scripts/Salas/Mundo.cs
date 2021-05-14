using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mundo : MonoBehaviour
{
    [SerializeField] Sala salaInicial;
    private void Start() {
        TrocaSalas.SetSalaAtual(salaInicial);
    }
}
