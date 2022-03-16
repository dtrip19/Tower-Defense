using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private float number;
    private float maxNumber;

    public int Number
    {
        get
        {
            return (int)number;
        }
        private set
        {
            number = Mathf.Clamp(value, 0, maxNumber);
        }
    }

    private void Awake()
    {
        
    }

    void Update()
    {
        GetComponent<Transform>();
        transform.position = new Vector3(0, 1 + Mathf.Sin(Time.time), 0);
    }
}
