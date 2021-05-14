using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Botao : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    Vector2 mouseOffset;

    public virtual void Clicar()
    {

    }

    public void ComecarArrastamento()
    {
        mouseOffset = PosicaoDoMouse() - rectTransform.anchoredPosition;
    }

    public void Arrastar()
    {
        rectTransform.anchoredPosition = PosicaoDoMouse() - mouseOffset;
    }

    Vector2 PosicaoDoMouse()
    {
        return new Vector2(Input.mousePosition.x, -(Screen.height - Input.mousePosition.y));
    }
}
