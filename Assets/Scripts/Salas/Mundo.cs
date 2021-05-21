using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mundo : MonoBehaviour
{
    public static Mundo MundoAtual;
    [SerializeField] Sala salaInicial;
    [SerializeField] AudioSource tocadorDeFundo;
    private void Start() {
        TrocaSalas.SetSalaAtual(salaInicial);
        MundoAtual = this;
    }

    public void TocarFundo(AudioClip clip)
    {
        if(tocadorDeFundo.clip != clip)
        {
            tocadorDeFundo.clip = clip;
            IniciarMusicaDeFundo();
        }
    }

    public void IniciarMusicaDeFundo() 
    {
        tocadorDeFundo.Play();
    }
}
