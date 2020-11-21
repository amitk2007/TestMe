using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AddFormulaImage : MonoBehaviour
{
    //\displaystyle\Large\int_{-\infty}^{\infty}e^{-x^{2}}\;dx=\left(\frac{a}{b}\right)\sqrt{M\cdot\pi}\hspace{30mm}x\in\mathbb{R}\hspace{30mm}(1)
    //https://stackoverflow.com/questions/36895801/render-latex-code-with-chart-apis-google-com
    //\displaystyle\int_{-\infty}^{\infty}e^{-x^{2}}\;dx=x^2

    [SerializeField] string formula = @"\displaystyle\int_{-\infty}^{\infty}e^{-x^{2}}\;dx=\sqrt{\pi}";
    public Image formulaImage;

    Texture2D texture = null;
    bool draw = false;

    IEnumerator Start()
    {
        formula = formula.Replace(" ", "");
        WWW www = new WWW("http://chart.apis.google.com/chart?cht=tx&chl=" + formula);
        yield return www;

        texture = www.texture;

        Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        formulaImage.sprite = s;
        draw = true;
        #region Remove Alfa - now in use - change image color insted
        /*texture.alphaIsTransparency = true;

        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                if (texture.GetPixel(i, j).Between(Color.white, new Color(0.5f, 0.5f, 0.5f)))
                {
                    texture.SetPixel(i, j, Color.clear);
                }
                else if (texture.GetPixel(i, j).Smaller(new Color(0.9f, 0.9f, 0.9f)))
                {
                    texture.SetPixel(i, j, Color.black);
                    print("asd");
                }
            }
        }*/

        //formulaImage.texture = texture;

        //formulaImage.sprite = s;
        #endregion
    }

    //void OnGUI()
    //{
    //    if (texture != null && draw)
    //        GUI.DrawTexture(new Rect(0, 0, texture.width, texture.height), texture);
    //}
}

public static class Extentions
{
    public static bool Between(this Color color, Color col1, Color col2)
    {
        if (Mathf.Max(col1.r, col2.r) >= color.r && Mathf.Min(col1.r, col2.r) <= color.r)
        {
            if (Mathf.Max(col1.g, col2.g) >= color.g && Mathf.Min(col1.g, col2.g) <= color.g)
            {
                if (Mathf.Max(col1.b, col2.b) >= color.b && Mathf.Min(col1.b, col2.b) <= color.b)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool Bigger(this Color color, Color col)
    {
        if (col.r <= color.r)
        {
            if (col.g <= color.g)
            {
                if (col.b <= color.b)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public static bool Smaller(this Color color, Color col)
    {
        if (col.r >= color.r)
        {
            if (col.g >= color.g)
            {
                if (col.b >= color.b)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
