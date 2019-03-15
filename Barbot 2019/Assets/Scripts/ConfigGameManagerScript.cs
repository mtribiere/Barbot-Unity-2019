using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ConfigGameManagerScript : MonoBehaviour
{

    public void OnClickConfirmButton()
    {
        Instantiate(arduinoComPrefabs);
        ArduinoCommunicatorScript.instance.startArduinoConnection("COM"+textInput.GetComponent<InputField>().text);
        SceneManager.LoadScene(1);
    }

    //Lien vers la barre de texte
    public GameObject textInput;

    //Lien vers le prefab du gestionnaire de com arduino
    public GameObject arduinoComPrefabs;
}
