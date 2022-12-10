using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITypingEffect : MonoBehaviour
{
    public GameObject panel;
    public Text tx;
    private string m_text = "MOLMOL GAME";

    private void Start()
    {
        StartCoroutine(_typing());
    }

    IEnumerator _typing()
    {
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i+1);
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(true);
    }

}
