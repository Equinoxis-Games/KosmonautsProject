using UnityEngine;

public class FollowBar : MonoBehaviour
{
    public Transform playerTr;

    private void LateUpdate()
    {
        transform.position = playerTr.position;
    }
}
