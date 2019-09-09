using UnityEngine;
using System;
using NPC.Enemy;
using NPC.Ally;
using TMPro;
public class MyHero : MonoBehaviour
{
    static System.Random r = new System.Random();
    //public readonly float velHeroe = (float)r.NextDouble()*8;
    public readonly float velHeroe = 8.0f;

    VillagerStruct datosAldeano;
    ZombieStruct datosZombie;
    bool contactoZombi;
    bool contactoAldeano;
    public GameObject mensajito;


    private void Start()
    {
        var mensajitos = FindObjectsOfType<GameObject>();
        foreach (var item in mensajitos)
        {
            if (item.name == "Mensaje Final")
            {
                mensajito = item;
                mensajito.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (contactoAldeano)
        {
            Debug.Log(MensajeAldeano(datosAldeano));

            contactoAldeano = false;
        }

        if (contactoZombi)
        {
            Debug.Log(MensajeZombi(datosZombie));
            contactoZombi = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Aldeano")
        {
            contactoAldeano = true;
            datosAldeano = collision.gameObject.GetComponent<MyVillager>().datosAldeano;
        }

        if (collision.transform.name == "Zombie")
        {
            contactoZombi = true;
            datosZombie = collision.gameObject.GetComponent<MyZombie>().datosZombie; // Esto va en el colision de cada zombie o aldeano
            Debug.Log("Game Over");
            mensajito.SetActive(true);
            // aqui voy saque el game over cuando lo toquen
            Time.timeScale = 0;

        }
    }

    public string MensajeZombi(ZombieStruct datosZombie)
    {
        string mensajeZombi = "Waaaarrrr quiero comer " + datosZombie.gustoZombi;
        return mensajeZombi;
    }

    public string MensajeAldeano(VillagerStruct datosAldeano)
    {
        string mensajeAldeano = "Hola soy " + datosAldeano.nombreAldeano + " y tengo " + datosAldeano.edadAldeano + " años";
        return mensajeAldeano;
    }
}