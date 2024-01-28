using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muve : MonoBehaviour
   

{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
