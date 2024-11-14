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


    // TitleScene의 처음 시작할 때 ButtonEvent
    public void ClickTitleEvent()
    {
        myBook.GetComponent<BookCoverScript>().OpenCover();
        myBook.GetComponent<RecipeBookController>().MovingCamera();
    }
}