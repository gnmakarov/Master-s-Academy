using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class leaf : MonoBehaviour
{
    
    public int id;
    public bool take_leaf = false;
    level12 MainCamera;
    public void OnMouseDown()
    {   int current1 = MainCamera.GetComponent<level12>().current1;
        int current2 = MainCamera.GetComponent<level12>().current2;
        if (current1 < 0) { current1 = id; GameObject.Find(id.ToString()).GetComponent<SpriteRenderer>().color = Color.green;}
        else if (current2 < 0 ) { if (Math.Abs(id - current1)%10 != 1) { }
                                  else {current2 = id;GameObject.Find(id.ToString()).GetComponent<SpriteRenderer>().color = Color.green;};}
        else {            
            GameObject.Find(MainCamera.current1.ToString()).GetComponent<SpriteRenderer>().color = MainCamera.native_colors[current1];
            GameObject.Find(MainCamera.current2.ToString()).GetComponent<SpriteRenderer>().color = MainCamera.native_colors[current2];
            GameObject.Find(id.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
            current1 = id; current2 = -1;
        };
        MainCamera.GetComponent<level12>().current1 = current1;
        MainCamera.GetComponent<level12>().current2 = current2;
    }
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<level12>();
    }

    // Update is called once per frame
    void Update()
    {
        if (take_leaf) {           
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-1, -4, 0), 0.4f); };
    }
}
