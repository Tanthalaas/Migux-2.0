using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemBase : MonoBehaviour
{
    public enum Direcao { Esquerda, Direita }
    public enum Sexo { Masculino, Feminino }
    [SerializeField] GameObject frente, cima, lado, diagonal;
    [SerializeField] List<SpriteRenderer> coresPrimarias = new List<SpriteRenderer>();
    [SerializeField] List<SpriteRenderer> coresSecundarias = new List<SpriteRenderer>();
    [SerializeField] List<GameObject> rostosHomem = new List<GameObject>();
    [SerializeField] List<GameObject> rostosMulher = new List<GameObject>();

    public void SelecionarCores(Color corPrimaria, Color corSecundaria)
    {
        foreach(SpriteRenderer spriteCorPrimaria in coresPrimarias)
        {
            spriteCorPrimaria.color = corPrimaria;
        }
        foreach(SpriteRenderer spriteCorSecundaria in coresSecundarias)
        {
            spriteCorSecundaria.color = corSecundaria;
        }
    }

    public void SelecionarSexo(Sexo sexo)
    {
        if(sexo == Sexo.Masculino)
        {
            foreach(GameObject rosto in rostosMulher)
            {
                Destroy(rosto);
            }
        } else {
            foreach(GameObject rosto in rostosHomem)
            {
                Destroy(rosto);
            }
        }
    }


    void Direcionar(GameObject objeto, Direcao direcao)
    {
        if(direcao == Direcao.Esquerda)
        {
            objeto.transform.localScale = new Vector3(-1f, 1f, 0f);
        } else {
            objeto.transform.localScale = new Vector3(1f, 1f, 0f);
        }
    }

    public void MostrarFrente()
    {
        frente.SetActive(true);
        cima.SetActive(false);
        lado.SetActive(false);
        diagonal.SetActive(false);
    }

    public void MostrarCima(Direcao direcao)
    {
        frente.SetActive(false);
        cima.SetActive(true);
        lado.SetActive(false);
        diagonal.SetActive(false);

        Direcionar(cima, direcao);
    }

    public void MostrarLado(Direcao direcao)
    {
        frente.SetActive(false);
        cima.SetActive(false);
        lado.SetActive(true);
        diagonal.SetActive(false);

        Direcionar(lado, direcao);
    }

    public void MostrarDiagonal(Direcao direcao)
    {
        frente.SetActive(false);
        cima.SetActive(false);
        lado.SetActive(false);
        diagonal.SetActive(true);

        Direcionar(diagonal, direcao);
    }
}
