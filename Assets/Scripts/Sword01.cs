using UnityEngine;
using System.Collections;

public class Sword01 : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    GameObject sword;

    private PlayerStats playerStats;

    void Start()
    {
        sword = transform.GetChild(0).gameObject;
        playerStats = transform.parent.GetComponent<PlayerStats>();
    }

    public void Slash()
    {
        if (!playerStats.GetAction())
        {
            StartCoroutine(StartSlash());
            playerStats.SetAction(0.5f);
        }
    }

    IEnumerator StartSlash()
    {
        weapon.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        sword.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        sword.SetActive(false);
        weapon.SetActive(true);
    }
}
