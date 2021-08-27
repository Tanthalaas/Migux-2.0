using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Personagem : MonoBehaviour
{
    [SerializeField] GameObject balaoDeMensagem;
    [SerializeField] SpriteRenderer balaoSprite;
    [SerializeField] TextMeshPro mensagemTexto, nomeTexto;
    public const float VELOCIDADE = 8, VELOCIDADENOSKATE = 10;
    public enum Criatura { AguaViva, CavaloMarinho, Estrela, Peixe, Polvo, Tubarao }

    [SerializeField] PersonagemBase aguaViva, cavaloMarinho, estrela, peixe, polvo, tubarao;
    PersonagemBase personagemSelecionado;
    [SerializeField] List<ChapeuNoPersonagem> chapeus = new List<ChapeuNoPersonagem>();
    bool filtrouChapeusInativos = false;

    public void Iniciar()
    {
        personagemSelecionado.MostrarFrente();
    }

    public void SelecionarCriatura(int criatura)
    {
        PersonagemBase[] seletor = {tubarao, peixe, aguaViva, polvo, cavaloMarinho, estrela};

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

    public void SelecionarSexo(int sexo)
    {
        PersonagemBase.Sexo sexoSelecionado = 
            sexo == 0 
            ? 
            PersonagemBase.Sexo.Masculino 
            : 
            PersonagemBase.Sexo.Feminino;
    
        personagemSelecionado.SelecionarSexo(sexoSelecionado);
    }

    public void SelecionarNome(string nome)
    {
        nomeTexto.text = nome;
    }

    public void SelecionarChapeu(int id)
    {
        Debug.Log($"adicionando chapeu {id}");
        RemoverChapeusInativos();
        foreach(ChapeuNoPersonagem chapeu in chapeus)
        {
            chapeu.MostrarChapeu(id);
        }
    }

    void RemoverChapeusInativos()
    {
        List<ChapeuNoPersonagem> chapeusFiltrados = new List<ChapeuNoPersonagem>();
        foreach(ChapeuNoPersonagem chapeu in chapeus)
        {
            if(chapeu) 
            {
                chapeusFiltrados.Add(chapeu);
            }
        }
        chapeus = new List<ChapeuNoPersonagem>(chapeusFiltrados);
    }

    public void EsconderChapeus()
    {
        foreach(ChapeuNoPersonagem chapeu in chapeus)
        {
            chapeu.EsconderChapeus();
        }
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
        
        StartCoroutine(AdaptarBalao());
        StartCoroutine(EsperarEOcultarBalao());
    }

    IEnumerator AdaptarBalao()
    {
        yield return new WaitForEndOfFrame();
        float alturaDoTexto = mensagemTexto.mesh.bounds.extents.y;
        balaoDeMensagem.transform.localPosition = new Vector3(0f, 1.6f + alturaDoTexto/1.5f, 0f);
        balaoSprite.size = new Vector2(10f, 10f + alturaDoTexto * 2f);
    }

    IEnumerator EsperarEOcultarBalao()
    {
        yield return new WaitForSecondsRealtime(5);
        balaoDeMensagem.SetActive(false);
    }

    

    public void DefinirEscala(float escala)
    {
        transform.localScale = Vector3.one * escala;
        balaoDeMensagem.transform.localScale = Vector3.one * (1/escala) * 0.3f;
        nomeTexto.transform.localScale = Vector3.one * (1/escala) * 0.5f;
    }
}
