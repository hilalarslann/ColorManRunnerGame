using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldLine : MonoBehaviour
{
    public GameObject GoldPrefab;
    public float Space = 0.5f;
    public float Height = 0;

    private void Start()
    {
        CreateGolds();
    }
    private void CreateGolds()
    {
        GoldForeach(CreateGold);
    }
    private void CreateGold(Vector3 pos)
    {
        var gold = Instantiate(GoldPrefab, pos, Quaternion.identity, transform);
    }
    //Editorde çalýþýr
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
        GoldForeach(DrawGoldGizmos);
    }
    private void DrawGoldGizmos(Vector3 pos)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pos, 0.25f);
    }
    private void GoldForeach(Action<Vector3> action)
    {
        Vector3 pos = transform.position + new Vector3(0, 0, transform.localScale.z / 2);
        pos.y = Height;

        // yukarý yuvarlar
        int count = Mathf.CeilToInt(transform.localScale.z / Space);

        for (int i = 0; i < count; i++)
        {
            action(pos);
            pos.z -= Space;
        }
    }
}
