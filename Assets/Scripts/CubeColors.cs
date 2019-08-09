using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColors : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        this.GetComponent<Renderer>().material.color = new Color(.5843f, .6784f, .2431f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
