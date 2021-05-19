using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    public const float VELOCIDADE = 8, VELOCIDADENOSKATE = 10;
    public enum Criatura { AguaViva, CavaloMarinho, Estrela, Peixe, Polvo, Tubarao }

    [SerializeField] PersonagemBase aguaViva, cavaloMarinho, estrela, peixe, polvo, tubarao;
    PersonagemBase personagemSelecionado;

    [Header("Dados iniciais")]
    [SerializeField] Color corInicial1, corInicial2;
    [SerializeField] PersonagemBase.Sexo sexoInicial;
    [SerializeField] Criatura criaturaInicial;

    public void SelecionarCriatura(string criatura)
    {
        Dictionary<string, PersonagemBase> seletor = new Dictionary<string, PersonagemBase>();

        seletor.Add("Agua Viva", aguaViva);
        seletor.Add("Cavalo Marinho", cavaloMarinho);
        seletor.Add("Estrela", estrela);
        seletor.Add("Peixe", peixe);
        seletor.Add("Polvo", polvo);
        seletor.Add("Tubarao", tubarao);

        personagemSelecionado = seletor[criatura];

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

    public void SelecionarSexo(string sexo)
    {
        PersonagemBase.Sexo sexoSelecionado = 
            sexo == "Masculino" 
            ? 
            PersonagemBase.Sexo.Masculino 
            : 
            PersonagemBase.Sexo.Feminino;
    
        personagemSelecionado.SelecionarSexo(sexoSelecionado);
    }

    public void MostrarFrente()
    {
        if(personagemSelecionado) personagemSelecionado.MostrarFrente();
    }

    public void MostrarCima(PersonagemBase.Direcao direcao)
    {
        if(personagemSelecionado) personagemSelecionado.MostrarCima(direcao);
    }

    public void MostrarLado(PersonagemBase.Direcao direcao)
    {
        if(personagemSelecionado) personagemSelecionado.MostrarLado(direcao);
    }

    public void MostrarDiagonal(PersonagemBase.Direcao direcao)
    {
        if(personagemSelecionado) personagemSelecionado.MostrarDiagonal(direcao);
    }
}
