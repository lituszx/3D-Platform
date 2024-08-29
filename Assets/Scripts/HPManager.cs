using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPManager : MonoBehaviour
{
    public ClassePlayerControl player;
    public int maxLife, currentLife;
    public float inmune, respawn, fadeSpeed, waitForFade;
    private float inmuneCounter, renderCounter, render = 0.1f;
    public Renderer playerRenderer;
    public List<Renderer> allPlayerComponents = new List<Renderer>();
    private Vector3 respawnPoint;
    public Image blackScreen;
    private bool isRespawning, isFadeToBlack, isFadeFromBlack;
    public bool activeCapsule = false;
    public GameObject life1, life2, life3;
    void Start()
    {
        currentLife = maxLife;
        respawnPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLife == 3)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
        }
        if (currentLife == 2)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(false);
        }
        if (currentLife == 1)
        {
            life1.SetActive(true);
            life2.SetActive(false);
            life3.SetActive(false);
        }
        if (currentLife == 0)
        {
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(false);
        }
        if (inmuneCounter > 0)
        {
            inmuneCounter -= Time.deltaTime;
            renderCounter -= Time.deltaTime;
            if(renderCounter <= 0)
            {
                for (int i = 0; i < allPlayerComponents.Count; i++)
                {
                    allPlayerComponents[i].enabled = !allPlayerComponents[i].enabled;
                    renderCounter = render;
                }
                //playerRenderer.enabled = !playerRenderer.enabled;
                //renderCounter = render;
            }
            if(inmuneCounter <= 0)
            {
                for (int i = 0; i < allPlayerComponents.Count; i++)
                {
                    allPlayerComponents[i].enabled = true;
                }
                //playerRenderer.enabled = true;
            }
        }
        if(isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
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
    public void GetDamage(int _damage, Vector3 _dir)
    {
        if (inmuneCounter <= 0)
        {
            currentLife -= _damage;
            if(currentLife <= 0)
            {
                Respawn();
            }
            else
            {
                player.KnockBack(_dir);
                inmuneCounter = inmune;
                playerRenderer.enabled = false;
                renderCounter = render;
            }
        }
    }
    public void Respawn()
    {
        if(!isRespawning)
        StartCoroutine("RespawnCo");
    }
    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawn);
        isFadeToBlack = true;
        yield return new WaitForSeconds(waitForFade);
        isFadeToBlack = false;
        isFadeFromBlack = true;
        isRespawning = false;
        player.gameObject.SetActive(true);
        player.transform.position = respawnPoint;
        currentLife = maxLife;
        inmuneCounter = inmune;
        playerRenderer.enabled = false;
        renderCounter = render;
        activeCapsule = true;
    }
    public void HealPlayer(int _heal)
    {
        currentLife += _heal;
        if (currentLife > maxLife)
        {
            currentLife = maxLife;
        }
    }
    public void SetSpawnPoint(Vector3 _checkpoint)
    {
        respawnPoint = _checkpoint;
    }
}
