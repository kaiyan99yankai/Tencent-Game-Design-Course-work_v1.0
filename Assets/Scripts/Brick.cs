using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int timeslimit = 1;
    private int times = 0;
    void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        times++;
    }
    // Update is called once per frame
    void Update()
    {
        if (times == timeslimit) {
            Destroy(this.gameObject);
        } 
    }
}
