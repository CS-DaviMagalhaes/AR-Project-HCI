using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using TMPro;

public class modTextBalas : MonoBehaviour {

    public TextMeshProUGUI text;
    public event Action FinDeJuego;
    public int Balas = 10;
    public int BalasMax = 10;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Balas: " + Balas.ToString();
    }

    public void Disparar()
    {
        if (Balas > 0)
        {
            Balas--;
            text.text = "Balas: " + Balas.ToString();
            if (Balas == 0)
            {
                FinDeJuego?.Invoke();
            }
        }
        else
        {
            Debug.Log("No tienes balas para disparar.");
        }
    }

    // Update is called once per frame
    public void GanarBalas(int N)
    {
        Balas += N;
        if (Balas > BalasMax) Balas = BalasMax;

        text.text = "Balas: " + Balas.ToString();
    }
}
