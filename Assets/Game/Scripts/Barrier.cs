using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private Material barrierMaterial;
    private Material[] materials;

    private int rnd;
    void Start()
    {
        barrierMaterial = GetComponent<Renderer>().material;
        materials = GameManager.Instance.ColorMaterials;
        rnd = Random.Range(0, materials.Length);
        SetRandomColor();
    }

    void SetRandomColor()
    {
        barrierMaterial.color = materials[rnd].color;
        gameObject.tag = materials[rnd].name;
    }
}