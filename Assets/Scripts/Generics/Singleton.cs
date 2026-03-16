using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    //Serve per verificare se il gioco sta chiudendo
    private static bool isApplicationQuit = false;

    //indica se L'Oggetto debba essere distrutto alla prossima scena o debba rimanere
    protected virtual bool ShouldBeDestoyOnLoad() => false;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //Se la instanza è nulla La cerco per tutto il gioco anche nei gmaeObject disattivati
                instance = FindAnyObjectByType<T>(FindObjectsInactive.Include);

                //se La istanza non esiste e quindi è null e il gioco non sta Chiudendo, creo la istanza
                if (instance == null && !isApplicationQuit)
                {
                    //qui invece sto creando dei Singleton da Zero
                    GameObject singleton = new GameObject(typeof(T).ToString());
                    instance = singleton.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            //se l'istanza non è collegata la collego
            instance = GetComponent<T>();

            //se deve esistere in più scene la rendo "Indistrutibile"
            if (!ShouldBeDestoyOnLoad())
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        //Se è già presente questo gameObject non deve esistere
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //qui distruggo rendo nulla la istanza, non la distruggo
        if (instance == this)
        {
            instance = null;
        }
    }
    protected virtual void OnApplicationQuit()
    {
        //Semplicemente indico che il gioco sta chiudendo al singleton, in modo tale che se viene richiamto evita di rinascere 
        isApplicationQuit = true;
    }
}
