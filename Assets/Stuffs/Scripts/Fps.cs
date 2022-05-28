using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fps : MonoBehaviour
{
	public Text FPS_Text;

	string label = "";
	float count;

	IEnumerator Start()
	{
		GUI.depth = 2;
		while (true)
		{
			if (Time.timeScale == 1)
			{
				yield return new WaitForSeconds(0.1f);
				count = (1 / Time.deltaTime);
				label = "FPS :" + (Mathf.Round(count));
			}
			else
			{
				label = "Pause";
			}
			yield return new WaitForSeconds(0.25f);
		}
	}

    private void Update()
    {
		FPS_Text.text = label;
    }

    //void OnGUI()
	//{
	//	GUI.Label(new Rect(5, 40, 100, 25), label);
	//}
}