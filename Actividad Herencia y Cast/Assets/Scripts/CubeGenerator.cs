using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;
using NPC.Ally;
using TMPro;

public class CubeGenerator : MonoBehaviour
{
    static System.Random r = new System.Random(); // VARIABLE AUXILIAR PARA DECLARAR E INICIALIZAR EL READONLY
    public readonly int limiteMinimo = r.Next(5,15); // LINEA NATIVA PARA ASIGNAR UN LIMITE ALEATORIO AL READONLY
    const int limiteMaximo = 25; // CONSTANTE PARA LA GENERACION MAXIMA DE CUBOS
    int nAlly = 0, nEnemy = 0, limiteGenerado,generadorRandom; // VARIABLES PARA LA GENERACION DE CUBOS
    // HEROE VARIABLES Y FUNCION GENERADORA
    public GameObject cuboHeroe;
    public GameObject heroe;
    public GameObject camaraHeroe;
    GameObject camara;
    Vector3 posHero;
    Vector3 camPos;
    GameObject enemys;
    GameObject allys;

    public GameObject heroObject;

    // VARIABLES DEL TEXTO DEL CANVAS
    public TextMeshProUGUI nEnemigos; 
    public TextMeshProUGUI nAliados;
    

    public void CreacionHeroe() // FUNCION GENERADORA DEL HEROE
    {
        // CREACION DEL HEROE
        posHero = new Vector3(Random.Range(-40.0f, -34.0f), 0.0f, Random.Range(-40.0f, -34.0f)); // CALCULA UNA POSICION
        heroe = GameObject.Instantiate(cuboHeroe, posHero, Quaternion.identity); // INSTANCIA AL HEROE EN ESCENA
        heroe.name = "Heroe"; // LO NOMBRA EN LA JERARQUIA DE UNITY
        heroe.AddComponent<MyHero>(); // LE AÑADE EL COMPONENTE DE HEROE CON SUS DATOS
        heroe.AddComponent<HeroMove>(); // LE AÑADE EL COMPONENTE DE MOVIMIENTO


        // CREACION DE LA CAMARA QUE SIGUE AL HEROE
        camPos = new Vector3(heroe.transform.position.x, heroe.transform.position.y + 0.8f, heroe.transform.position.z); // CALCULA UNA POSICION
        camara = Instantiate(camaraHeroe, camPos, Quaternion.identity); // iNSTANCIA A LA CAMARA EN ESCENA
        camara.AddComponent<HeroCam>(); // LE AÑADE EL COMPONENTE CON LAS FUNCIONES DE LA CAMARA
        camara.name = "Camara Heroe"; // LO NOMBRA EN LA JERARQUIA DE UNITY
        camara.transform.SetParent(heroe.transform); // ASIGNA A LA CAMARA COMO HIJA DEL HEROE PARA QUE LO SIGA
    }

    // ZOMBIE VARIABLES Y FUNCION GENERADORA
    int colorZombie;
    public GameObject zombie;
    public GameObject mensaje;
    public GameObject mensajeZombi;

    public void CreacionZombie(GameObject enemigos) // FUNCION GENERADORA DE LOS ZOMBIES
    {
        zombie = GameObject.CreatePrimitive(PrimitiveType.Cube); // INSTANCIA UN CUBO COMO ZOMBIE EN LA ESCENA
        zombie.name = "Zombie"; // LO NOMBRA EN LA JERARQUIA DE UNITY
        zombie.transform.SetParent(enemigos.transform); // ASIGNA AL ZOMBIE COMO HIJO DE UN GRUPO QUE CONTIENE SOLO A LOS ZOMBIES
        Vector3 posZombi = new Vector3(Random.Range(-14.0f, 14.0f), 0.0f, Random.Range(-14.0f, 14.0f)); // CALCULA LA POSICION INICIAL DEL ZOMBIE EN ESCENA
        zombie.transform.position = posZombi; // UBICA AL ZOMBIE EN LA POSICION CALCULADA
        zombie.AddComponent<Rigidbody>(); // AÑADE UN COMPONENTE DE CUERPO RIGIDO AL GAMEOBJECT
        zombie.GetComponent<Rigidbody>().freezeRotation = true; // CONGELA LA ROTACION POR INERCIA DE FISICAS DE UNITY

        mensajeZombi = Instantiate(mensaje); // CREA UN OBJETO MENSAJE
        mensajeZombi.name = "Mensaje"; // LO NOMBRA EN LA JERARQUIA DE UNITY
        mensajeZombi.transform.SetParent(zombie.transform); // EL MENSAJE SE VUELVE HIJO DE ZOMBIE
        mensajeZombi.transform.localPosition = Vector3.zero; // UBICA EN EL ORIGEN DE ZOMBIE
        mensajeZombi.transform.localPosition = Vector3.up; // LO SUBE UNA UNIDAD EN Y       
        zombie.AddComponent<MyZombie>(); // AÑADA EL COMPONENTE ZOMBIE CON SUS DATOS
        
        switch (zombie.GetComponent<MyZombie>().datosZombie.colorZombi) // UNA VEZ OBTENIDOS LOS DATOS EN LA LINEA ANTERIOR SE LE DA UN COLOR AL ZOMBIE
        {
            case 0:
                zombie.GetComponent<Renderer>().material.color = Color.cyan; // SE LE ASIGNA COLOR AZUL
                break;
            case 1:
                zombie.GetComponent<Renderer>().material.color = Color.green; // SE LE ASIGNA COLOR VERDE
                break;
            case 2:
                zombie.GetComponent<Renderer>().material.color = Color.magenta; // SE LE ASIGNA COLOR ROSA
                break;
        }
    }

    // ALDEANO VARIABLES Y FUNCION GENERADORA
    public GameObject aldeano;
    public GameObject mensajeAldeano;
    public void CreacionAldeano(GameObject aliados)
    {
        aldeano = GameObject.CreatePrimitive(PrimitiveType.Cube); // CREA LA FIGURA SOLICITADA
        Vector3 posAldeano = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f)); // ELIGE UNA POSICION ALEATORIA
        aldeano.transform.position = posAldeano; // ASIGNA LA POSICION A UNA VARIABLE
        aldeano.GetComponent<Renderer>().material.color = Color.black; // ASIGNACION DE UN COLOR AL ALDEANO
        aldeano.GetComponent<Transform>().localScale = new Vector3(1.0f, 2.0f, 1.0f); // ASIGNA UN COLOR PARA IDENTIFICAR A LOS ALDEANOS
        aldeano.AddComponent<Rigidbody>().freezeRotation = true; // AÑADE CUERPO SOLIDO AL ZOMBIE Y CONGELA LA ROTACION 
        aldeano.name = "Aldeano"; // NOMBRE DEL ALDEANO EN LA JERARQUIA
        aldeano.transform.SetParent(aliados.transform); // ALDEANO SE VUELVE HIJO DEL GRUPO DE ALIADOS

        mensajeAldeano = Instantiate(mensaje); // CREA UN OBJETO MENSAJE
        mensajeAldeano.name = "Mensaje"; // LO NOMBRA EN LA JERARQUIA DE UNITY
        mensajeAldeano.transform.SetParent(aldeano.transform); // LO VUELVE HIJO DE ALDEANO
        mensajeAldeano.transform.localPosition = Vector3.zero; // UBICA EN EL ORIGEN DE ALDEANO
        mensajeAldeano.transform.localPosition = Vector3.up; // LO SUBE UNA UNIDAD EN Y   
        aldeano.AddComponent<MyVillager>(); // AÑADA EL COMPONENTE ALDEANO CON SUS DATOS
    }

    void Start()
    {
        limiteGenerado = Random.Range(limiteMinimo, limiteMaximo); // GENERA ALEATORIAMENTE EL LIMITE DE OBJETOS A CREAR
        for (int i = 0; i < limiteGenerado; i++) // CALCULA ALEATORIAMENTE EL NUMERO DE ZOMBIES Y ALDEANOS
        {
            generadorRandom = Random.Range(0, 2);
            if (generadorRandom == 0)
                nEnemy++;
            if (generadorRandom == 1)
                nAlly++;
        }
        // CREACION DEL HEROE
        CreacionHeroe();

        // CREACION DE LOS ZOMBIS
        enemys = new GameObject();
        enemys.name = "Enemys";
        for (int i = 0; i < nEnemy; i++) // CICLO QUE CREA UN ZOMBIE POR CADA ITERACION
        {
            CreacionZombie(enemys);
        }

        // CREACION DE LOS ALDEANOS
        allys = new GameObject();
        allys.name = "Allys";
        for (int i = 0; i < nAlly; i++) // CICLO QUE CREA UN ALDEANO POR CADA ITERACION
        {
            CreacionAldeano(allys);
        }
    }

    void Update() // ACTUALIZA EL CONTEO DE ALIADOS Y ENEMIGOS EN LA ESCENA
    {
        var zombieList = FindObjectsOfType<MyZombie>();
        foreach (var item in zombieList)
        {
            nEnemigos.text = zombieList.Length.ToString();
        }

        var villagerList = FindObjectsOfType<MyVillager>();
        foreach (var item in villagerList)
        {
            nAliados.text = villagerList.Length.ToString();
        }
    }
}