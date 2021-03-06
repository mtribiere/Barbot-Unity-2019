﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMasterMenu : MonoBehaviour
{

    //Au debut de la scene
    public void Start()
    {
        StartCoroutine(enterAnimation());

        //Demarrer la lecture sur arduino
        ArduinoCommunicatorScript.instance.startArduinoRead();
    }

    public void Update()
    {
        //Detecter l'appui sur Espace pour changer de scene
        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(exitAnimation(2));
        }

        //Verifier si un message est disponible
        if (ArduinoCommunicatorScript.instance.getIsMessageReceived())
        {
            //Si le bouton Play est appuyé
            if(ArduinoCommunicatorScript.instance.getMessage() == "P")
            {
                StartCoroutine(exitAnimation(2));
            }

            ArduinoCommunicatorScript.instance.clearFlag();
            
        }
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
        while(transitionImage.GetComponent<RectTransform>().offsetMin.y <= -10)
        {
            yield return new WaitForSeconds(0.01f);
            transitionImage.GetComponent<RectTransform>().offsetMin = Vector3.Lerp(transitionImage.GetComponent<RectTransform>().offsetMin, new Vector2(transitionImage.GetComponent<RectTransform>().offsetMin.x, 0), transitionSpeed * Time.deltaTime);
            transitionImage.GetComponent<RectTransform>().offsetMax = Vector3.Lerp(transitionImage.GetComponent<RectTransform>().offsetMax, new Vector2(transitionImage.GetComponent<RectTransform>().offsetMax.x, 0), transitionSpeed * Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(NextSceneIndex);
    }

    /*
    //Animation de sortie de scene
    private IEnumerator exitAnimation(int NextSceneIndex)
    {
        for(int i = 0; i <= 100; i+=2)
        {
            transitionImage.GetComponent<Image>().color = new Color(0f, 0f, 0f, (i / 100f));
            yield return new WaitForSeconds(0.01f);
        }
    }
    */

    //Variables
    public float transitionSpeed = 5f; 

    //Lien vers l'image de transition
    public GameObject transitionImage;


}
