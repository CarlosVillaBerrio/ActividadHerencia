using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;
using NPC.Ally;


public class NPCRegulator : MonoBehaviour
{
    public bool victimaCercana = false; // Variable para accionar la persecusion del zombie
    public bool agresorCercano = false;
    public float distanciaEntreObjetos = 15.0f;
    public int seMueve, selectorDireccional, edad, estadoActual;
    public float velocidad;
    public GameObject heroObject;
    public GameObject villagerObject;
    public GameObject zombiObject;
    public Vector3 direction;
    Vector3 dPlayer;
    Vector3 dZombi;
    Vector3 dAldeano;
    public float distanciaAJugador;
    public float distanciaAZombi;
    public float distanciaAldeano;

    public void ComportamientoNormal()
    {
        if (estadoActual == 0) { } // Idle

        if (estadoActual == 1) // Moving
        {
            transform.position += transform.forward * velocidad * (15 / (float)edad) * Time.deltaTime;

        }

        if (estadoActual == 2) // Rotating
        {
            if (selectorDireccional == 0) // Rotacion Positiva
            {
                transform.eulerAngles += new Vector3(0, Random.Range(10f, 150f) * Time.deltaTime, 0);
            }
            if (selectorDireccional == 1) // Rotacion Negativa
            {
                transform.eulerAngles += new Vector3(0, Random.Range(-10f, -150f) * Time.deltaTime, 0);
            }
        }
    }

    public IEnumerator EstadosComunes()
    {
        while (true)
        {
            estadoActual = Random.Range(0, 3);
            if (estadoActual == 2) // Rotating
            {
                selectorDireccional = Random.Range(0, 2);
            }
            yield return new WaitForSeconds(3.0f); // Espera 3 segundos y cambia de comportamiento
        }
    }

    public void VerificarVictima()
    {
        if (heroObject == null)
            heroObject = GameObject.Find("Heroe");

        dPlayer = heroObject.transform.position - transform.position;
        distanciaAJugador = dPlayer.magnitude;

        GameObject[] AllGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject aGameObject in AllGameObjects)
        {
            Component bComponent = aGameObject.GetComponent<MyVillager>();
            if (bComponent != null)
            {
                villagerObject = aGameObject;
                dAldeano = villagerObject.transform.position - transform.position;
                distanciaAldeano = dAldeano.magnitude;
                if (distanciaAldeano <= distanciaEntreObjetos)
                    break;
            }
        }
    }

    public void PerseguirVictima(ZombieStruct zs)
    {
        if (distanciaAldeano <= distanciaEntreObjetos)
        {
            direction = Vector3.Normalize(villagerObject.transform.position - transform.position);
            transform.position += direction * zs.velocidadZombi * (15 / (float)zs.edadZombi) * Time.deltaTime;
        }
        else if (distanciaAJugador <= distanciaEntreObjetos)
        {
            direction = Vector3.Normalize(heroObject.transform.position - transform.position);
            transform.position += direction * zs.velocidadZombi * (15 / (float)zs.edadZombi) * Time.deltaTime;
        }
    }

    public void VerificarAgresor()
    {
        if(heroObject == null)
            heroObject = GameObject.Find("Heroe");

        dPlayer = heroObject.transform.position - transform.position;
        distanciaAJugador = dPlayer.magnitude;

        GameObject[] AllGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject aGameObject in AllGameObjects)
        {
            Component bComponent = aGameObject.GetComponent<MyZombie>();
            if (bComponent != null)
            {
                zombiObject = aGameObject;
                dZombi = zombiObject.transform.position - transform.position;
                distanciaAZombi = dZombi.magnitude;
                if (distanciaAZombi <= distanciaEntreObjetos)
                    break;
            }                
        }
    }

    public void HuirAgresor(VillagerStruct als)
    {       
        direction = Vector3.Normalize(zombiObject.transform.position - transform.position);
        transform.position += -1 * direction * als.velocidadAldeano * (15 / (float)als.edadAldeano) * Time.deltaTime;
    }
}
