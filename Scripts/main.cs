using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;


public class main : MonoBehaviour
{
    public void load_level(string InputString)
    {
        //InputString format: level total min_take ... max_take
        string[] inputs = InputString.Split(' ');
        string name = inputs[0];
        level0to4.TOTAL = Convert.ToInt32(inputs[1]);
        level0to4.min_take = Convert.ToInt32(inputs[2]);
        level0to4.max_take = Convert.ToInt32(inputs[3]);
        SceneManager.LoadScene("Scenes/" + name);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
