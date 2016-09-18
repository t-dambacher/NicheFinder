using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheFinder.DataBase.Entities
{
    public class NAF5 : Entity
    {
        public String Code { get; set; }
        public String Libelle { get; set; }

        [Obsolete(EF_ONLY, error: true)]
        public NAF5()
        { }

        public NAF5(String code, String libelle)
        {
            this.Code = code;
            this.Libelle = libelle;
        }
    }
}
