using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemManager : MonoBehaviour
{
    [SerializeField]
    int gemNumber;
    public List<GameObject> vases;
    [SerializeField]List<GameObject> gemVases;

    // Start is called before the first frame update
    void Start()
    {
        GemAssignment();
    }

    private void GemAssignment()
    {
        gemVases = new List<GameObject>(gemNumber);

        for (int i = 0; i < gemNumber; i++)
        {
            int Rand = Random.Range(0, vases.Count);
            vases[Rand].GetComponent<Breakable>().hasGem = true;
            gemVases.Add(vases[Rand]);
            vases.RemoveAt(Rand);
        }
    }
}
