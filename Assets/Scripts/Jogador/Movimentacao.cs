using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movimentacao : MonoBehaviour
{
    public static Movimentacao Instance;
    [SerializeField] Personagem personagem;
    [SerializeField] float velocidade;
    [SerializeField] float velocidadeDeSkate;
    bool andando, podeAndar;
    [SerializeField] bool usandoSkate;
    TrocaSalas trocadorDeSalasClicado;
    [SerializeField] float multiplicadorDeVelocidadeAtual;


    private void Start() {
        Instance = this;
    }

    public void BloquearMovimentacao()
    {
        podeAndar = false;
    }

    void LateUpdate()
    {
        DetectarTrocadorDeSalas();
        DetectarColisao();
        AndarAoClicar();
        OlharProMouse();
    }

    void DetectarTrocadorDeSalas()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.CircleCast(position, 0.2f, Vector2.up, 0.1f);
            if(hit)
            {
                TrocaSalas trocador;
                if(hit.collider.CompareTag("Troca Salas") && hit.collider.TryGetComponent<TrocaSalas>(out trocador))
                {
                    trocadorDeSalasClicado = trocador;
                }else{
                    trocadorDeSalasClicado = null;
                }
            }else{
                trocadorDeSalasClicado = null;
            }
        }
    }

    void DetectarColisao()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.CircleCast(mousePos, 0.1f, Vector2.up, 0.05f);
            if(hit2D.collider)
            {
                if(hit2D.collider.CompareTag("Colisao"))
                {
                    podeAndar = false;
                }
            }
        }
    }

    void AndarAoClicar()
    {
        if(Input.GetMouseButtonDown(0) && podeAndar && !Transicao.Instance.EmTransicao())
        {
            OlharProMouse(true);
            andando = true;
            Vector2 destino = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.DOKill();
            if(!usandoSkate)
            {
                transform
                    .DOMove(destino, Vector2.Distance(destino, transform.position) / velocidade / multiplicadorDeVelocidadeAtual)
                    .SetUpdate(true)
                    .SetEase(Ease.Linear)
                    .OnComplete(OnAndarComplete);
            } 
            else 
            {
                transform
                    .DOMove(destino, Vector2.Distance(destino, transform.position) / velocidadeDeSkate / multiplicadorDeVelocidadeAtual)
                    .SetUpdate(true)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(OnAndarComplete);
            }
            Conexao.Instance.EnviarMovimentacao(transform.position, destino);
        }
        podeAndar = true;
    }

    void OnAndarComplete()
    {
        andando = false;
        if(trocadorDeSalasClicado)
        {
            trocadorDeSalasClicado.Trocar();
        }
    }

    void OlharProMouse(bool forcar = false)
    {
        if(!andando || forcar)
        {
            //TODO: Lógica de olhar para o mouse
            Vector2 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            personagem.OlharParaPonto(posicaoMouse);
        }
    }

    public void SetMultiplicadorDeVelocidade(float multiplicador) 
    {
        multiplicadorDeVelocidadeAtual = multiplicador;
    }
}
