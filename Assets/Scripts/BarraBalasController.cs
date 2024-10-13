using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraBalasController : MonoBehaviour
{
    public Image rellenoBarraBalas;
    public int balasMax = 10;
    private int balasActuales;

    // Start is called before the first frame update
    private void Start()
    {
        balasActuales = balasMax;
        ActualizarBarraBalas();
    }

    public void AumentarBalas()
    {
        balasActuales++;
        if (balasActuales > balasMax) balasActuales = balasMax;
        ActualizarBarraBalas();
    }

    public void AumentarNBalas(int cantidad)
    {
        balasActuales = balasActuales + cantidad;
        if (balasActuales > balasMax) balasActuales = balasMax;
        ActualizarBarraBalas();
    }

    public void DisminuirBalas()
    {
        balasActuales--;
        if (balasActuales < 0) balasActuales = 0;
        ActualizarBarraBalas();
    }

    private void ActualizarBarraBalas()
    {
        float porcentajeBalas = (float)balasActuales / balasMax;
        rellenoBarraBalas.fillAmount = porcentajeBalas;
    }
}
