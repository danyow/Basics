using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;
    // 分辨率范围
    [Range (10, 100)]
    public int resolution = 10;
    // [Range (0, 1)]
    public GraphFunctionName function;
    Transform[] points;
    static GraphFunction[] functions = {
        SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction, Ripple, Cylinder, Sphere, Torus
    };

    private void Awake() {
        // 间距 阶
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; i++) {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            // 这个false的目的是让这个控件在添加进来的时候 是否采用原先所处地方位置 不然就用父控件作为参照物
            point.SetParent(transform, false);
            points[i] = point; 
        }
    }

    private void Update() {
        float t = Time.time;
        GraphFunction f = functions[(int)function];
        float step = 2f / resolution;
        int i = 0;
        for (int z = 0; z < resolution; z++) {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++) {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }   
        }
    }

    const float pi = Mathf.PI;

    // 正弦函数
    static Vector3 SineFunction(float x, float z, float t) {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.z = z;
        return p;
    }

    // 2D正弦
    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        /**
        Sin(π(x+t)) + Sin(π(z+t))
        --------------------------
                    2
        如果 后面的t * 2的话就是Morphing(变形正弦函数)
         */
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(pi * (z + t));
        p.y *= 0.5f;
        p.z = z;
        return p;
    }

    // 多正弦函数
    static Vector3 MultiSineFunction(float x, float z, float t) {
        /**
                      Sin(2π(x+t))
        Sin(π(x+t)) + ------------
                           2
        如果 后面的t * 2的话就是Morphing(变形正弦函数)
         */
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        p.y *= 2f / 3f;
        p.z = z;
        return p;
    }
    
    // 多变正弦2d
    static Vector3 MultiSine2DFunction (float x, float z, float t) {
        
        Vector3 p;
        p.x = x;
		p.y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
		p.y += Mathf.Sin(pi * (x + t));
		p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
		p.y *= 1f / 5.5f;
        p.z = z;
        return p;
	}

    static Vector3 Ripple (float x, float z, float t) {
        Vector3 p;
        p.x = x;
		float d = Mathf.Sqrt(x * x + z * z);
		p.y = Mathf.Sin(pi* (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = z;
        return p;
	}

    static Vector3 Cylinder(float u, float v, float t) {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi *(6f * u + 2f * v + t)) * 0.2f;
        p.x = r * Mathf.Sin(pi * u);
        p.y = v;
        p.z = r * Mathf.Cos(pi * u);
        return p;
    }

    static Vector3 Sphere(float u, float v, float t) {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(pi * 0.5f * v);
        p.x = s * Mathf.Sin(pi * u);
        p.y = Mathf.Sin(pi * 0.5f * v);
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }

    static Vector3 Torus(float u, float v, float t) {
		Vector3 p;
        float r1 = 0.65f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(pi * (4f * v + t)) * 0.05f;
		float s = r2 * Mathf.Cos(pi * v) + 0.5f + r1;
		p.x = s * Mathf.Sin(pi * u);
		p.y = r2 * Mathf.Sin(pi * v);
		p.z = s * Mathf.Cos(pi * u);
		return p;
	}
}
