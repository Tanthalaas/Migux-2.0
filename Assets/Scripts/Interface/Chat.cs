using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chat : BotaoComFilho
{
    [SerializeField] TMP_InputField inputField;
    
    public override void Clicar()
    {
        if(filho.activeSelf && inputField.text.Trim() != "")
        {
            EnviarMensagem();
            return;
        }

        base.Clicar();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            EnviarMensagem();
        }
    }

    void EnviarMensagem()
    {
        string mensagem = inputField.text.Trim();
        if(mensagem != "")
        {
            Conexao.Instance.GetJogadorLocal().MostrarMensagem(mensagem);
            Conexao.Instance.EnviarMensagem(mensagem);
            inputField.text = "";
        }
    }
}
