using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Drawing;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using DG.Tweening;

public class UImanager : MonoBehaviour
{
    [SerializeField] private GameObject scanMenuCanvas;
    [SerializeField] private GameObject tryitCanvas;
    [SerializeField] private ARSession arSession;
    [SerializeField] private AndroidCodeReaderToggleableSample codeReader;
    [SerializeField] private ARCameraManager cameraManager;


    void Start()
    {
        GameManager.Instance.OnScanMenu += ActivateScanMenu;
        GameManager.Instance.OnTryitMenu += ActivateTryit;

       
    }



    private void ActivateScanMenu()
    {
        
        scanMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        scanMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        scanMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        scanMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1, 1, 1), 0.5f);

        tryitCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0  , 0, 0), 0.5f);

        cameraManager.requestedFacingDirection = CameraFacingDirection.World;
        codeReader.StartScanning();

    }

    private void ActivateTryit()
    {
        

        scanMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        scanMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        scanMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        scanMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        
        tryitCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.5f);

        codeReader.StopScanning();

        cameraManager.requestedFacingDirection = CameraFacingDirection.User;


    }


}