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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AndarAoClicar();
        OlharProMouse();
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
                    .DOMove(position, Vector2.Distance(position, transform.position) / velocidade)
                    .SetUpdate(true)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => andando = false);
            } 
            else 
            {
                transform
                    .DOMove(position, Vector2.Distance(position, transform.position) / velocidadeDeSkate)
                    .SetUpdate(true)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(() => andando = false);
            }
        }
    }

    void OlharProMouse(bool forcar = false)
    {
        if(!andando || forcar)
        {
            //TODO: Lógica de olhar para o mouse
            Vector2 direcaoDoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angulo = Vector2.SignedAngle(Vector2.right, direcaoDoMouse);
            Debug.Log(angulo);

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
}
