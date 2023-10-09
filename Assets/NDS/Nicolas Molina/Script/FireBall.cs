using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    void Update()
    {
        this.GetComponent<Rigidbody>().AddRelativeForce(transform.right);
        Destroy(this.gameObject,5f);
    }
}
