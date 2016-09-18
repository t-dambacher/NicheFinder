using System;

namespace NicheFinder.DataBase.Entities
{
    public class NAF5Description : Entity
    {
        public Int32 IDNAF5 { get; set; }
        public String Description { get; set; }

        [Obsolete(EF_ONLY, error: true)]
        public NAF5Description()
        { }

        public NAF5Description(Int32 idNaf5, String description)
        {
            this.IDNAF5 = idNaf5;
            this.Description = description;
        }
    }
}
