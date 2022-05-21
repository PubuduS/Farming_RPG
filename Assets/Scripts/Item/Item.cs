using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int m_ItemCode;

    private SpriteRenderer m_SpriteRenderer;

    public int ItemCode { get => m_ItemCode; set => m_ItemCode = value; }

    private void Awake()
    {
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if( ItemCode != 0 )
        {
            Init( ItemCode );
        }
    }

    public void Init( int itemCode )
    {        
    }


}
