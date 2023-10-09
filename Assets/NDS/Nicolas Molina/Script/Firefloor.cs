using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefloor : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        target = GameObject.Find("Priest");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(target.transform.position.x,0, target.transform.position.z);
        Destroy(this.gameObject, 10f);
    }
}
