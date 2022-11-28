using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCreator : MonoBehaviour
{
    [HideInInspector]
    public List<Road> ActiveRoads;

    public GameObject[] RoadPrefabs;

    public GameObject FinishRoad;

    [Space]
    public Transform Player;
    public float Far;

    private Vector3 lastSpawnPos;

    private void Update()
    {
        if (GameManager.Instance.start)
        {
            if (!GameManager.Instance.endGame)
            {
                UpdateCreateRoads();
                UpdateDestroyRoads();
            }
        }
    }
    private void UpdateCreateRoads()
    {
        float distance = Vector3.Distance(Player.transform.position, lastSpawnPos);
        if (distance < Far)
            CreateRoad();
    }
    private void UpdateDestroyRoads()
    {
        if (ActiveRoads.Count > 0)
        {
            var road = ActiveRoads[0].GetComponent<Road>();
            float distance = Vector3.Distance(Player.transform.position, road.transform.position);

            if (distance > Far)
            {
                ActiveRoads.RemoveAt(0);
                Destroy(road.gameObject);
            }
        }
    }
    private void CreateRoad()
    {
        var road = CreateRandomRoad().GetComponent<Road>();
        road.transform.position = lastSpawnPos;
        lastSpawnPos.z += road.Length;

        ActiveRoads.Add(road);
    }
    private GameObject CreateRandomRoad()
    {
        if (GameManager.Instance.Score >= GameManager.Instance.maxScore)
        {
            GameManager.Instance.endGame = true;
            return Instantiate(FinishRoad);
        }
        else
        {
            return Instantiate(RoadPrefabs[Random.Range(0, RoadPrefabs.Length)]);
        }
    }
}