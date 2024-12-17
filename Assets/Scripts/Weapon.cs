using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Movement playerMove;
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint;         // Punto de disparo
    private PlayerStats playerStats;

    [Header ("Mode 1")]
    [SerializeField] float coneAngle = 45f;   // Ángulo total del cono de disparo en grados
    [SerializeField] float bulletForce01 = 8f; // Fuerza con la que se disparan las balas
    [SerializeField] int bulletCount = 5;     // Número de balas disparadas
    [SerializeField] float reloadTimer01 = 2f;
    [SerializeField] float recoil01 = 300f;
    [Header("Mode 2")]
    [SerializeField] float bulletForce02 = 12f;
    [SerializeField] float reloadTimer02 = 1f;
    [SerializeField] float recoil02 = 200f;

    float reload = 0f;
    bool weaponMode = false;

    private void Start()
    {
        playerMove = transform.parent.GetComponent<Movement>();
        playerStats = transform.parent.GetComponent<PlayerStats>();
    }

    private void LateUpdate()
    {
        if (reload > 0)
        {
            reload -= Time.deltaTime;
        }
    }

    private void Shoot01()
    {
        int projectileRealCount = bulletCount;

        if (bulletCount > playerStats.GetAmmo()) projectileRealCount = playerStats.GetAmmo();

        if (bulletCount < 1) return;

        // Calcular el ángulo inicial (mitad del cono hacia la izquierda)
        float startAngle = -coneAngle / 2;
        float angleStep = coneAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            // Calcular el ángulo de cada bala
            float currentAngle = startAngle + angleStep * i;

            // Crear una rotación basada en el ángulo actual
            Quaternion bulletRotation = Quaternion.Euler(0, 0, firePoint.eulerAngles.z + currentAngle);

            // Instanciar la bala
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, bulletRotation);

            // Obtener el Rigidbody2D de la bala para aplicar fuerza
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Aplicar la fuerza en la dirección de la bala
                rb.AddForce(bulletRotation * Vector2.right * bulletForce01, ForceMode2D.Impulse);
            }
        }
    }

    private void Shoot02()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Quaternion bulletRotation = Quaternion.Euler(0, 0, firePoint.eulerAngles.z);

        // Obtener el Rigidbody2D de la bala para aplicarle velocidad
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Aplicar velocidad en la dirección del firePoint
            rb.AddForce(bulletRotation * Vector2.right * bulletForce02, ForceMode2D.Impulse);
        }
    }

    public void ChangeMode()
    {
        if (weaponMode)
        {
            weaponMode = false;
            reload = reloadTimer01;
        }
        else
        {
            weaponMode = true;
            reload = reloadTimer02;
        }
    }

    public void FireWepon()
    {
        if(reload <= 0 && playerStats.GetAmmo() > 0 && !playerStats.GetAction())
        {
            if (weaponMode)
            {
                Shoot02();
                reload = reloadTimer02;
                playerStats.SetAmmo(-1);

                Vector2 direction = playerMove.transform.position - transform.position;
                direction.Normalize();

                playerMove.AnadirImpulso(direction, recoil02, 0.5f);
            }
            else
            {
                Shoot01();
                reload = reloadTimer01;
                playerStats.SetAmmo(-bulletCount);

                Vector2 direction = playerMove.transform.position - transform.position;
                direction.Normalize();

                playerMove.AnadirImpulso(direction, recoil01, 0.5f);
            }

            playerStats.SetAction(0.5f);
        }
    }
}
