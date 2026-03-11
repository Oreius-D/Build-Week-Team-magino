using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaConsumabile : MonoBehaviour
{
    [SerializeField] private SO_ConsumableUpgrade _useUpgrade;
     // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (InventoryManager.Instance.HaveConsumable(_useUpgrade)) 
            {
                InventoryManager.Instance.UseUpgrade(_useUpgrade);
                Debug.Log($"Oggetto usato");
            }
            else
            {
                Debug.Log($"Non hai l'oggeto");
            } 
        }
    }
}
