using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaDeGenero : MonoBehaviour
{
    public void SetMasculino() => Selecao.Instance.SetGenero(PersonagemBase.Sexo.Masculino);
    public void SetFeminino() => Selecao.Instance.SetGenero(PersonagemBase.Sexo.Feminino);
}
