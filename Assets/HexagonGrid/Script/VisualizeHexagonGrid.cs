using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mytool
{
    public class VisualizeHexagonGrid : MonoBehaviour
    {
        public GameObject cube;
        public Transform hit_cube;
        public Camera target_camera;
        public Transform place_holder;

        // geogebra的3軸方向不太一樣
        static Vector3 xDir = new Vector3(1, 0, -1); //new Vector3(1, -1, 0);
        static Vector3 yDir = new Vector3(0, 1, -1);// new Vector3(0, -1, 1);

        public int xCount = 5;
        public int yCount = 5;

        public void setCameraTransform()
        {
            target_camera.transform.position = transform.position + new Vector3(1, 1, 1) * 5;
            target_camera.transform.LookAt(transform.position, Vector3.up);
            var hexagonGridCamera = target_camera.GetComponent<HexagonGridCamera>();
            hexagonGridCamera.visualizeHexagonGrid = this;
        }

        Vector3 getNearBoxOffset(ref Vector3 hit_pos)
        {
            var fraction_x = hit_pos.x - Mathf.Floor(hit_pos.x);
            var fraction_y = hit_pos.y - Mathf.Floor(hit_pos.y);
            var fraction_z = hit_pos.z - Mathf.Floor(hit_pos.z);

            var min = fraction_x;
            var offset = new Vector3(-1, 0, 0);
            if (min > fraction_y)
            {
                min = fraction_y;
                offset = new Vector3(0, -1, 0);
            }

            if (min > fraction_z)
            {
                min = fraction_z;
                offset = new Vector3(0, 0, -1);
            }

            Debug.Log(hit_pos + " ; distance to floor(" + fraction_x + "," + fraction_y + "," + fraction_z + ") ;offset" + offset);
            return offset;
        }

        public void hitBox(Ray ray)
        {
            // to local ray
            ray.origin = transform.InverseTransformPoint(ray.origin);
            ray.direction = transform.InverseTransformVector(ray.direction);

            /*
             雖然荷蘭兄已經有提供教學
             但我還是想試試自己的方法

             group 0 : x+y+z=0
             https://www.geogebra.org/m/jmznkghr

            group 1 : x+y+z=1
            https://www.geogebra.org/m/h7nqemzh
             */

            // 擊中 x+y+z=3的平面
            Vector3 hit_pos;
            var C = new Vector3(1.0f / 3, 1.0f / 3, 4.0f / 3);
            var N = new Vector3(1, 1, 1).normalized;
            var result = GeometryTool.RayHitPlane(ray.origin, ray.direction, N, C, out hit_pos);

            // 分組:看看取floor之後落在 x+y+z=0 還是 x+y+z=1
            var index = new Vector3(Mathf.Floor(hit_pos.x), Mathf.Floor(hit_pos.y), Mathf.Floor(hit_pos.z));
            var group = index.x + index.y + index.z;
            Debug.Log("group value:" + group + " ; " + index);

            if (group == 0)
                hit_cube.localPosition = index;
            else
                hit_cube.localPosition = index + getNearBoxOffset(ref hit_pos);

            Debug.DrawLine(transform.TransformPoint(ray.origin), transform.TransformPoint(hit_pos));
        }

        public void generateCubeSet()
        {
            Debug.Log(yDir);

            // clear 
            var child_count = place_holder.childCount;
            for (var i = 0; i < child_count; ++i)
                DestroyImmediate(place_holder.GetChild(0).gameObject);

            // generate
            for (var x = -xCount; x <= xCount; ++x)
                for (var y = -yCount; y <= yCount; ++y)
                    GameObject.Instantiate<GameObject>(cube, xDir * x + yDir * y + transform.position, Quaternion.identity, place_holder);
        }
    }

}
