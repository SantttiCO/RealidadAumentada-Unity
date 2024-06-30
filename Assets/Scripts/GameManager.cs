using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    
    public event Action OnTryitMenu;
    public event Action OnScanMenu;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        ScanMenu();

    }


   
    public void ScanMenu()
    {
        OnScanMenu?.Invoke();
        Debug.Log("Scan Menu Activated");

    }
    public void TryitMenu()
    {
        OnTryitMenu?.Invoke();// esto se hace con los botones que estan en el canvas 
        Debug.Log("Try it Menu Activated");

    }

    

    // Update is called once per frame
    public void Close() //Finaliza la aplicación
    {
        Application.Quit();
    }
}
