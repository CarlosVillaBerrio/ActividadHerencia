namespace NPC
{
    namespace Enemy
    {
        using UnityEngine;
        using System.Collections;
        

        public struct ZombieStruct
        {
            // Datos del zombi
            public int colorZombi;
            public int edadZombi;
            public enum estadosZombi { Idle, Moving, Rotating, Pursuing };
            public enum gustosZombi { cerebro, corazon, higado, nariz, lengua };
            public gustosZombi gustoZombi;           
            public estadosZombi estadoZombi;
            public float velocidadZombi;
        }
        
        public class MyZombie : NPCRegulator
        {
            public ZombieStruct datosZombie;
   
            public void Awake()
            {
                datosZombie.gustoZombi = (ZombieStruct.gustosZombi)Random.Range(0, 5);
                datosZombie.colorZombi = Random.Range(0, 3);
                datosZombie.edadZombi = Random.Range(15, 101);
                datosZombie.velocidadZombi = 2.5f;
                edad = datosZombie.edadZombi;
                velocidad = datosZombie.velocidadZombi;
            }

            public void ActualizadorDeEstadoZombie()
            {
                datosZombie.estadoZombi = (ZombieStruct.estadosZombi)estadoActual;
            }

            public void mostrarMensaje()
            {
                if (distanciaAJugador <= distanciaEntreObjetos) // Muestra el mensaje del aldeano con el heroe cerca
                {
                    gameObject.GetComponentInChildren<TextMesh>().text = "Waaaarrrr quiero comer " + gameObject.GetComponent<MyZombie>().datosZombie.gustoZombi.ToString();

                    gameObject.GetComponentInChildren<TextMesh>().transform.rotation = heroObject.transform.rotation;
                }
                else
                {
                    gameObject.GetComponentInChildren<TextMesh>().text = "";
                }
            }

            void Start()
            {
                VerificarVictima();
                StartCoroutine(EstadosComunes());                
            }

            void OnDrawGizmos()
            {
                Gizmos.DrawLine(transform.localPosition, transform.localPosition + direction);
            }
            
            void Update()
            {
                if (Time.timeScale == 0) return;

                if (distanciaAldeano <= distanciaEntreObjetos)
                {
                    ActualizadorDeEstadoZombie();
                    VerificarVictima();
                    PerseguirVictima(datosZombie);
                    mostrarMensaje();
                }
                    else if (distanciaAJugador <= distanciaEntreObjetos)
                    {
                        ActualizadorDeEstadoZombie();
                        VerificarVictima();
                        PerseguirVictima(datosZombie);
                        mostrarMensaje();
                    }
                        else
                        {
                            ActualizadorDeEstadoZombie();
                            VerificarVictima();
                            ComportamientoNormal();
                            mostrarMensaje();
                        }                
            }
        }
    }
}