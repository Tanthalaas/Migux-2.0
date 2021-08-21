using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletorDeChapeu : MonoBehaviour
{
    [SerializeField] Chapeu.ChapeuId chapeu;
    public void SelecionarChapeu()
    {
        Conexao.Instance.GetJogadorLocal().SelecionarChapeu((int)chapeu);
        Conexao.Instance.EnviarChapeu((int)chapeu, false);
    }

    public void DevolverChapeu()
    {
        Conexao.Instance.GetJogadorLocal().EsconderChapeus();
        Conexao.Instance.EnviarChapeu(0, true);
    }

    private void OnMouseUpAsButton() {
        SelecionarChapeu();
    }
}
