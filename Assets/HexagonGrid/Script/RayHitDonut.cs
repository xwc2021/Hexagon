using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHitDonut : MonoBehaviour
{
    public UpdateDountMaterial updateDountMaterial;
    Camera using_camera;
    private void Awake()
    {
        using_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = using_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                var uv = hit.textureCoord;
                Debug.Log(uv);
                updateDountMaterial.updateMaterialProperty(uv);
            }
            else
                Debug.Log("not Hit");


        }

    }
}
