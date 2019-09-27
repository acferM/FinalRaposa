using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class PlayerSaveAndLoad 
{
    public static void playerSaveGame(PlayerBehaviour player)
    {
        var formatador = new BinaryFormatter();
        string caminho = Application.persistentDataPath + "player.savezin";
        var stream = new FileStream(caminho, FileMode.Create);

        var data = new PlayerGetData(player);

        formatador.Serialize(stream, data);
    }

    public static PlayerGetData playerLoadGame()
    {
        string caminho = Application.persistentDataPath + "player.savezin";
        if (File.Exists(caminho))
        {
            var desformatar = new BinaryFormatter();
            var stream = new FileStream(caminho, FileMode.Open);

            PlayerGetData data = desformatar.Deserialize(stream) as PlayerGetData;

            return data;
        }
        else
        {
            Debug.Log("Save não encontrado em " + caminho);
            return null;
        }
    }
}
