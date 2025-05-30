using System;
using System.Data;
using Microsoft.Data.Sqlite;
using UnityDataTools.Analyzer.SerializedObjects;
using UnityDataTools.FileSystem.TypeTreeReaders;

namespace UnityDataTools.Analyzer.SQLite.Handlers;

public class PreloadDataHandler : ISQLiteHandler
{
    private SqliteCommand m_InsertDepCommand;

    public void Init(SqliteConnection db)
    {
        m_InsertDepCommand = db.CreateCommand();
        m_InsertDepCommand.Connection = db;
        m_InsertDepCommand.CommandText = "INSERT INTO asset_dependencies(object, dependency) VALUES(@object, @dependency)";
        m_InsertDepCommand.Parameters.Add("@object", SqliteType.Integer);
        m_InsertDepCommand.Parameters.Add("@dependency", SqliteType.Integer);
    }

    public void Process(Context ctx, long objectId, RandomAccessReader reader, out string name, out long streamDataSize)
    {
        var preloadData = PreloadData.Read(reader);
        m_InsertDepCommand.Transaction = ctx.Transaction;
        m_InsertDepCommand.Parameters["@object"].Value = ctx.SceneId;

        foreach (var asset in preloadData.Assets)
        {
            var fileId = ctx.LocalToDbFileId[asset.FileId];
            var objId = ctx.ObjectIdProvider.GetId((fileId, asset.PathId));
            
            m_InsertDepCommand.Parameters["@dependency"].Value = objId;
            m_InsertDepCommand.ExecuteNonQuery();
        }

        name = "";
        streamDataSize = 0;
    }

    public void Finalize(SqliteConnection db)
    {
    }

    void IDisposable.Dispose()
    {
        m_InsertDepCommand?.Dispose();
    }
}