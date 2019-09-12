using UnityEngine;
using System;
using NPC.Enemy;
using NPC.Ally;
using TMPro;
public class MyHero : MonoBehaviour
{
    static System.Random r = new System.Random(); // VARIABLE AUXILIAR PARA ASIGNACION NATIVA DEL READONLY
    public readonly float velHeroe = (float)r.NextDouble()*8.0f + 1.5f; // VELOCIDAD ALEATORIA DEL HEROE

    VillagerStruct datosAldeano;
    ZombieStruct datosZombie;
    bool contactoZombi;
    bool contactoAldeano;
    public GameObject mensajito;
    
    private void Start()
    {
        var mensajitos = FindObjectsOfType<GameObject>(); // LISTA PARA DETECTAR EL GAME OVER
        foreach (var item in mensajitos)
        {
            if (item.name == "Mensaje Final")
            {
                mensajito = item; // ASIGNA EL TEXTO EN EL CANVAS CON EL GAME OVER
                mensajito.SetActive(false); // DESACTIVA EL TEXTO CAMVAS DEL GAME OVER
            }
        }
    }

    void Update() // CONDICIONES PARA MENSAJES POR CONTACTO
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
            // aqui saca el game over cuando lo tocan
            Time.timeScale = 0; // EL TIMESCALE LO VUELVE CERO PARA DETENER EL JUEGO CUANDO UN ZOMBIE TOQUE AL HEROE

        }
    }

    // FUNCIONES QUE DEVUELVEN EL MENSAJE POR CONTACTO CON EL HEROE

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