using UnityEngine;
using System;
using NPC.Enemy;
using NPC.Ally;

public class MyHero : MonoBehaviour
{
    static System.Random r = new System.Random();
    public readonly float velHeroe = (float)r.NextDouble()*8;

    VillagerStruct datosAldeano;
    ZombieStruct datosZombie;
    bool contactoZombi;
    bool contactoAldeano;

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