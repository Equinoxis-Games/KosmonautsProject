using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    private Movement playerMove;
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint;         // Punto de disparo
    private PlayerStats playerStats;

    public float coneAngle = 45f;       // Ángulo total del cono en grados
    public float projectileSpeed = 10f; // Velocidad de los proyectiles
    public int projectileCount = 4;     // Cantidad de proyectiles
    public float reloadTimer = 2f;
    public float recoil = 300f;

    private float reload = 0f;

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

    public void Shoot()
    {
        int projectileRealCount = projectileCount;

        if (projectileCount > playerStats.GetAmmo()) projectileRealCount = playerStats.GetAmmo();

        for (int i = 0; i < projectileRealCount; i++)
        {
            // Generar un ángulo aleatorio dentro del rango del cono
            float randomAngle = Random.Range(-coneAngle / 2, coneAngle / 2);

            // Calcular la rotación para el proyectil
            Quaternion rotation = Quaternion.Euler(0, 0, firePoint.eulerAngles.z + randomAngle);

            // Instanciar el proyectil en la posición del firePoint
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);

            // Aplicar velocidad al proyectil
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = rotation * Vector2.right * projectileSpeed;
            }
        }
    }

    public void FireWepon()
    {
        if(reload <= 0 && playerStats.GetAmmo() > 0)
        {
            Shoot();
            reload += reloadTimer;

            playerStats.SetAmmo(-projectileCount);

            Vector2 direction = playerMove.transform.position - transform.position;
            direction.Normalize();

            playerMove.AnadirImpulso(direction, recoil, 0.5f);
        }
    }
}
