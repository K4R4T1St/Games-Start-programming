using UnityEngine;

public class WindBlock : MonoBehaviour
{
    public float windForce;

    private Vector3 windDirection;
    private float windChangeTimer = 0f; 
    private const float WIND_CHANGE_INTERVAL = 2f;

    void Start()
    {
        windDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }
    
    //тут аналогично использую FixedUpdate так как необходимо чтобы физика объекта не зависила от фпс
    void FixedUpdate()
    {
        windChangeTimer += Time.fixedDeltaTime;
        if (windChangeTimer >= WIND_CHANGE_INTERVAL)
        {
            windChangeTimer = 0f;
            ChangeWindDirection();
        }
    }

    void ChangeWindDirection()
    {
        windDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb == null) return;
        rb.AddForce(windDirection * windForce, ForceMode.Acceleration);
    }
}