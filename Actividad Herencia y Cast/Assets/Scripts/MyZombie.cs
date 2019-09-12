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
            }
            
            void Start()
            {
                VerificarVictima();
                StartCoroutine(ComportamientoZombie(datosZombie));
                
            }

            void OnDrawGizmos()
            {
                Gizmos.DrawLine(transform.localPosition, transform.localPosition + direction);
            }
            

            void Update()
            {
                if (Time.timeScale == 0) return;

                

                if (seMueveZ == 3) // Pursuing
                {
                    VerificarVictima();
                    PerseguirVictima(datosZombie);

                }
                else
                {
                    ComportamientoNormal(this.gameObject);
                }
            }
        }
    }
}