using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Text : MonoBehaviour
{
    public Piece piece;
    public TextMeshProUGUI text;
    public int num = 0;

    // Update is called once per frame
    void Update()
    {
        num += 1;
        //text.text = this.piece.position.x.ToString();
    }
}
