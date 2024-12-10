using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int shield;
    [SerializeField] float oxygen;
    [SerializeField] int ammo;

    [Header ("UI")]
    public TextMeshProUGUI ammoTxt;
    public Image oxygenImg;
    public TextMeshProUGUI oxygenTxt;
    public TextMeshProUGUI healthTxt;
    public TextMeshProUGUI shieldTxt;
    public Image healthImg;
    public Image shieldImg;

    private void Awake()
    {
        ammoTxt.text = ammo.ToString();
        healthTxt.text = health.ToString();
        shieldTxt.text = shield.ToString();

        healthImg.fillAmount = health / 100f;
        shieldImg.fillAmount = shield / 100f;
    }

    public void RecieveDamage(int dmg)
    {
        if(shield > 0)
        {
            int restDmg = dmg - shield;

            SetShield(-dmg);

            if (restDmg > 0) dmg = restDmg;
            else
            {
                return;
            }
        }

        SetHealth(-dmg);
    }

    public void SetHealth(int e)
    {
        health += e;

        if (health < 0)
        {
            health = 0;
        }
        else if(health > 100)
        {
            health = 100;
        }

        healthImg.fillAmount = health / 100f;
        healthTxt.text = health.ToString();
    }

    public void SetShield(int e)
    {
        shield += e;

        if(shield < 0)
        {
            shield = 0;
        }
        else if (shield > 100)
        {
            shield = 100;
        }

        shieldImg.fillAmount = shield / 100f;
        shieldTxt.text = shield.ToString();
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public void SetAmmo(int e)
    {
        ammo += e;

        if (ammo < 0) ammo = 0;

        ammoTxt.text = ammo.ToString();
    }

    public float GetOxygen()
    {
        return oxygen;
    }

    public void SetOxygen(float e)
    {
        oxygen += e;

        if (oxygen < 0) oxygen = 0;

        oxygenImg.fillAmount = oxygen / 100f;

        oxygenTxt.text = (int)oxygen + "%";
    }
}
