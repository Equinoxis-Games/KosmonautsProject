using UnityEngine;
using System.Collections;

public class GenerateResource : MonoBehaviour
{
    [SerializeField] float delayTime = 20f;
    [SerializeField] GameObject resourceObj;

    void Awake()
    {
        GenerateObj();
    }

    private void GenerateObj()
    {
        GameObject resource = Instantiate(resourceObj, transform);

        int rnd = Random.Range(0, 5);
        resource.GetComponent<CollectableResources>().SetType(rnd);
    }

    public void StartCorroutineTimer()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(delayTime);

        GenerateObj();
    }
}
