using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    public Transform target;

    void Update () {
        Vector3 heading = target.transform.position - transform.position;
        Vector3 heading2d = new Vector2(heading.x, heading.z).normalized;
        float angle = Mathf.Atan2(heading2d.y, heading2d.x) * -Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}