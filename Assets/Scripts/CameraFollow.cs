using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float Smoothing = 5.0f;

    Vector3 m_OffsetVal;

    // Start is called before the first frame update
    void Start()
    {
        m_OffsetVal = transform.position - Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetcamerapos = Target.position + m_OffsetVal;
        transform.position = Vector3.Lerp(transform.position, targetcamerapos, Smoothing * Time.deltaTime);
    }
}
