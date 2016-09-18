using System;

namespace NicheFinder.DataBase.Entities
{
    public interface IDbEntity
    {
        Boolean IsNew { get; }
    }
}
