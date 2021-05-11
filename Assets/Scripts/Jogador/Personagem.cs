using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    public enum Criatura { AguaViva, CavaloMarinho, Estrela, Peixe, Polvo, Tubarao }

    [SerializeField] PersonagemBase aguaViva, cavaloMarinho, estrela, peixe, polvo, tubarao;
    PersonagemBase personagemSelecionado;

    [Header("Dados iniciais")]
    [SerializeField] Color corInicial1, corInicial2;
    [SerializeField] PersonagemBase.Sexo sexoInicial;
    [SerializeField] Criatura criaturaInicial;

    private void Start() 
    {
        SelecionarCriatura(criaturaInicial);
        SelecionarSexo(sexoInicial);
        SelecionarCores(corInicial1, corInicial2);
    }

    public void SelecionarCriatura(Criatura criatura)
    {
        switch(criatura) 
        {
            case Criatura.AguaViva:
                personagemSelecionado = aguaViva;
                break;
            case Criatura.CavaloMarinho:
                personagemSelecionado = cavaloMarinho;
                break;
            case Criatura.Estrela:
                personagemSelecionado = estrela;
                break;
            case Criatura.Peixe:
                personagemSelecionado = peixe;
                break;
            case Criatura.Polvo:
                personagemSelecionado = polvo;
                break;
            case Criatura.Tubarao:
                personagemSelecionado = tubarao;
                break;
        }

        if(personagemSelecionado != aguaViva) Destroy(aguaViva.gameObject);
        if(personagemSelecionado != cavaloMarinho) Destroy(cavaloMarinho.gameObject);
        if(personagemSelecionado != estrela) Destroy(estrela.gameObject);
        if(personagemSelecionado != peixe) Destroy(peixe.gameObject);
        if(personagemSelecionado != polvo) Destroy(polvo.gameObject);
        if(personagemSelecionado != tubarao) Destroy(tubarao.gameObject);
    }

    public void SelecionarCores(Color corPrimaria, Color corSecundaria)
    {
        personagemSelecionado.SelecionarCores(corPrimaria, corSecundaria);
    }

    public void SelecionarSexo(PersonagemBase.Sexo sexo)
    {
        personagemSelecionado.SelecionarSexo(sexo);
    }

    public void MostrarFrente()
    {
        personagemSelecionado.MostrarFrente();
    }

    public void MostrarCima(PersonagemBase.Direcao direcao)
    {
       personagemSelecionado.MostrarCima(direcao);
    }

    public void MostrarLado(PersonagemBase.Direcao direcao)
    {
        personagemSelecionado.MostrarLado(direcao);
    }

    public void MostrarDiagonal(PersonagemBase.Direcao direcao)
    {
        personagemSelecionado.MostrarDiagonal(direcao);
    }
}
