using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movimentacao : MonoBehaviour
{
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
            } else {
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
        }
    }
}
