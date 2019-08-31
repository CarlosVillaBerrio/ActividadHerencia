using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;
using NPC.Ally;

public class NPCRegulator : MonoBehaviour
{
    public bool victimaCercana = false; // Variable para accionar la persecusion del zombie
    public bool agresorCercano = false;
    public float distanciaVictima;
    public int seMueveZ, selectorDireccionalZ;
    public int seMueveV, selectorDireccionalV;

    public IEnumerator ComportamientoZombie(ZombieStruct gameStruct)
    {
        while (true)
        {
            
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

    public IEnumerator ComportamientoAldeano(VillagerStruct gameStruct)
    {
        while (true)
        {
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

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
