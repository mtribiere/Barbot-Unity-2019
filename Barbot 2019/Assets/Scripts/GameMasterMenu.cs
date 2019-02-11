using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterMenu : MonoBehaviour
{

    //Au debut de la scene
    public void Start()
    {
        StartCoroutine(enterAnimation());        
    }

    public void Update()
    {
        //Detecter l'appui sur Espace
        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(exitAnimation(1));
        }
    }

    //Animation d'entree de scene
    private IEnumerator enterAnimation()
    {
        for (int i = 100; i >= 0; i -= 2)
        {
            transitionImage.GetComponent<Image>().color = new Color(0f, 0f, 0f, (i / 100f));
            yield return new WaitForSeconds(0.01f);
        }
    }



    //Animation de sortie de scene
    private IEnumerator exitAnimation(int NextSceneIndex)
    {
        for(int i = 0; i <= 100; i+=2)
        {
            transitionImage.GetComponent<Image>().color = new Color(0f, 0f, 0f, (i / 100f));
            yield return new WaitForSeconds(0.01f);
        }
    }


    //Variables

    //Lien vers l'image de transition
    public GameObject transitionImage;


}
