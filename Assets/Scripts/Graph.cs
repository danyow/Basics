using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;
    // 分辨率范围
    [Range (10, 100)]
    public int resolution = 10;

    Transform[] points;
    private void Awake() {
        points = new Transform[resolution];
        // 间距 阶
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position = Vector3.zero;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            // position.y = position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;
            // 这个false的目的是让这个控件在添加进来的时候 是否采用原先所处地方位置 不然就用父控件作为参照物
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    private void Update() {
        for (int i = 0; i < points.Length; i++) {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
            point.localPosition = position;
        }
    }

}
