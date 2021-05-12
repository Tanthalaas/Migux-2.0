using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Transicao : MonoBehaviour
{
    public static Transicao Instance;
    [SerializeField] GameObject mascara;
    [SerializeField] TextMeshPro textoCarregamento;
    [SerializeField] float tempoDeTransicao;
    bool falsaTransicao;
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
        this.finalDaTransicaoFalsa = finalDaTransicaoFalsa;

        mascara.transform
            .DOScale(Vector3.zero, tempoDeTransicao)
            .SetUpdate(true)
            .OnComplete(aoTransicionar);
    }

    public void IniciarFalsoCarregamento()
    {
        textoCarregamento.gameObject.SetActive(true);
        falsaTransicao = true;
        porcentagem = 0;
    }

    private void FixedUpdate() {
        textoCarregamento.text = $"{Mathf.CeilToInt(porcentagem)}%";
        if(falsaTransicao)
        {
            porcentagem += 5f;
        }
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
            .SetUpdate(true);
    }
}
