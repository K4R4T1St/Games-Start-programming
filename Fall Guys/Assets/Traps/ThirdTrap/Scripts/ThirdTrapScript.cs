using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTrapScript : MonoBehaviour
{
    public GameObject rockPrefab; 
    public float rockSpeed = 5f; 
    public float playerPushForce = 10f; 

    private float rockSpawnTimer = 0f; 
    private const float ROCK_SPAWN_INTERVAL = 2f; 

    //��� � � ��������� ������� ��������� FixedUpdate ��� ��� �� �� ������� �� ���, ��� ����� ����� ��� ������ ������
    void FixedUpdate()
    {
        rockSpawnTimer += Time.fixedDeltaTime;
        if (rockSpawnTimer >= ROCK_SPAWN_INTERVAL)
        {
            rockSpawnTimer = 0f;
            SpawnRock();
        }
    }

    void SpawnRock()
    {
        GameObject rock = Instantiate(rockPrefab, transform.position + Vector3.up * 5f, Quaternion.identity);
        rock.GetComponent<Rigidbody>().velocity = Vector3.down * rockSpeed;

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            StartCoroutine(DelayedDestroy(other.gameObject));
        }
    }

    //����� �������� �������� ����� ����� ������������ �� ����� � ����� ��������� ����� ��� ��� ����������� ������ ����� �������� �� ����� ������
    IEnumerator DelayedDestroy(GameObject rock)
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(rock);
    }
}
