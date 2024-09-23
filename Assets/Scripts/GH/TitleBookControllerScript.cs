using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBookControllerScript : MonoBehaviour
{
    private GameObject myBook;

    void Start()
    {
        myBook = GetComponent<Transform>().GetChild(0).gameObject;
    }


    public void ClickTitleEvent()
    {
        myBook.GetComponent<BookCoverScript>().OpenCover();
        myBook.GetComponent<RecipeBookController>().MovingCamera();
    }
}