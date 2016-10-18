﻿using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField]
    private Text m_Text;

    private string m_DescriptionText;

    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        m_Text.text = "Please set using the SetText(string)";

        iTweenManager.Show_ScaleTo(this.gameObject, 0.2f);
    }

    void Update()
    {
        m_Text.text = m_DescriptionText;
        transform.SetAsLastSibling();
    }

    public void Button_OK()
    {
        iTweenManager.Hide_ScaleTo(this.gameObject, 0.2f, "EndAction", this.gameObject);
    }

    void EndAction()
    {
        Destroy(this.gameObject);
    }

    public void SetText(string text)
    {
        m_DescriptionText = text;
    }
}