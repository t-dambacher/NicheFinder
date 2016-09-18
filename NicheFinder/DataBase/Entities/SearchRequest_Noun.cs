using System;

namespace NicheFinder.DataBase.Entities
{
    public class SearchRequest_Noun : IDbEntity
    {
        public Int32 IDSearchRequest { get; set; }
        public Int32 IDNoun { get; set; }
        public Int32 Order { get; set; }
        public Boolean IsNew { get; set; }

        [Obsolete(Entity.EF_ONLY, error: true)]
        public SearchRequest_Noun()
            : base()
        {
            this.IsNew = false;
        }

        public SearchRequest_Noun(Int32 idSearchRequest, Int32 idNoun, Int32 order)
        {
            this.IsNew = true;
            this.IDSearchRequest = idSearchRequest;
            this.IDNoun = idNoun;
            this.Order = order;
        }
    }
}
