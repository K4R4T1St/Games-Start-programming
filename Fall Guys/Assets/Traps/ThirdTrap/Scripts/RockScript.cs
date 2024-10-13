using UnityEngine;

public class RockScript : MonoBehaviour
{
    public float playerPushForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody player = other.gameObject.GetComponent<Rigidbody>();
            player.AddForce((transform.position - other.transform.position).normalized * playerPushForce, ForceMode.Impulse);
        }
    }
}
