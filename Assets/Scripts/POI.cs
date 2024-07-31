using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class POI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text poiName;

    [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        poiName.text = this.gameObject.name;
    }

    public void showName()
    {
        panel.SetActive(true);
    }
}
