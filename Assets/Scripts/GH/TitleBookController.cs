using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TitleBookController : MonoBehaviour
{
    private GameObject myBook;

    void Start()
    {
        myBook = GetComponent<Transform>().GetChild(0).gameObject;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&
            myBook.GetComponent<BookCoverScript>().GetIsOpening() &&
            !myBook.GetComponent<RecipeBookController>().GetIsLookCloser())
        {
            myBook.GetComponent<BookCoverScript>().CloseCover();
            myBook.GetComponent<RecipeBookController>().ResetCamera();
        }
    }

    public void ClickTitleEvent()
    {
        myBook.GetComponent<BookCoverScript>().OpenCover();
        myBook.GetComponent<RecipeBookController>().MovingCamera();
    }
}
