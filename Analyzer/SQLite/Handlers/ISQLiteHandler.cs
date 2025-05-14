using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using UnityDataTools.FileSystem.TypeTreeReaders;

namespace UnityDataTools.Analyzer.SQLite.Handlers;

public class Context
{
    public int AssetBundleId { get; set; }
    public int SerializedFileId { get; set; }
    public int SceneId { get; set; }
    public Util.ObjectIdProvider ObjectIdProvider { get; set; }
    public Util.IdProvider<string> SerializedFileIdProvider { get; set; }
    public Dictionary<int, int> LocalToDbFileId { get; set; }
    public SqliteTransaction Transaction { get; set; }
}

public interface ISQLiteHandler : IDisposable
{
    void Init(Microsoft.Data.Sqlite.SqliteConnection db);
    void Process(Context ctx, long objectId, RandomAccessReader reader, out string name, out long streamDataSize);
    void Finalize(Microsoft.Data.Sqlite.SqliteConnection db);
}