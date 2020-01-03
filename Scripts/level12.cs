using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class level12 : MonoBehaviour
{
    public int current1 = -1, current2 = -1;
    int player = 0, total = 12;
    GameObject button1, button2, button3, button4, text1, text2;
    public GameObject[] leaf = new GameObject[13];
    public Color[] native_colors = new Color[13];
    Vector3[] initial_position = new Vector3[13];
    int[] massive = { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    public void func_button1()
    {
        button1.SetActive(false); button2.SetActive(false); text2.SetActive(false); player = 1;
        for (int i = 0; i < 13; i++) { string s = i.ToString(); leaf[i].SetActive(true); }
        text1.SetActive(true); button3.SetActive(true); button4.SetActive(true);
        text1.GetComponent<Text>().text = "                            Ваш ход,\n выберите 1 лепесток или 2 подряд:";
    }
    public void func_button2()
    {
        button1.SetActive(false); button2.SetActive(false); text2.SetActive(false); player = 2;
        for (int i = 0; i < 13; i++) { string s = i.ToString(); leaf[i].SetActive(true); }
        text1.SetActive(true); button3.SetActive(true); button4.SetActive(true);
        text1.GetComponent<Text>().text = "                  Вы играете вторым,\nсейчас ход компьютера, подождите ...";
        Invoke("comp_take_random", 3f);           
    }

    public void human_take()
    {
        if (current1 < 0) { } else {            
            leaf[current1].GetComponent<leaf>().take_leaf = true; total--; massive[current1]= 0;
            leaf[current1].GetComponent<SpriteRenderer>().color = native_colors[current1];
            if (current2 > 0) { leaf[current2].GetComponent<leaf>().take_leaf = true; total--; massive[current2] = 0;
                                leaf[current2].GetComponent<SpriteRenderer>().color = native_colors[current2];};
            if (total == 0) { text1.GetComponent<Text>().text = "!!! WINNER !!!"; return; };
            text1.GetComponent<Text>().text = "Ход компьютера, подождите ...";
            if (player == 1) { Invoke("comp_take", 1.5f); } else { Invoke("comp_take_random", 1.5f); }; };

    }
    public void comp_take()
    {   current1 = (current1 + 6) % 12; if (current1 == 0) { current1 = 12;};
        leaf[current1].GetComponent<leaf>().take_leaf = true; total--; 
        leaf[current1].GetComponent<SpriteRenderer>().color = native_colors[current1];
        if (current2 > 0)
        {   current2 = (current2 + 6) % 12; if (current2 == 0) { current2 = 12;};
            leaf[current2].GetComponent<leaf>().take_leaf = true;total--;
            leaf[current2].GetComponent<SpriteRenderer>().color = native_colors[current2];};
        current1 = -1; current2 = -1;
        if (total == 0) { text1.GetComponent<Text>().text = "!!! GAMEOVER !!!"; return; };
        text1.GetComponent<Text>().text = "                            Ваш ход,\n выберите 1 лепесток или 2 подряд:";
    }

    public void comp_take_random()
    {   // Random.Range(int,int): max is exclusive 
        current1 = 0;
        while (massive[current1] != 1 && total>0) { int c = Random.Range(1, 3); Debug.Log(c);
            if (c > 1)
            {   current1 = Random.Range(1, 12);
                if (massive[current1 + 1] != 0) { current2 = current1 + 1; }
                else if (massive[current1 - 1] != 0) { current2 = current1 + 1; }
                else { current2 = -1; }
            }
            else { current1 = Random.Range(1, 13); current2 = -1; };
            Debug.Log(current1);
            Debug.Log(current2);
        };

        leaf[current1].GetComponent<leaf>().take_leaf = true; total--; massive[current1] = 0;
        leaf[current1].GetComponent<SpriteRenderer>().color = native_colors[current1];
        if (current2 > 0)
        {   leaf[current2].GetComponent<leaf>().take_leaf = true; total--; massive[current2] = 0;
            leaf[current2].GetComponent<SpriteRenderer>().color = native_colors[current2]; };
        current1 = -1; current2 = -1;
        if (total == 0) { text1.GetComponent<Text>().text = "!!! GAMEOVER !!!"; return; };
        text1.GetComponent<Text>().text = "                            Ваш ход,\n выберите 1 лепесток или 2 подряд:";
    }

    public void restart()
    {
        text1.SetActive(false); text2.SetActive(true);
        button1.SetActive(true); button2.SetActive(true); button3.SetActive(false); button4.SetActive(false);
        for (int i = 0; i < 13; i++) { leaf[i].SetActive(false); leaf[i].transform.position = initial_position[i];
            leaf[i].GetComponent<leaf>().take_leaf = false; leaf[i].GetComponent<SpriteRenderer>().color = native_colors[i]; 
            massive[i] = 1; };
        player = 0; current1 = -1; current2 = -1; massive[0] = 0; total = 12;
    }
    // Start is called before the first frame update
    void Start()
    {
        button1 = GameObject.Find("Button1");
        button2 = GameObject.Find("Button2");
        button3 = GameObject.Find("Button3"); button3.SetActive(false);
        button4 = GameObject.Find("Button4"); button4.SetActive(false);
        text1 = GameObject.Find("Text (1)"); text1.SetActive(false);
        text2 = GameObject.Find("Text (2)"); 
        for (int i=0; i< 13; i++) {string s = i.ToString(); leaf[i] = GameObject.Find(s);
            leaf[i].GetComponent<leaf>().id = i; native_colors[i] = leaf[i].GetComponent<SpriteRenderer>().color;
            initial_position[i] = leaf[i].transform.position;  leaf[i].SetActive(false);};
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
