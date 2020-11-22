using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolsDictionary : MonoBehaviour
{
    public static Dictionary<string, string> symbolsDictionary;
    public static string formula;
    // Start is called before the first frame update
    void Start()
    {
        symbolsDictionary = new Dictionary<string, string>();
        symbolsDictionary.Add("+", "%2B");///must
        symbolsDictionary.Add("/", "%2F");
        symbolsDictionary.Add("!", "%21");
        symbolsDictionary.Add("(", "%28");
        symbolsDictionary.Add(")", "%29");
        symbolsDictionary.Add("/", "%2F");
        symbolsDictionary.Add("GroupS", @"\mathbb{");
        symbolsDictionary.Add("GroupE", @"}");
        symbolsDictionary.Add("&", "%26"); ///must
        //\geq -> '>=' , \leq -> '<='
        /*
         * \sqrt{49}
            \sum
            \subset
            \subseteq
            \supset
            \supseteq
            \int
            \bigcup
            \bigcap
            \in
            \neq
            \pi
            \forall
            \infty
            \imath
        */
    }

    // Update is called once per frame
    void Update()
    {

    }
}
//http://milde.users.sourceforge.net/LUCR/Math/mathpackages/amsfonts-symbols.pdf
//https://en.wikipedia.org/wiki/TeX
/******/
//https://www.xm1math.net/texmaker/download.html
/******/
