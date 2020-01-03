using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;


public class level0to4 : MonoBehaviour
{
    public GameObject prefab, canvas;
    GameObject text1,text2,text3,text4,text5, background2, bracket1, bracket2;
    GameObject[] buttons = new GameObject[7];
    GameObject[] candies;
    int[] current,massive;
    static public int TOTAL, max_take, min_take;
    int total, player = 0;
    public void menu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }
    public void choose_player(int num)
    {
        for (int i = 1; i < 3; i++) { buttons[i].SetActive(false); };
        for (int i = 3; i < 5; i++) { buttons[i].SetActive(true); };
        background2.SetActive(true); player = num;
        if (player == 1) { text2.SetActive(true); }
        else { text1.SetActive(true); Invoke("computer_random_take", 2.0f); };
    }

    public void on_click(int id)
    {
        if (player == 0) { return; }; int i = 1;
        while (i < max_take + 1 && current[i] > 0 ) { if (id == current[i]) { return; }; i++; };
        if (current[max_take]>0) { 
            for (int j = 1; j < max_take + 1; j++) { candies[current[j]].GetComponent<Image>().color = Color.white; current[j] = -1;};
            current[1] = id; highlighte(id); return;
        };
        current[i] = id; highlighte(id);
    }

    void computer_take()
    {
        int i = 1; int j = 1; text3.SetActive(true);
        while (i < max_take + 1 && current[i]>0 ) { current[i] = -1; i++; };
        for (int k = i; k < max_take + 2; k++) {while (j < TOTAL + 1 && massive[j] < 1) { j++; };
                                                      massive[j] = 0; candies[j].SetActive(false);total--; };
        if (total == 0) { text3.SetActive(false);text5.SetActive(true);return; };
        text2.SetActive(true); text3.SetActive(false); text1.SetActive(false);
    }
    void computer_random_take()
    {
        // Random.Range(int,int): max is exclusive 
        int c = UnityEngine.Random.Range(1, 3); 
        for (int j = 1; j < max_take + 1; j++) { current[j] = -1; };
        if (total % (min_take + max_take) == min_take) { for (int i = 1; i < max_take + 1; i++) { current[i] = 1; }; player = 1; computer_take(); return; };
        if (total % (min_take + max_take) == max_take) { for (int i = 1; i < min_take + 1; i++) { current[i] = 1; }; player = 1; computer_take(); return; };
        if (c > 1) { for (int j=1; j < Math.Min(max_take,total)+1; j++) {int i = 1; while (i < TOTAL + 1 && massive[i] < 1) { i++; };
                                                                        massive[i] = 0; candies[i].SetActive(false); total--; }; }
        else { int i = 1; while (i < TOTAL + 1 && massive[i] < 1) { i++; }; massive[i] = 0; candies[i].SetActive(false); total--; };        
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
        if (current[1] < 0 || current[max_take]*current[2]<0) { return; } //TODO change 2
        else { int j = 1; while (j < max_take + 1 && current[j] > 0) { candies[current[j]].SetActive(false); massive[current[j]] = 0; total--; j++; }; };
        if (total == 0) { text2.SetActive(false);text4.SetActive(true); return; }
        text2.SetActive(false); text3.SetActive(true);
        if (player == 1) { Invoke("computer_take", 1.5f); } else { Invoke("computer_random_take", 1.5f); };        
    }
    public void restart()
    {
        player = 0; total = TOTAL;
        for (int i = 1; i < max_take + 1; i++) { current[i] = -1; };
        for (int i = 1; i < 3; i++) { buttons[i].SetActive(true); };
        for (int i = 3; i < 5; i++) { buttons[i].SetActive(false); };
        background2.SetActive(false); text1.SetActive(false); text2.SetActive(false);text4.SetActive(false);text5.SetActive(false);
        for (int i = 1; i < TOTAL+1; i++) { candies[i].SetActive(true);  candies[i].GetComponent<Image>().color = Color.white; massive[i] = 1; };
    }
    public void clue()
    {
        bracket1.SetActive(true); bracket2.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        candies = new GameObject[TOTAL + 1]; massive = new int[TOTAL + 1]; current = new int[max_take + 1];
        for (int i = 1; i < 7; i++) { buttons[i] = GameObject.Find("Button" + i.ToString()); };
        for (int i = 3; i < 5; i++) { buttons[i].SetActive(false); };
        switch(TOTAL)
        {
            case 3: Instantiate(Resources.Load("3sticks") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                    buttons[6].SetActive(false);
                    break;
            case 6: Instantiate(Resources.Load("6sticks") as GameObject, new Vector3(-0.8f, -0.3f, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                    break;
            case 8: Instantiate(Resources.Load("8sticks") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                    break;
            case 9: Instantiate(Resources.Load("9sticks") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                    break;
        }
        for (int i=1; i< TOTAL + 1; i++) { candies[i] = GameObject.Find("Candy" + i.ToString()); 
                                           int j = i; //sth magic, we create own variable for each step
                                           candies[i].GetComponent<Button>().onClick.AddListener(() => on_click(j)); massive[i] = 1;};
        total = TOTAL;
        for (int i = 1; i < max_take + 1; i++) { current[i] = -1; };
        GameObject.Find("Text").GetComponent<Text>().text = "Перед вами " + TOTAL.ToString() + " палочки. За ход можно взять " + min_take.ToString() + " или " + max_take.ToString() + ".Выигрывает тот, кто берёт последнюю палочку.\n\n-Кто может всегда выигрывать ?\n-Как нужно играть?";
        text1 = GameObject.Find("Text (1)"); text1.SetActive(false);
        text2 = GameObject.Find("Text (2)"); text2.GetComponent<Text>().text = "              Ваш ход,\nвозьмите " + min_take.ToString() +" или " + max_take.ToString() + " палочки: "; text2.SetActive(false);
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
