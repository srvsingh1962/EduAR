using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public bool isloading = false;

    [SerializeField] GameObject _sidepanel;
    [SerializeField] Image _loading;
    [SerializeField] GameObject _loadingpanel;
    [SerializeField] Image _menubutton;
    [SerializeField] Sprite _closesign;
    [SerializeField] Sprite _menusign;

    bool _panelisactive = false;

    private void Awake()
    {
        _loadingpanel.SetActive(true);
        isloading = true;
        _menubutton.sprite = _menusign;
    }

    private void Start()
    {
        _sidepanel.SetActive(false);
    }

    public void SidePanel()
    {
        if (!_panelisactive)
        {
            _menubutton.sprite = _closesign;
            _sidepanel.SetActive(true);
            _panelisactive = true;
        }
        else
        {
            _menubutton.sprite = _menusign;
            _sidepanel.SetActive(false);
            _panelisactive = false;
        }
    }

    public void Alphabet()
    {
        SceneManager.LoadScene("ARScene");
    }

    private void Update()
    {
        if (isloading)
        {
            _loading.GetComponent<Image>().fillAmount += 0.2f*Time.deltaTime;
            if (_loading.GetComponent<Image>().fillAmount == 1f)
            {
                isloading = false;
                _loadingpanel.SetActive(false);
            }
        }
    }
}