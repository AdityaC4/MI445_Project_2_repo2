using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> pages = new List<GameObject>();
    int curPage;
    private void Start()
    {
        curPage = 0;
        SetPage(curPage);
    }

    public void SetPage(int page)
    {
        pages[curPage].SetActive(false);
        pages[page].SetActive(true);
        curPage = page;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
