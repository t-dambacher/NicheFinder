using System;

namespace NicheFinder.DataBase.Entities
{
    public class SearchRequest : Entity
    {
        public Int32 IDNAF5Description { get; set; }
        public SearchRequestState State { get; set; }
        public String Result { get; set; }
        public Int32 ResultCount { get; set; }

        [Obsolete(EF_ONLY, error: true)]
        public SearchRequest()
        { }

        public SearchRequest(Int32 idNAF5Description)
        {
            this.IDNAF5Description = idNAF5Description;
            this.State = SearchRequestState.Waiting;
            this.Result = null;
            this.ResultCount = 0;
        }
    }
}
