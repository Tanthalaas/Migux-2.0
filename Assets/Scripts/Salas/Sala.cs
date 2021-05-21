using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sala : MonoBehaviour
{
    [SerializeField] float escalaDoJogador = 1f;
    [SerializeField] float velocidade = 1f;
    [SerializeField] AudioClip musicaDeFundo;
    
    public float GetEscalaDoJogador() => escalaDoJogador;
    public float GetVelocidade() => velocidade;
    public AudioClip GetMusicaDeFundo() => musicaDeFundo;
}
