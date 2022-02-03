using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitBergerUpdater : MonoBehaviour
{
    [SerializeField]
    public GameObject textBox,doneButton,loadingIcon;

    public IEnumerator UpdateGitBerger()
    {
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Initializing...";
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Searching for Updates...";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Downloading Update...";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Installing Update...";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Your GitBerger client is Uptodate!";
        doneButton.SetActive(true);
        loadingIcon.SetActive(false);
    }

    public void ToggleUpdateWindow()
    {
        if(gameObject.activeSelf)
        {
            StopAllCoroutines();
            doneButton.SetActive(false);
            gameObject.SetActive(false);
        }
        else
        {
            loadingIcon.SetActive(true);
            gameObject.SetActive(true);
            StartCoroutine(UpdateGitBerger());
            
        }
    }
}
