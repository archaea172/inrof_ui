using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManagerController : MonoBehaviour
{
    public GameObject NavUi;
    public GameObject picture;
    // Start is called before the first frame update
    void Start()
    {
        ShowNavUi();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNavUi()
    {
        NavUi.SetActive(true);
        picture.SetActive(false);
    }
    
    public void Showpicture()
    {
        NavUi.SetActive(false);
        picture.SetActive(true);
    }
}
