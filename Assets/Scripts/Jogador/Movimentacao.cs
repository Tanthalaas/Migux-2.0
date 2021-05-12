using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movimentacao : MonoBehaviour
{
    [SerializeField] Personagem personagem;
    [SerializeField] float velocidade;
    [SerializeField] float velocidadeDeSkate;
    bool andando;
    [SerializeField] bool usandoSkate;
    TrocaSalas trocadorDeSalasClicado;
    [SerializeField] float multiplicadorDeVelocidadeAtual;


    void Update()
    {
        DetectarTrocadorDeSalas();
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

    void AndarAoClicar()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OlharProMouse(true);
            andando = true;
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.DOKill();
            if(!usandoSkate)
            {
                transform
                    .DOMove(position, Vector2.Distance(position, transform.position) / velocidade / multiplicadorDeVelocidadeAtual)
                    .SetUpdate(true)
                    .SetEase(Ease.Linear)
                    .OnComplete(OnAndarComplete);
            } 
            else 
            {
                transform
                    .DOMove(position, Vector2.Distance(position, transform.position) / velocidadeDeSkate / multiplicadorDeVelocidadeAtual)
                    .SetUpdate(true)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(OnAndarComplete);
            }
        }
    }

    void OnAndarComplete()
    {
        andando = false;
        if(trocadorDeSalasClicado)
        {
            trocadorDeSalasClicado.Trocar(this);
        }
    }

    void OlharProMouse(bool forcar = false)
    {
        if(!andando || forcar)
        {
            //TODO: Lógica de olhar para o mouse
            Vector2 direcaoDoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angulo = Vector2.SignedAngle(Vector2.right, direcaoDoMouse);

            if(angulo >= 135f)
            {
                personagem.MostrarLado(PersonagemBase.Direcao.Esquerda);
            } 
            else if(angulo >= 90f) 
            {
                personagem.MostrarCima(PersonagemBase.Direcao.Esquerda);
            }
            else if(angulo >= 45f)
            {
                personagem.MostrarCima(PersonagemBase.Direcao.Direita);
            }
            else if(angulo >= -36f)
            {
                personagem.MostrarLado(PersonagemBase.Direcao.Direita);
            }
            else if(angulo >= -72f)
            {
                personagem.MostrarDiagonal(PersonagemBase.Direcao.Direita);
            }
            else if(angulo >= -108f)
            {
                personagem.MostrarFrente();
            }
            else if(angulo >= -144f)
            {
                personagem.MostrarDiagonal(PersonagemBase.Direcao.Esquerda);
            }
            else 
            {
                personagem.MostrarLado(PersonagemBase.Direcao.Esquerda);
            }
        }
    }

    public void SetMultiplicadorDeVelocidade(float multiplicador) 
    {
        multiplicadorDeVelocidadeAtual = multiplicador;
    }
}
