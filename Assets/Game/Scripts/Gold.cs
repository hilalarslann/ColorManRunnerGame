using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public float MinRotateSpeed = 40;
    public float MaxRotateSpeed = 80;

    private float rotateSpeed;

    private void Start()
    {
        rotateSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);
    }
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    // Altýn toplandýðýnda yapýlacak iþlemler örnek ses - efekt
    public void Collect()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GameManager.Instance.Score++;
    }
}
