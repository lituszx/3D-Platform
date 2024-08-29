using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LateralMode : MonoBehaviour
{
    private bool isFadeToBlack, isFadeFromBlack;
    public Image blackScreen;
    public float fadeSpeed;
    public float waitForFade;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }
        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }
    public IEnumerator Fade2()
    {
        isFadeToBlack = true;
        StartCoroutine("CameraChangeTo2");
        yield return new WaitForSeconds(waitForFade);
        isFadeToBlack = false;
        isFadeFromBlack = true;
    }
    public IEnumerator Fade3()
    {
        isFadeToBlack = true;
        StartCoroutine("CameraChangeTo3");
        yield return new WaitForSeconds(waitForFade);
        isFadeToBlack = false;
        isFadeFromBlack = true;
    }
    public IEnumerator CameraChangeTo2()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<ClasseCamera>().maxR = 1000;
        player.GetComponent<ClasseCamera>().height = 2;
    }
    public IEnumerator CameraChangeTo3()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<ClasseCamera>().maxR = 5;
        player.GetComponent<ClasseCamera>().height = 4;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("Fade2");                     
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("Fade3");          
        }
    }
}
