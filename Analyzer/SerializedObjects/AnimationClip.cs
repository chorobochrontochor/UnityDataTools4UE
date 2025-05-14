using UnityDataTools.FileSystem.TypeTreeReaders;

namespace UnityDataTools.Analyzer.SerializedObjects;

public class AnimationClip
{
    public string Name { get; private set; }
    public bool Legacy { get; private set; }
    public int Events { get; private set; }
    
    private AnimationClip() {}

    public static AnimationClip Read(RandomAccessReader reader)
    {
        return new AnimationClip()
        {
            Name = reader["m_Name"].GetValue<string>(),
            Legacy = reader["m_Legacy"].GetValue<bool>(),
            Events = reader["m_Events"].GetArraySize()
        };
    }
}