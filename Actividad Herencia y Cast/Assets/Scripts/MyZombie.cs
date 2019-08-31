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
            public enum gustosZombi { cerebro, corazon, higado, nariz, lengua };
            public gustosZombi gustoZombi;
            public enum estadosZombi { Idle, Moving, Rotating, Pursuing };
            public estadosZombi estadoZombi;
            public float velocidadZombi;
        }
        public class MyZombie : NPCRegulator
        {
            public ZombieStruct datosZombie;

            GameObject heroObject;

            public void Awake()
            {
                datosZombie.gustoZombi = (ZombieStruct.gustosZombi)Random.Range(0, 5);
                datosZombie.colorZombi = Random.Range(0, 3);
                datosZombie.edadZombi = Random.Range(15, 101);
                datosZombie.velocidadZombi = 5.5f;
            }
            void Start()
            {
                StartCoroutine(ComportamientoZombie(datosZombie));
            }

            public void PerseguirVictima(ZombieStruct datosZombie)
            {
                // Buscar como calcular la distancia entre 2 objetos

            }

            

            void Update()
            {
                if (seMueveZ == 0) { } // Idle

                if (seMueveZ == 1) // Moving
                {
                    transform.localPosition += transform.forward * datosZombie.velocidadZombi * (15/ (float)datosZombie.edadZombi) * Time.deltaTime;
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

                if (seMueveZ == 3) // Pursuing
                {
                    PerseguirVictima(datosZombie);
                }
            }
        }
    }
}