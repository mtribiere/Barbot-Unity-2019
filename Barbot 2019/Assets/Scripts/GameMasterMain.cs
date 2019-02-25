using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMasterMain : MonoBehaviour
{
    void Start()
    {
        //Au debut de la scene
        StartCoroutine(enterAnimation());
    }

    void Update()
    {
        //Detecter l'appui sur Espace
        if (Input.GetKeyDown("space"))
        {
            GameObject.FindObjectOfType<ArrowResultScript>().OnItemSelected(1);
            StartCoroutine(exitAnimation(0));
        }
    }

    //Animation d'entree de scene
    private IEnumerator enterAnimation()
    {
        while (transitionImage.GetComponent<RectTransform>().offsetMin.y >= -640)
        {
            yield return new WaitForSeconds(0.01f);
            transitionImage.GetComponent<RectTransform>().offsetMin = Vector3.Lerp(transitionImage.GetComponent<RectTransform>().offsetMin, new Vector2(transitionImage.GetComponent<RectTransform>().offsetMin.x, -650), transitionSpeed * Time.deltaTime);
            transitionImage.GetComponent<RectTransform>().offsetMax = Vector3.Lerp(transitionImage.GetComponent<RectTransform>().offsetMax, new Vector2(transitionImage.GetComponent<RectTransform>().offsetMax.x, -650), transitionSpeed * Time.deltaTime);
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
