using UnityEngine;
using UnityEngine.EventSystems;

public class OxigenBtn : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Movement player;
    public PhysicsMaterial2D playerMat;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.OxigenImpulseValue(true);
        playerMat.bounciness = 0f;
    }

    // M�todo que se llama cuando se suelta el bot�n
    public void OnPointerUp(PointerEventData eventData)
    {
        player.OxigenImpulseValue(false);
        playerMat.bounciness = 0.4f;
    }
}
