using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selecao : MonoBehaviour
{
    public static Selecao Instance;
    [SerializeField] GameObject telaDeCriatura, telaDeCor, telaDeGenero, telaDeNome;
    
    int criaturaAtual = 0;
    Color cor1, cor2;
    PersonagemBase.Sexo sexo = PersonagemBase.Sexo.Masculino;
    string nome;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetCriatura(int criatura)
    {
        criaturaAtual = criatura;
        if(criaturaAtual > 5)
        {
            criaturaAtual = 5;
        }else if(criaturaAtual < 0)
        {
            criaturaAtual = 0;
        }
    }

    public int CriaturaAtual() => criaturaAtual;

    public void SetCor1(Color color) => cor1 = color;
    public void SetCor2(Color color) => cor2 = color;
    public void SetGenero(PersonagemBase.Sexo sexo) => this.sexo = sexo;
    public void SetNome(string nome) 
    {
        this.nome = nome;
        telaDeNome.SetActive(false);
        TerminarRegistro();
    }

    void TerminarRegistro()
    {
        SceneManager.LoadScene(1);
    }

    public Color GetCor1() => cor1;
    public Color GetCor2() => cor2;
    public PersonagemBase.Sexo GetSexo() => sexo;
    public string GetNome() => nome;
    

    public void TelaDeCriatura()
    {
        telaDeCriatura.SetActive(true);
        telaDeCor.SetActive(false);
        telaDeGenero.SetActive(false);
        telaDeNome.SetActive(false);
    }

    public void TelaDeCor()
    {
        telaDeCriatura.SetActive(false);
        telaDeCor.SetActive(true);
        telaDeGenero.SetActive(false);
        telaDeNome.SetActive(false);
    }

    public void TelaDeGenero()
    {
        telaDeCriatura.SetActive(false);
        telaDeCor.SetActive(false);
        telaDeGenero.SetActive(true);
        telaDeNome.SetActive(false);
    }

    public void TelaDeNome()
    {
        telaDeCriatura.SetActive(false);
        telaDeCor.SetActive(false);
        telaDeGenero.SetActive(false);
        telaDeNome.SetActive(true);
    }
}
