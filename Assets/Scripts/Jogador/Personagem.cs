using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Personagem : MonoBehaviour
{
    [SerializeField] GameObject balaoDeMensagem;
    [SerializeField] TextMeshPro mensagemTexto;
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

    void MostrarFrente()
    {
        if(personagemSelecionado) personagemSelecionado.MostrarFrente();
    }

    void MostrarCima(PersonagemBase.Direcao direcao)
    {
        if(personagemSelecionado) personagemSelecionado.MostrarCima(direcao);
    }

    void MostrarLado(PersonagemBase.Direcao direcao)
    {
        if(personagemSelecionado) personagemSelecionado.MostrarLado(direcao);
    }

    void MostrarDiagonal(PersonagemBase.Direcao direcao)
    {
        if(personagemSelecionado) personagemSelecionado.MostrarDiagonal(direcao);
    }

    public void OlharParaPonto(Vector3 ponto)
    {
        Vector2 direcao = ponto - transform.position;
        float angulo = Vector2.SignedAngle(Vector2.right, direcao);

        if(angulo >= 135f)
        {
            MostrarLado(PersonagemBase.Direcao.Esquerda);
        } 
        else if(angulo >= 90f) 
        {
            MostrarCima(PersonagemBase.Direcao.Esquerda);
        }
        else if(angulo >= 45f)
        {
            MostrarCima(PersonagemBase.Direcao.Direita);
        }
        else if(angulo >= -36f)
        {
            MostrarLado(PersonagemBase.Direcao.Direita);
        }
        else if(angulo >= -72f)
        {
            MostrarDiagonal(PersonagemBase.Direcao.Direita);
        }
        else if(angulo >= -108f)
        {
            MostrarFrente();
        }
        else if(angulo >= -144f)
        {
            MostrarDiagonal(PersonagemBase.Direcao.Esquerda);
        }
        else 
        {
            MostrarLado(PersonagemBase.Direcao.Esquerda);
        }
    }

    [ContextMenu("Teste mensagem")]
    public void TesteMensagem()
    {
        MostrarMensagem("Com este commit, o módulo de recursão paralela deletou todas as entradas do nosso servidor de DNS.");
    }

    public void MostrarMensagem(string mensagem)
    {
        StopAllCoroutines();
        balaoDeMensagem.SetActive(true);
        mensagemTexto.text = mensagem;
        StartCoroutine(EsperarEOcultarBalao());
    }

    IEnumerator EsperarEOcultarBalao()
    {
        yield return new WaitForSecondsRealtime(5);
        balaoDeMensagem.SetActive(false);
    }
}
