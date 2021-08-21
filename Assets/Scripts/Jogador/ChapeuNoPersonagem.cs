using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapeuNoPersonagem : MonoBehaviour
{
    [SerializeField] List<GameObject> chapeus = new List<GameObject>();

    public void MostrarChapeu(int id)
    {
        EsconderChapeus();
        chapeus[id].SetActive(true);
    }

    public void EsconderChapeus()
    {
        foreach(GameObject chapeu in chapeus)
        {
            chapeu.SetActive(false);
        }
    }
}
