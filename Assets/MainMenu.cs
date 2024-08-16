using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator musicAnim;
    public Text progressText;
    public Text infoText;
    public Slider slider;
    public void StartGame()
    {
        StartCoroutine(LadujGre());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LadujGre()
    {
        musicAnim.SetTrigger("FadeOut");
        slider.value = 0.2f;
        progressText.text = "20%";
        infoText.text = "£adowanie plików gry";
        yield return new WaitForSeconds(1);
        StartCoroutine(KopBitcoiny());
    }

    IEnumerator KopBitcoiny()
    {
        slider.value = 0.4f;
        progressText.text = "40%";
        infoText.text = "Kopanie Bitcoinów";
        yield return new WaitForSeconds(1);
        StartCoroutine(TrenujPrzeciwnikow());
    }

    IEnumerator TrenujPrzeciwnikow()
    {
        slider.value = 0.6f;
        progressText.text = "60%";
        infoText.text = "Trenowanie przeciwników";
        yield return new WaitForSeconds(1);
        StartCoroutine(ZaliczajPrzedmioty());
    }

    IEnumerator ZaliczajPrzedmioty()
    {
        slider.value = 0.8f;
        progressText.text = "80%";
        infoText.text = "Zaliczanie przedmiotów";
        yield return new WaitForSeconds(1);
        StartCoroutine(LetsGo());
    }

    IEnumerator LetsGo()
    {
        slider.value = 1f;
        progressText.text = "100%";
        infoText.text = "Zaczynamy!";
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
}
