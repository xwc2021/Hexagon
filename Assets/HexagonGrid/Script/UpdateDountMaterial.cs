using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDountMaterial : MonoBehaviour
{
    public Material material;
    // Update is called once per frame
    public void updateMaterialProperty(Vector2 uv)
    {
        material.SetVector("_userClickUV", uv);
    }
}
