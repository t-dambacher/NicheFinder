using System;

namespace NicheFinder.DataBase.Entities
{
    public class NAF5Description_Noun : IDbEntity
    {
        public Int32 IDNAF5 { get; set; }
        public Int32 IDNoun { get; set; }
        public Boolean IsNew { get; set; }

        [Obsolete(Entity.EF_ONLY, error: true)]
        public NAF5Description_Noun()
        {
            this.IsNew = false;
        }

        public NAF5Description_Noun(Int32 idNaf5, Int32 idNoun)
        {
            this.IsNew = true;
            this.IDNAF5 = idNaf5;
            this.IDNoun = idNoun;
        }
    }
}
