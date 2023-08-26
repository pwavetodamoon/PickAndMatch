using UnityEngine;

public sealed class UIHeplerBackFround : MonoBehaviour
{
    private void Awake()
    {
        FixedSize();
    }
    void FixedSize()
    {
        float aspectio = (Screen.width / Screen.height);
        //Kich thuoc cua hinh background
        float bGWidth = 1168.0f;  
        //kich thuoc cua background
        float bGHeight = 2048.0f;
        float offsetX = 88.0f;      //dieu nay, de dam bao ti le hinh anh duoc scale voi ti le dep hon
        float offsetY = 128.0f;     //-------------------//------------------------------------------

        if (aspectio > 0.5)
        {
            transform.localScale = new Vector3((Screen.width / (bGWidth - offsetX)), Screen.height / (bGHeight - offsetY), 0);
        }
    }
}
