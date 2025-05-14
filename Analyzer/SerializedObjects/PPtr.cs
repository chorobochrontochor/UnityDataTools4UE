using UnityDataTools.FileSystem.TypeTreeReaders;

namespace UnityDataTools.Analyzer.SerializedObjects;

public class PPtr
{
    public int FileId { get; private set; }
    public long PathId { get; private set; }
    
    private PPtr() {}

    public static PPtr Read(RandomAccessReader reader)
    {
        return new PPtr()
        {
            FileId = reader["m_FileID"].GetValue<int>(),
            PathId = reader["m_PathID"].GetValue<long>()
        };
    }
}
