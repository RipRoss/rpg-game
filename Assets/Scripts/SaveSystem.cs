using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{
    public static void SavePlayer ()
    {
        string path = Application.persistentDataPath + "/our_game.gamefile";
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
        {
            PlayerData data = new PlayerData();

            formatter.Serialize(stream, data);
            stream.Close();
        }        
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/our_game.gamefile";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;

                stream.Close();
                return data;
            }
        } else
        {
            // default 
            return null;
        }
    }

    public static void SaveWorld()
    {
        string path = Application.persistentDataPath + "/our_world.gamefile";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        WorldData data = new WorldData();

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static WorldData LoadWorld() 
    {
        string path = Application.persistentDataPath + "/our_world.gamefile";
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                WorldData data = formatter.Deserialize(stream) as WorldData;
                return data;
            }
        } else
        {
            return null;
        }
    }
}
