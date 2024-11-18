using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Movement : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxSpeed = 10f;
    public float rotationSpeed = 10f;
    public float oxygenPercent = 100f;

    private float maxSpeedPlus = 1f;
    private Rigidbody2D rb;
    private float timeStuned = 0f;
    private float maxSpeedTime = 0f;
    private float targetAngle = 0f;
    private bool oxigenImpulse = false;
    private Vector2 currentVelocity = Vector2.zero;

    public Joystick joystick;
    public SpriteRenderer playerSprite;
    public Image oxygenImg;
    public TextMeshProUGUI oxygenTxt;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        HandleInput();

        //LimitVelocity();

        PlayerRotation();
    }

    void HandleInput()
    {
        /*if (!oxigenImpulse) return;

        if (timeStuned > 0)
        {
            timeStuned -= 1 * Time.deltaTime;
            currentVelocity = rb.linearVelocity;

            return;
        }*/

        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector2 inputDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (inputDirection.magnitude > 0)
        {
            currentVelocity = Vector2.Lerp(currentVelocity, inputDirection * maxSpeed * maxSpeedPlus, acceleration * Time.deltaTime);

            UseOxygen();
        }

        rb.linearVelocity = currentVelocity;
    }

    void LimitVelocity()
    {
        if (!oxigenImpulse) return;

        if (maxSpeedTime > 0)
        {
            if (maxSpeedPlus != 1.5f) maxSpeedPlus = 1.5f;
            maxSpeedTime -= 1 * Time.deltaTime;
        }
        else
        {
            if (maxSpeedPlus != 1f) maxSpeedPlus = 1f;
        }

        if (rb.linearVelocity.magnitude > (maxSpeed * maxSpeedPlus))
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void UseOxygen()
    {
        oxygenPercent -= 0.5f * Time.deltaTime;

        oxygenImg.fillAmount = oxygenPercent / 100f;

        oxygenTxt.text = (int)oxygenPercent + "%";
    }

    void PlayerRotation()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        if (Mathf.Abs(horizontalInput) > 0.4f || Mathf.Abs(verticalInput) > 0.4f)
        {
            targetAngle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed * Time.deltaTime);

        /*if(Mathf.Abs(transform.rotation.z) > 0.55f)
        {
            playerSprite.flipY = true;
        }
        else if(Mathf.Abs(transform.rotation.z) < 0.45f)
        {
            playerSprite.flipY = false;
        }*/
    }

    public void AnadirImpulso(Vector2 direction, float fuerza, float time)
    {
        rb.AddForce(direction * fuerza);
        currentVelocity = rb.linearVelocity;

        timeStuned = time;
    }

    public void MejoraVelocidad()
    {
        maxSpeedTime = 20f;
    }

    public void OxigenImpulseValue(bool e)
    {
        currentVelocity = rb.linearVelocity;

        oxigenImpulse = e;
    }
}
