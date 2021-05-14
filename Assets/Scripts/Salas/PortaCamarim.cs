using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaCamarim : TrocaSalas
{
    [SerializeField] GameObject portaAberta, portaFechada;
    private void OnMouseEnter() {
        portaAberta.SetActive(true);
        portaFechada.SetActive(false);
    }

    private void OnMouseExit() {
        portaAberta.SetActive(false);
        portaFechada.SetActive(true);
    }
}
