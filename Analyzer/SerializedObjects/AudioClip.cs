using UnityDataTools.FileSystem.TypeTreeReaders;

namespace UnityDataTools.Analyzer.SerializedObjects;

public class AudioClip
{
    public string Name { get; private set; }
    public int StreamDataSize { get; private set; }
    public int BitsPerSample { get; private set; }
    public int Frequency { get; private set; }
    public int Channels { get; private set; }
    public int LoadType { get; private set; }
    public int Format { get; private set; }

    private AudioClip() {}

    public static AudioClip Read(RandomAccessReader reader)
    {
        return new AudioClip()
        {
            Name = reader["m_Name"].GetValue<string>(),
            Channels = reader["m_Channels"].GetValue<int>(),
            Format = reader["m_CompressionFormat"].GetValue<int>(),
            Frequency = reader["m_Frequency"].GetValue<int>(),
            LoadType = reader["m_LoadType"].GetValue<int>(),
            BitsPerSample = reader["m_BitsPerSample"].GetValue<int>(),
            StreamDataSize = reader["m_Resource"]["m_Size"].GetValue<int>()
        };
    }
}