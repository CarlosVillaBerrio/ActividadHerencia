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
    public int seMueveZ, selectorDireccionalZ;
    public int seMueveV, selectorDireccionalV;
    GameObject heroObject;
    GameObject villagerObject;
    GameObject zombiObject;
    public Vector3 direction;
    Vector3 dPlayer;
    Vector3 dZombi;
    Vector3 dAldeano;
    public float distanciaAJugador;
    public float distanciaAZombi;
    public float distanciaAldeano;
    public float tiempo = 3.0f;
    public GameObject mensajeZombi;
    public GameObject mensajeAldeano;

    public void ComportamientoNormal(GameObject gameObject)
    {
        if (seMueveZ == 0) { } // Idle

        if (seMueveZ == 1) // Moving
        {
            transform.localPosition += transform.forward * gameObject.GetComponent<MyZombie>().datosZombie.velocidadZombi * (15 / (float)gameObject.GetComponent<MyZombie>().datosZombie.edadZombi) * Time.deltaTime;
        }

        if (seMueveZ == 2) // Rotating
        {
            if (selectorDireccionalZ == 0) // Rotacion Positiva
            {
                transform.eulerAngles += new Vector3(0, Random.Range(10f, 150f) * Time.deltaTime, 0);
            }
            if (selectorDireccionalZ == 1) // Rotacion Negativa
            {
                transform.eulerAngles += new Vector3(0, Random.Range(-10f, -150f) * Time.deltaTime, 0);
            }
        }
    }

    public void VerificarVictima()
    {
        if (heroObject == null)
            heroObject = GameObject.Find("Heroe");

        GameObject[] AllGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject aGameObject in AllGameObjects)
        {
            //Component aComponent = aGameObject.GetComponent<MyHero>();
            //if (aComponent != null)
            //    heroObject = aGameObject;

            Component bComponent = aGameObject.GetComponent<MyVillager>();
            if (bComponent != null)
            {
                villagerObject = aGameObject;
                dAldeano = villagerObject.transform.position - transform.position;
                distanciaAldeano = dAldeano.magnitude;
                if (distanciaAldeano <= distanciaEntreObjetos)
                    break;
            }
                
                Component cComponent = aGameObject.GetComponent<MyZombie>();
            if (cComponent != null)
                zombiObject = aGameObject;
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
    public IEnumerator ComportamientoZombie(ZombieStruct gameStruct)
    {
        while (true)
        {
            dPlayer = heroObject.transform.position - transform.position;
            distanciaAJugador = dPlayer.magnitude;

            dAldeano = villagerObject.transform.position - transform.position;
            distanciaAldeano = dAldeano.magnitude;

            if (mensajeZombi == null)
            {
                mensajeZombi = GameObject.Find("Mensaje");
                mensajeZombi.GetComponent<TextMesh>().text = "Waaaarrrr quiero comer " + gameObject.GetComponent<MyZombie>().datosZombie.gustoZombi.ToString();

            }
            if(mensajeZombi != null)
                mensajeZombi.GetComponent<TextMesh>().text = "Waaaarrrr quiero comer " + gameObject.GetComponent<MyZombie>().datosZombie.gustoZombi.ToString();


            if (distanciaAJugador <= distanciaEntreObjetos)
            {
                mensajeZombi.SetActive(true);
                mensajeZombi.GetComponent<TextMesh>().text = "Waaaarrrr quiero comer " + gameObject.GetComponent<MyZombie>().datosZombie.gustoZombi.ToString();

                mensajeZombi.transform.rotation = heroObject.transform.rotation;
            }
            else
            {
                mensajeZombi.SetActive(false);
            }
                       
            if (distanciaAJugador <= distanciaEntreObjetos || distanciaAldeano <= distanciaEntreObjetos)
            {
                victimaCercana = true;
            }
            else
            {
                victimaCercana = false;
            }

            if (victimaCercana == true)
            {
                gameStruct.estadoZombi = ZombieStruct.estadosZombi.Pursuing;            
            }
            else
            {
                gameStruct.estadoZombi = (ZombieStruct.estadosZombi)Random.Range(0, 3);
                
            }
            switch (gameStruct.estadoZombi)
            {
                case ZombieStruct.estadosZombi.Idle: // Para que se quede quieto
                    seMueveZ = 0;
                    break;
                case ZombieStruct.estadosZombi.Moving: // Para que se mueva hacia el frente
                    seMueveZ = 1;
                    break;
                case ZombieStruct.estadosZombi.Rotating: // Para que rote
                    seMueveZ = 2;
                    selectorDireccionalZ = Random.Range(0, 2);
                    break;
                case ZombieStruct.estadosZombi.Pursuing:
                    seMueveZ = 3;
                    break;
            }

            yield return new WaitForSeconds(3.0f); // Espera 3 segundos y cambia de comportamiento
        }
    }

    public void VerificarAgresor()
    {
        if(heroObject == null)
        heroObject = GameObject.Find("Heroe");

        GameObject[] AllGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject aGameObject in AllGameObjects)
        {
            //Component aComponent = aGameObject.GetComponent<MyHero>();
            //if (aComponent != null)
            //    heroObject = aGameObject;

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
        if (distanciaAZombi <= distanciaEntreObjetos)
        {
            direction = Vector3.Normalize(zombiObject.transform.position - transform.position);
            transform.position += -1 * direction * als.velocidadAldeano * (15 / (float)als.edadAldeano) * Time.deltaTime;
        }       
    }


    public IEnumerator ComportamientoAldeano(VillagerStruct gameStruct)
    {
        while (true)
        {
            dPlayer = heroObject.transform.position - transform.position;
            distanciaAJugador = dPlayer.magnitude;

            dZombi = zombiObject.transform.position - transform.position;
            distanciaAZombi = dZombi.magnitude;

            if (mensajeAldeano == null)
            {
                mensajeAldeano = GameObject.Find("Mensaje");
            }

            if (distanciaAJugador <= distanciaEntreObjetos) // Muestra el mensaje del aldeano con el heroe cerca
            {
                mensajeAldeano.SetActive(true);
                mensajeAldeano.transform.rotation = heroObject.transform.rotation;
            }
            else
            {
                mensajeAldeano.SetActive(false);
            }

            if (distanciaAZombi <= distanciaEntreObjetos)
            {
                agresorCercano = true;
            }
            else
            {
                agresorCercano = false;
            }

                           

            if (agresorCercano == true)
            {
                gameStruct.estadoAldeano = VillagerStruct.estadosAldeano.Running;                
            }
            else
            {
                gameStruct.estadoAldeano = (VillagerStruct.estadosAldeano)Random.Range(0, 3);                
            }
            switch (gameStruct.estadoAldeano)
            {
                case VillagerStruct.estadosAldeano.Idle: // Para que se quede quieto
                    seMueveV = 0;
                    break;
                case VillagerStruct.estadosAldeano.Moving: // Para que se mueva hacia el frente
                    seMueveV = 1;
                    break;
                case VillagerStruct.estadosAldeano.Rotating: // Para que rote
                    seMueveV = 2;
                    selectorDireccionalV = Random.Range(0, 2);
                    break;
                case VillagerStruct.estadosAldeano.Running:
                    seMueveV = 3;
                    break;
            }
            yield return new WaitForSeconds(3.0f); // Espera 3 segundos y cambia de comportamiento
        }
    }    
}
