using System;

public class MensagemModel: Id
{
    public string texto;

    public MensagemModel(string mensagem)
    {
        texto = mensagem;
    }
}