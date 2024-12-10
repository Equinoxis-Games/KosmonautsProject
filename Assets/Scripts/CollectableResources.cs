using UnityEngine;

public class CollectableResources : MonoBehaviour
{
    [SerializeField] int ObjectNumber;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();

            switch (ObjectNumber)
            {
                case 0:
                    playerStats.SetAmmo(15);
                    break;
                case 1:
                    playerStats.SetOxygen(20);
                    break;
                case 2:
                    playerStats.SetHealth(20);
                    break;
                case 3:
                    playerStats.SetShield(25);
                    break;
                case 4:
                    playerStats.GetComponent<Movement>().SpeedBoost();
                    break;

            }

            transform.parent.GetComponent<GenerateResource>().StartCorroutineTimer();

            Destroy(gameObject);
        }
    }

    public void SetType(int e)
    {
        ObjectNumber = e;

        switch (ObjectNumber)
        {
            case 0:
                GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 3:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 4:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;

        }
    }
}
