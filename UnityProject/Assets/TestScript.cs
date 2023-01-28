using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public int ID = 0;
    private int prev = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (prev != ID)
        {
            GetComponent<Animator>().SetInteger("Index", ID);
            prev = ID;
        }
    }
}
