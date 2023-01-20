using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private int _prevIndex = -1;
    public int CurrentIndex = 0;
    public Animator Animator;

    private readonly int ID = Animator.StringToHash("Index");

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_prevIndex != CurrentIndex)
        {
            Animator.SetInteger(ID, CurrentIndex);
            _prevIndex = CurrentIndex;
        }
    }
}
