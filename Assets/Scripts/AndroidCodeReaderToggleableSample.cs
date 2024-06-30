using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;

public class AndroidCodeReaderToggleableSample : MonoBehaviour
{

    [SerializeField] private ARCameraManager cameraManager;
    

    [SerializeField] private TextMeshProUGUI debugResultText;



    [SerializeField] private GameObject PrefabShirt;
    [SerializeField] private GameObject PrefabCap;
    [SerializeField] private GameObject PrefabGlasses;
    public GameObject panelUI;


    [SerializeField] private Button botoncito;



    private GameObject PlacedObject;

    
    

    private Texture2D cameraImageTexture;
    private bool scanningEnabled = false;

    

    private List<GameObject> placedObjects = new List<GameObject>();

    List<Product> productos = new List<Product>();

    private void Start()
    {
        panelUI.SetActive(false);
        
        productos.Add(new Product(1000001, "Gorra TEC", "Ropa", "Gorra azul, letras TEC blancas ", new List<string> { "S", "M", "L" }));
        productos.Add(new Product(1000002, "Camisa", "Ropa", "Camisa blanca estilo polo", new List<string> { "S", "M", "L", "XL" }));
        productos.Add(new Product(1000003, "Lentes de sol", "Accesorios", "Par de gafas oscuras de sol", new List<string> { "<121", "121-124", "125-128", "129-132", "133-136", "137-140", ">140" }));

    }

    private BarcodeReader barcodeReader = new BarcodeReader
    {
        AutoRotate = false,
        Options = new ZXing.Common.DecodingOptions
        {
            TryHarder = false,
            PossibleFormats = new List<BarcodeFormat> {
            BarcodeFormat.CODE_128,
            BarcodeFormat.CODE_39,
            BarcodeFormat.QR_CODE
            }
        }
    };



    



    private Result result;

    /*
    private void OnEnable() {
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    private void OnDisable() {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }
    */

    public void StartScanning()
    {
        scanningEnabled = true;
    }

    public void stope()
    {
        scanningEnabled = false;
        Color newColor = botoncito.GetComponent<Image>().material.color;
        newColor.a = .5f;
        panelUI.SetActive(true);

        botoncito.GetComponent<Image>().material.color = newColor;

    }

    public void StopScanning()
    {
        scanningEnabled = false;
        DestroyAllPlacedObjects();
    }



    private void Update()
    {
        if (scanningEnabled)
        {
            Color newColor = botoncito.GetComponent<Image>().material.color;
            newColor.a = 1;

            botoncito.GetComponent<Image>().material.color = newColor;

            DetectQRCode();

        }
        

    }

    public void DestroyAllPlacedObjects()
    {
        foreach (GameObject obj in placedObjects)
        {
            Destroy(obj);
            obj.SetActive(false);
        }
        placedObjects.Clear();
    }





    private void DetectQRCode()
    {



        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image)) //obtiene la ultima imagen de la camara
        {
            return;
        }

        var conversionParams = new XRCpuImage.ConversionParams  //parametrización de conversion de imagen
        {
            // Get the entire image.
            inputRect = new RectInt(0, 0, image.width, image.height),

            // Downsample by 2.
            outputDimensions = new Vector2Int(image.width / 2, image.height / 2),

            // Choose RGBA format.
            outputFormat = TextureFormat.RGBA32,

            // Flip across the vertical axis (mirror image).
            transformation = XRCpuImage.Transformation.MirrorY
        };

        // See how many bytes you need to store the final image.
        int size = image.GetConvertedDataSize(conversionParams);

        // Allocate a buffer to store the image.
        var buffer = new NativeArray<byte>(size, Allocator.Temp);

        // Extract the image data
        image.Convert(conversionParams, buffer);

        // The image was converted to RGBA32 format and written into the provided buffer
        // so you can dispose of the XRCpuImage. You must do this or it will leak resources.
        image.Dispose();

        // At this point, you can process the image, pass it to a computer vision algorithm, etc.
        // In this example, you apply it to a texture to visualize it.

        // You've got the data; let's put it into a texture so you can visualize it.    Se crea una textura para su procesamiento
        cameraImageTexture = new Texture2D(
            conversionParams.outputDimensions.x,
            conversionParams.outputDimensions.y,
            conversionParams.outputFormat,
            false);

        cameraImageTexture.LoadRawTextureData(buffer);
        cameraImageTexture.Apply();  // actualiza la textura

        // Done with your temporary data, so you can dispose it.
        buffer.Dispose();

        // Detect and decode the barcode inside the bitmap
        result = barcodeReader.Decode(cameraImageTexture.GetPixels32(), cameraImageTexture.width, cameraImageTexture.height); //zxing






        // Ahora instancia los jugadores en posiciones relativas al estadio.




        // Do something with the result
        if (result != null)
        {
            int numero;
            int.TryParse(result.Text, out numero);
            Product productoEncontrado = productos.Find(p => p.GetCode() == numero);

            string mensaje = "Código: " + productoEncontrado.GetCode() + "\n" +
                                 "Nombre: " + productoEncontrado.GetName() + "\n" +
            "Tipo: " + productoEncontrado.GetType() + "\n" +
                                 "Descripción: " + productoEncontrado.GetDescription() + "\n" +
                                 "Tallas: ";
            foreach (string talla in productoEncontrado.GetTallas())
            {
                mensaje += talla + " ";
            }

            debugResultText.text = mensaje;

            Vector3 cameraPosition = cameraManager.transform.position;

            // Comparar con las palabras "cap" y "shirt"
            switch (result.Text)
            {
                case "1000001":
                    // Construir el objeto Cap y colocarlo cerca de la cámara
                    PlacedObject = Instantiate(PrefabCap, cameraPosition + new Vector3(0, 0, 1), Quaternion.identity);
                    PlacedObject.tag = "PlacedObject"; // Asignar la etiqueta "PlacedObject"
                    PlacedObject.AddComponent<ClickDrag>();
                    
                    placedObjects.Add(PlacedObject);
                    break;
                case "1000002":
                    // Construir el objeto Shirt y colocarlo cerca de la cámara
                    PlacedObject = Instantiate(PrefabShirt, cameraPosition + new Vector3(0, 0, 1), Quaternion.identity);
                    PlacedObject.tag = "PlacedObject"; // Asignar la etiqueta "PlacedObject"
                    PlacedObject.AddComponent<ClickDrag>();
                    
                    break;
                case "1000003":
                    PlacedObject = Instantiate(PrefabGlasses, cameraPosition + new Vector3(0, 0, 1), Quaternion.identity);
                    PlacedObject.tag = "PlacedObject"; // Asignar la etiqueta "PlacedObject"
                    PlacedObject.AddComponent<ClickDrag>();
                    
                    break;
                default:
                    // Manejo para otros casos
                    break;
            }

            MeshCollider mc = PlacedObject.GetComponent<MeshCollider>();
            mc.convex = true;


            stope();

            

        }
    }

    public void ToggleScanning()
    {
        scanningEnabled = !scanningEnabled;
    }



    public string GetCurrentState()
    {
        return "Is Scanner running? - " + scanningEnabled;
    }
}