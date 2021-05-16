using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Transicao : MonoBehaviour
{
    public static Transicao Instance;
    [SerializeField] GameObject mascara, fundo;
    [SerializeField] TextMeshPro textoCarregamento;
    [SerializeField] float tempoDeTransicao;
    bool falsaTransicao, emTransicao;
    float porcentagem;
    Action finalDaTransicaoFalsa;

    private void Start() {
        if(Instance) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void Carregar(TweenCallback aoTransicionar, Action finalDaTransicaoFalsa = null)
    {
        if(emTransicao) return;

        this.finalDaTransicaoFalsa = finalDaTransicaoFalsa;

        fundo.SetActive(true);
        mascara.SetActive(true);
        SetEmTransicao(true);

        if(finalDaTransicaoFalsa == null) textoCarregamento.gameObject.SetActive(true);

        mascara.transform
            .DOScale(Vector3.zero, tempoDeTransicao)
            .SetUpdate(true)
            .OnComplete(aoTransicionar);
    }

    public void IniciarFalsoCarregamento()
    {
        falsaTransicao = true;
        porcentagem = 100;
    }

    private void FixedUpdate() {
        textoCarregamento.text = $"{Mathf.CeilToInt(porcentagem)}%";
        if(porcentagem >= 100f)
        {
            TerminarCarregamento();
        }
    }

    void TerminarCarregamento()
    {
        falsaTransicao = false;
        porcentagem = 0;

        textoCarregamento.gameObject.SetActive(false);

        if(finalDaTransicaoFalsa != null) finalDaTransicaoFalsa();

        mascara.transform
            .DOScale(Vector3.one * 1.1f, tempoDeTransicao)
            .SetUpdate(true)
            .OnComplete(AoTerminarAnimacaoDeTransicao);
    }

    void AoTerminarAnimacaoDeTransicao()
    {
        OcultarMascara();
        SetEmTransicao(false);
    }

    //Economia de recursos
    void OcultarMascara()
    {
        fundo.SetActive(false);
        mascara.SetActive(false);
    }

    public void SetEmTransicao(bool valor)
    {
        emTransicao = valor;
    }

    public bool EmTransicao() => emTransicao;
}
