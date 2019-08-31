namespace NPC
{
    namespace Ally
    {
        using UnityEngine;
        using System.Collections;
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
        }
        public class MyVillager : NPCRegulator
        {
            public VillagerStruct datosAldeano;
            
            void Awake()
            {
                datosAldeano.nombreAldeano = (VillagerStruct.nombresAldeano)Random.Range(0, 20); // SELECTOR DE NOMBRES
                datosAldeano.edadAldeano = Random.Range(15, 101); // SELECTOR DE EDAD
                datosAldeano.velocidadAldeano = 7.0f;
            }

            void Start()
            {
                StartCoroutine(ComportamientoAldeano(datosAldeano));
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
            public void HuirAgresor(VillagerStruct datosAldeano)
            {

            }
        }
    }
}