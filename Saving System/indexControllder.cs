using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Saving
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class indexControllder : MonoBehaviour
{
    public Save sav = new Save();  // Data structure
    public int index = 0;
    public Text indexText;

    public SpriteRenderer render;
    public Sprite[] cake;

	void Update()
	{
        indexText.text = index.ToString();
        render.sprite = cake[index];
    }
	public void previous()
	{
        index--;
        
	}

    public void next()
    {
        index++;
        
    }

    public void SaveGame()
	{
        //My saving data
        sav.index = index;

        // Serialization proccessing
        string path = Application.dataPath + "/Data";  // Saving direction
        string fileName = "Mysave.sav";  // Saving Document

		if (!Directory.Exists(path))  // If save direction don't exists,create path
		{
            Directory.CreateDirectory(path);
		}
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path + "/" + fileName);

		try  // If error,Get error message
		{
            bf.Serialize(file, sav);  // Serialize Data
		}
		catch (SerializationException e)
		{
            Debug.Log("Serialize Error");
		}
        

        file.Close();
	}

    public void LoadGame()
	{
        //Deserialize proccessing
        string path = Application.dataPath + "/Data";
        string fileName = "Mysave.sav";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path + "/" + fileName, FileMode.Open);  //Get ducument data

        sav = (Save)bf.Deserialize(file);  // Fitting data in class

        //Load my saving data
        index = sav.index;

        file.Close();

    }
}
