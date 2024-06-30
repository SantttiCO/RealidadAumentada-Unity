using TMPro;
using UnityEngine;

public class ShowScannerState : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI stateText;
    [SerializeField]
    private AndroidCodeReaderToggleableSample codeReader;

    private void Update()
    {
        if (codeReader.GetCurrentState() == "Is Scanner running? - False")
        {
            stateText.text = "[Escaneame]";
        }
        else
        {
            stateText.text = "";
        }
    }

    
}