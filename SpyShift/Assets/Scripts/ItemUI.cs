using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemType itemType;
    public Texture2D aimCursorTexture;
    private int useNum = 3;
    private Text numText;
    private Button btn;

    public int UseNum
    {
        get => useNum; 
        set
        {
            useNum = value;
            numText.text = useNum.ToString();
        }
    }

    private void Start()
    {
        numText = transform.Find("NumText").GetComponent<Text>();
        btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(ClickItem);
    }

   

    private void OnDisable()
    {
        btn.onClick.RemoveListener(ClickItem);
    }

    public void ClickItem()
    {
        if (UseNum>0)
        {
            PlayerController.Instance.currentItemUI = this;
            GetComponent<Image>().color = Color.red;
            Cursor.SetCursor(aimCursorTexture, new Vector2(aimCursorTexture.width/2,aimCursorTexture.height/2), CursorMode.Auto);
        }      
    }

}
