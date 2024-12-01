using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float recoil;
    [SerializeField] bool playerHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerHit)
        {
            if (collision.tag == "Player")
            {
                Vector2 direction = collision.transform.position - transform.position;
                direction.Normalize();

                collision.GetComponent<Movement>().AnadirImpulso(direction, recoil, 0.5f);
            }
        }
        else
        {
            if (collision.tag == "Enemy")
            {
                Vector2 direction = collision.transform.position - transform.position;
                direction.Normalize();

                collision.GetComponent<Movement>().AnadirImpulso(direction, recoil, 0.5f);
            }
        }

        if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
