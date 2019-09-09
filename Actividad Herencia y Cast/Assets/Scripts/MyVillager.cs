﻿namespace NPC
{
    namespace Ally
    {
        using UnityEngine;
        using System.Collections;
        using NPC.Enemy;
        public struct VillagerStruct
        {
            // Datos del aldeano
            public enum nombresAldeano
            {
                Alex, Bernardo, Carlos, David, Fernando, Gustavo, Jose, Maria, Jesus, Pedro,
                Camilo, Alberto, Leon, John, Camila, Layla, Emily, Claire, Marta, Sofia
            };
            public nombresAldeano nombreAldeano;
            public int edadAldeano;
            public enum estadosAldeano { Idle, Moving, Rotating, Running};
            public estadosAldeano estadoAldeano;
            public float velocidadAldeano;

            public static explicit operator ZombieStruct(VillagerStruct md1)
            {
                ZombieStruct despuesStruct = new ZombieStruct();
                despuesStruct.edadZombi = md1.edadAldeano;
                despuesStruct.velocidadZombi = md1.velocidadAldeano;
                despuesStruct.colorZombi = Random.Range(0, 3);
                despuesStruct.gustoZombi = (ZombieStruct.gustosZombi)Random.Range(0, 5);
                

                return despuesStruct;
            }
        }
        public class MyVillager : NPCRegulator
        {
            public VillagerStruct datosAldeano;
            
            void Awake()
            {
                datosAldeano.nombreAldeano = (VillagerStruct.nombresAldeano)Random.Range(0, 20); // SELECTOR DE NOMBRES
                datosAldeano.edadAldeano = Random.Range(15, 101); // SELECTOR DE EDAD
                datosAldeano.velocidadAldeano = 4.0f;
            }

            private void OnCollisionEnter(Collision collision) // CONTACTO QUE CONVIERTE ALDEANO A ZOMBI
            {
                if (collision.transform.name == "Zombie")
                {
                    mensajeAldeano.SetActive(true);
                    ZombieStruct zombieStruct = gameObject.AddComponent<MyZombie>().datosZombie;
                    zombieStruct = (ZombieStruct)gameObject.GetComponent<MyVillager>().datosAldeano;
                    
                    switch (gameObject.GetComponent<MyZombie>().datosZombie.colorZombi)
                    {
                        case 0:
                            gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                            break;
                        case 1:
                            gameObject.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case 2:
                            gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                            break;
                    }
                    gameObject.name = "Zombie";
                    
                    Destroy(gameObject.GetComponent<MyVillager>());
                    
                }
            }

            void Start()
            {
                HuirAgresor(datosAldeano);
                StartCoroutine(ComportamientoAldeano(datosAldeano));
            }

            void OnDrawGizmos()
            {
                Gizmos.DrawLine(transform.localPosition, transform.localPosition + direction);
            }

            void Update()
            {
                if (seMueveV == 0) { } // Idle

                if (seMueveV == 1) // Moving
                {
                    transform.localPosition += transform.forward * datosAldeano.velocidadAldeano * (15 / (float)datosAldeano.edadAldeano) * Time.deltaTime;
                }

                if (seMueveV == 2) // Rotating
                {
                    if (selectorDireccionalV == 0) // Rotacion Positiva
                    {
                        transform.eulerAngles += new Vector3(0, Random.Range(10f, 150f) * Time.deltaTime, 0);
                    }
                    if (selectorDireccionalV == 1) // Rotacion Negativa
                    {
                        transform.eulerAngles += new Vector3(0, Random.Range(-10f, -150f) * Time.deltaTime, 0);
                    }
                }

                if (seMueveV == 3) // Running
                {
                    HuirAgresor(datosAldeano);
                }
            }            
        }
    }
}