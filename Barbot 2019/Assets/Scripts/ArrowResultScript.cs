using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowResultScript : MonoBehaviour {

    void Start()
    {
        //Definir le debut de l'animation
        targetPosition = new Vector2(itemHolder.transform.GetChild(currentItemIndex).transform.position.x + 1.5f, itemHolder.transform.GetChild(currentItemIndex).transform.position.y);

        //Demarrer l'animation de la fleche
        StartCoroutine(arrowAnimation());

      
    }

    void Update()
    {
        //Bouger la fléche
        arrow.transform.position = Vector3.Lerp(arrow.transform.position, targetPosition,animationSpeed * Time.deltaTime);
    }

    //Quand l'item est choisi
    public void OnItemSelected(int id)
    {
        selectedItemId = id;
        isItemSelected = true;
    }

    //Animation de fleche qui bouge
    private IEnumerator arrowAnimation()
    {
        //Tant que l'item n'est pas choisi
        while (!isItemSelected)
        {
            //Si on est en bas, aller en haut et inversement
            if (currentItemIndex == 0)
                currentItemIndex = itemHolder.transform.childCount-1;
            else
                currentItemIndex = 0;
            
            //Definir la position de destination
            targetPosition = new Vector2(itemHolder.transform.GetChild(currentItemIndex).transform.position.x + 1.5f, itemHolder.transform.GetChild(currentItemIndex).transform.position.y);
            //Debug.Log(currentItemIndex);

            yield return new WaitForSeconds(0.5f);
        }

        //Placer la fleche devant l'item choisi
        targetPosition = new Vector2(itemHolder.transform.GetChild(selectedItemId).transform.position.x + 1.5f, itemHolder.transform.GetChild(selectedItemId).transform.position.y);

        //Animer l'item choisi
        itemHolder.transform.GetChild(selectedItemId).GetComponent<Animator>().enabled = true;

        //Animer la fleche
        arrow.GetComponent<Animator>().enabled = true;

    }

    //Variables
    public float animationSpeed = 5f;

    private Vector3 targetPosition = Vector3.zero;
    private int currentItemIndex = 0;
    private int selectedItemId = 0;
    private bool isItemSelected = false;

    //Lien vers la fleche
    public GameObject arrow;

    //Lien vers les items
    public GameObject itemHolder;

    
}
