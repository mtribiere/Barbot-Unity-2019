using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMasterMain : MonoBehaviour
{
    void Start()
    {
        //Demarrer la transition d'entree
        StartCoroutine(enterAnimation());

        //Demarrer la lecture sur arduino
        ArduinoCommunicatorScript.instance.startArduinoRead();
    }

    void Update()
    {
        //Detecter l'appui sur Espace
        if (Input.GetKeyDown("space"))
        {  
            StartCoroutine(exitAnimation(0));
        }

        
        //Verifier si un message est disponible
        if (ArduinoCommunicatorScript.instance.getIsMessageReceived())
        {
            //Si le jeton est tombé
            if(ArduinoCommunicatorScript.instance.getMessage()[0] == 'R')
            {
                GameObject.FindObjectOfType<ArrowResultScript>().OnItemSelected(charToInt(ArduinoCommunicatorScript.instance.getMessage()[1]));

                //Relancer la lecture
                ArduinoCommunicatorScript.instance.startArduinoRead();
            }

            //Si le verre est rempli
            if(ArduinoCommunicatorScript.instance.getMessage()[0] == 'D')
            {
                StartCoroutine(exitAnimation(0));
            }

            ArduinoCommunicatorScript.instance.clearFlag();
        }

    }

    //Conversion de char vers int
    private int charToInt(char c)
    {
        int toReturn = -1;

        switch (c)
        {
            case '0':
                toReturn = 0;
                break;
            case '1':
                toReturn = 1;
                break;
            case '2':
                toReturn = 2;
                break;
            case '3':
                toReturn = 3;
                break;

            default:
                toReturn = -1;
                break;
        }

        return toReturn;
    }


    //Animation d'entree de scene
    private IEnumerator enterAnimation()
    {
        while (transitionImage.GetComponent<RectTransform>().offsetMin.y >= -990)
        {
            yield return new WaitForSeconds(0.01f);
            transitionImage.GetComponent<RectTransform>().offsetMin = Vector3.Lerp(transitionImage.GetComponent<RectTransform>().offsetMin, new Vector2(transitionImage.GetComponent<RectTransform>().offsetMin.x, -1000), transitionSpeed * Time.deltaTime);
            transitionImage.GetComponent<RectTransform>().offsetMax = Vector3.Lerp(transitionImage.GetComponent<RectTransform>().offsetMax, new Vector2(transitionImage.GetComponent<RectTransform>().offsetMax.x, -1000), transitionSpeed * Time.deltaTime);
        }
    }


    //Animation de sortie de scene
    private IEnumerator exitAnimation(int NextSceneIndex)
    {

        yield return new WaitForSeconds(5f);

        while (transitionImage.GetComponent<RectTransform>().offsetMin.y <= -10)
        {
            yield return new WaitForSeconds(0.01f);
            transitionImage.GetComponent<RectTransform>().offsetMin = Vector3.Lerp(transitionImage.GetComponent<RectTransform>().offsetMin, new Vector2(transitionImage.GetComponent<RectTransform>().offsetMin.x, 0), transitionSpeed * Time.deltaTime);
            transitionImage.GetComponent<RectTransform>().offsetMax = Vector3.Lerp(transitionImage.GetComponent<RectTransform>().offsetMax, new Vector2(transitionImage.GetComponent<RectTransform>().offsetMax.x, 0), transitionSpeed * Time.deltaTime);
        }
        //Attendre une seconde
        yield return new WaitForSeconds(1f);

        //Charger la scene suivante
        SceneManager.LoadScene(NextSceneIndex);
    }

    //Variables
    public float transitionSpeed = 5f;

    //Lien vers l'image de transition
    public GameObject transitionImage;

}
