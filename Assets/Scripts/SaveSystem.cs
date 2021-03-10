using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{
    public static string gamePath = Application.persistentDataPath + "/our_game.gamefile";
    public static string worldPath = Application.persistentDataPath + "/our_world.gamefile";

    public static void SavePlayer ()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(gamePath, FileMode.OpenOrCreate))
        {
            PlayerData data = new PlayerData();

            formatter.Serialize(stream, data);
            stream.Close();
        }        
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(gamePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(gamePath, FileMode.Open))
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
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(worldPath, FileMode.OpenOrCreate);

        WorldData data = new WorldData();

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static WorldData LoadWorld() 
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(worldPath))
        {
            using (FileStream stream = new FileStream(worldPath, FileMode.Open))
            {
                WorldData data = formatter.Deserialize(stream) as WorldData;
                return data;
            }
        } else
        {
            return null;
        }
    }

    public static void NewGame() {
        if (File.Exists(gamePath))
        {
            File.Delete(gamePath);
        }

        if (File.Exists(worldPath))
        {
            File.Delete(worldPath);
        }
    }

    public static bool HasSave()
    {
        if (File.Exists(gamePath) || File.Exists(worldPath))
        {
            return true;
        }

        return false;
    }
}
