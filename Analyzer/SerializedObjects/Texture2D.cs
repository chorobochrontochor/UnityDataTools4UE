using UnityDataTools.FileSystem.TypeTreeReaders;

namespace UnityDataTools.Analyzer.SerializedObjects;

public class Texture2D
{
    public string Name { get; private set; }
    public int StreamDataSize { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Format { get; private set; }
    public int MipCount { get; private set; }
    public bool RwEnabled { get; private set; }

    private Texture2D() {}
    
    public static Texture2D Read(RandomAccessReader reader)
    {
        return new Texture2D()
        {
            Name = reader["m_Name"].GetValue<string>(),
            Width = reader["m_Width"].GetValue<int>(),
            Height = reader["m_Height"].GetValue<int>(),
            Format = reader["m_TextureFormat"].GetValue<int>(),
            RwEnabled = reader["m_IsReadable"].GetValue<int>() != 0,
            MipCount = reader["m_MipCount"].GetValue<int>(),
            StreamDataSize = reader["image data"].GetArraySize() == 0 ? reader["m_StreamData"]["size"].GetValue<int>() : 0
        };
    }
}
