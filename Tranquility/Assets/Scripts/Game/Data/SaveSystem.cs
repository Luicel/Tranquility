// This file is a heavily modified version of the class from Brackleys's tutorial on Save and Load Systems
// https://www.youtube.com/watch?v=XOjd_qU2Idousing

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem {
    public static void SaveCheckpointPosition(Vector2 position) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/checkpoint_position.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        CheckpointPositionData data = new CheckpointPositionData(position);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CheckpointPositionData LoadCheckpointPosition() {
        string path = Application.persistentDataPath + "/checkpoint_position.data";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CheckpointPositionData data = formatter.Deserialize(stream) as CheckpointPositionData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SavePiecesCollected(bool[] collectedPieces) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/pieces_collected.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PiecesCollectedData data = new PiecesCollectedData(collectedPieces);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PiecesCollectedData LoadPiecesCollected() {
        string path = Application.persistentDataPath + "/pieces_collected.data";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PiecesCollectedData data = formatter.Deserialize(stream) as PiecesCollectedData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
