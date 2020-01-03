using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;


public class level0 : MonoBehaviour
{
    int[] Input = new int[4];
    GameObject text1,text2,text3,text4,text5, background2, bracket1, bracket2;
    GameObject[] buttons = new GameObject[7];
    GameObject[] candies;
    int[] current,massive;
    int player = 0;
    public int total;

    public void menu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }
    public void choose_player(string InputString)
    {
        //InputString format: total min_take max_take player_number -> total player_number min_take ... max_take
        string[] inputs = InputString.Split(' '); 
        for (int i = 0; i < inputs.Length; i++) { Input[i] = Convert.ToInt32(inputs[i]); }; player = Input[1];
        candies = new GameObject[Input[0] + 1]; massive = new int[Input[0] + 1]; current = new int[Input[Input.Length-1] + 1];
        for (int i = 1; i < Input[0] + 1; i++) { candies[i] = GameObject.Find("Candy" + i.ToString()); massive[i] = 1; };
        for (int i = 1; i < Input[Input.Length-1] + 1; i++) { current[i] = -1; };
        for (int i = 1; i < 3; i++) { buttons[i].SetActive(false); };
        for (int i = 3; i < 5; i++) { buttons[i].SetActive(true); };
        background2.SetActive(true); total = Input[0];
        if (player == 1) { text2.SetActive(true); }
        else { text1.SetActive(true); Invoke("computer_random_take", 2.0f); };
    }

    public void on_click(int id)
    {
        if (player == 0) { return; }; int i = 1;
        while (i < Input[Input.Length-1]+1 && current[i] > 0 ) { if (id == current[i]) { return; }; i++; };
        if (current[Input[Input.Length-1]]>0) { 
            for (int j =1;j<Input[Input.Length-1]+1;j++) { candies[current[j]].GetComponent<Image>().color = Color.white; current[j] = -1;};
            current[1] = id; highlighte(id); return;
        };
        current[i] = id; highlighte(id);
    }

    void computer_take()
    {
        int i = 1; int j = 1; text3.SetActive(true);
        while (i < Input[Input.Length-1] + 1 && current[i]>0 ) { current[i] = -1; i++; };
        for (int k = i; k < (Input[Input.Length-1]+1) + 1; k++) {while (j<Input[0]+1 && massive[j] < 1) { j++; };
                                                      massive[j] = 0; candies[j].SetActive(false);total--; };
        if (total == 0) { text3.SetActive(false);text5.SetActive(true);return; };
        text2.SetActive(true); text3.SetActive(false); text1.SetActive(false);
    }
    void computer_random_take()
    {
        // Random.Range(int,int): max is exclusive 
        int c = UnityEngine.Random.Range(1, 3); 
        for (int j = 1; j < Input[Input.Length-1] + 1; j++) { current[j] = -1; };
        if (total % (Input[2] + Input[Input.Length-1]) == Input[2]) { for (int i = 1; i < Input[Input.Length-1] + 1; i++) { current[i] = 1; }; player = 1; computer_take(); return; };
        if (total % (Input[2] + Input[Input.Length-1]) == Input[Input.Length-1]) { for (int i = 1; i < Input[2] + 1; i++) { current[i] = 1; }; player = 1; computer_take(); return; };
        if (c > 1) { for (int j=1; j < Math.Min(Input[Input.Length-1],total)+1; j++) {int i = 1; while (i < Input[0] + 1 && massive[i] < 1) { i++; };
                                                                        massive[i] = 0; candies[i].SetActive(false); total--; }; }
        else { int i = 1; while (i < Input[0] + 1 && massive[i] < 1) { i++; }; massive[i] = 0; candies[i].SetActive(false); total--; };        
        if (total == 0) { text1.SetActive(false); text3.SetActive(false); text5.SetActive(true); return; }
        text1.SetActive(false); text2.SetActive(true); text3.SetActive(false); 
    }
    void highlighte(int id)
    { 
        if (id % 3 == 1) { candies[id].GetComponent<Image>().color = Color.green; };
        if (id % 3 == 2) { candies[id].GetComponent<Image>().color = Color.red; };
        if (id % 3 == 0) { candies[id].GetComponent<Image>().color = Color.blue; };
    }
    public void human_take()
    {        
        if (current[1] < 0 || current[Input[2]+1]*current[Input[Input.Length-1]]<0) { return; }
        else { int j = 1; while (j < Input[Input.Length-1] + 1 && current[j] > 0) { candies[current[j]].SetActive(false); massive[current[j]] = 0; total--; j++; }; };
        if (total == 0) { text2.SetActive(false);text4.SetActive(true); return; }
        text2.SetActive(false); text3.SetActive(true);
        if (player == 1) { Invoke("computer_take", 1.5f); } else { Invoke("computer_random_take", 1.5f); };        
    }
    public void restart()
    {
        player = 0; total = Input[0];
        for (int i = 1; i < Input[Input.Length-1] + 1; i++) { current[i] = -1; };
        for (int i = 1; i < 3; i++) { buttons[i].SetActive(true); };
        for (int i = 3; i < 5; i++) { buttons[i].SetActive(false); };
        background2.SetActive(false); text1.SetActive(false); text2.SetActive(false);text4.SetActive(false);text5.SetActive(false);
        for (int i = 1; i < Input[0]+1; i++) { candies[i].SetActive(true);  candies[i].GetComponent<Image>().color = Color.white; massive[i] = 1; };
    }
    public void clue()
    {
        bracket1.SetActive(true); bracket2.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 7; i++) { buttons[i] = GameObject.Find("Button" + i.ToString()); };
        for (int i = 3; i < 5; i++) { buttons[i].SetActive(false); };
        text1 = GameObject.Find("Text (1)"); text1.SetActive(false);
        text2 = GameObject.Find("Text (2)"); text2.SetActive(false);
        text3 = GameObject.Find("Text (3)"); text3.SetActive(false);
        text4 = GameObject.Find("Text (4)"); text4.SetActive(false);
        text5 = GameObject.Find("Text (5)"); text5.SetActive(false);
        bracket1 = GameObject.Find("bracket1"); bracket1.SetActive(false);
        bracket2 = GameObject.Find("bracket2"); bracket2.SetActive(false);
        background2 = GameObject.Find("background2"); background2.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
