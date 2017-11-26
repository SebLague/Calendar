using UnityEngine;
using System.Collections;

public class Calendar : MonoBehaviour {

    public int year;

    public TextMesh dateText;
	public Block block;

    string months = "January February March April May June July August September October November December";
    GameObject graphicHolder;

	Block[] blocks;

	void Start() {
        StartCoroutine(CaptureCalendar());
    }

    IEnumerator CaptureCalendar()
    {
		for (int i = 0; i < 12; i++)
		{
            yield return null;
			DrawMonth(i);
			ScreenCap(i);
		}
        yield return null;

        UnityEditor.AssetDatabase.Refresh();
    }

    void InitMonth(bool extraRow)
    {
        if (graphicHolder != null)
        {
            Destroy(graphicHolder);
        }

        graphicHolder = new GameObject("Graphic holder");
        dateText.transform.position = new Vector3(dateText.transform.position.x, (extraRow) ? -5.4f: -4.6f, 0);

		int c = 0;
        int numRows = (extraRow)?6:5;
        blocks = new Block[7*numRows];
        for (int y = 0; y < numRows; y++)
		{
			for (int x = 0; x < 7; x++)
			{
				Block b = Instantiate(block, new Vector3(11.6f * .1f * x, -y * .1f * 9.2f, 0), Quaternion.identity) as Block;
                b.gameObject.transform.parent = graphicHolder.transform;

                b.text.text = "";
				blocks[c] = b;
				c++;
			}
		}
    }

    void DrawMonth(int monthIndex)
    {
        int numDays = System.DateTime.DaysInMonth(year, monthIndex + 1);
        System.DateTime dateTime = new System.DateTime(year, monthIndex + 1, 1);
        int startIndex = ((int)dateTime.DayOfWeek + 6) % 7;

        dateText.text = months.Split(' ')[monthIndex] + " " + year;

        InitMonth(startIndex >= 5);
		int day = 1;
		for (int i = startIndex; i < startIndex + numDays; i++)
		{
			blocks[i].text.text = day + "";
			day++;
		}
    }

    void ScreenCap(int index)
    {
        ScreenCapture.CaptureScreenshot("Assets/ScreenCaps/Month("+index+").png", 3);
    }


}
