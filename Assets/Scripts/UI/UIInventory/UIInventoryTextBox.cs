using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryTextBox : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextMeshTop1 = null;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshTop2 = null;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshTop3 = null;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshBottom1 = null;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshBottom2 = null;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshBottom3 = null;

    /// <summary>
    /// Set Text Values
    /// </summary>
    /// <param name="textTop1"></param>
    /// <param name="textTop2"></param>
    /// <param name="textTop3"></param>
    /// <param name="textBottom1"></param>
    /// <param name="textBottom2"></param>
    /// <param name="textBottom3"></param>
    public void SetTextBoxText( string textTop1, string textTop2, string textTop3, string textBottom1, string textBottom2, string textBottom3 )
    {
        m_TextMeshTop1.text = textTop1;
        m_TextMeshTop2.text = textTop2;
        m_TextMeshTop3.text = textTop3;

        m_TextMeshBottom1.text = textBottom1;
        m_TextMeshBottom2.text = textBottom2;
        m_TextMeshBottom3.text = textBottom3;
    }
}
