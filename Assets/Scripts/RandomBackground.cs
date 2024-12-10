using UnityEngine;
using System.Collections.Generic;

public class RandomBackground : MonoBehaviour
{
    public List<Sprite> fondos01;
    public List<Sprite> fondos02;
    public List<Sprite> fondos03;
    public List<Sprite> fondos04;

    private void Awake()
    {
        int randomNum = Random.Range(0, 4);

        GetComponent<SpriteRenderer>().sprite = fondos01[randomNum];
    }
}
