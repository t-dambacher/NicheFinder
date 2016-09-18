using NicheFinder.Utils;
using System;

namespace NicheFinder.DataBase.Entities
{
    public class Noun : Entity
    {
        public String Name { get; set; }
        public String Normalized { get; set; }

        [Obsolete(EF_ONLY, error: true)]
        public Noun()
        { }

        public Noun(String name)
        {
            this.Name = name;
            this.Normalized = StringNormalizer.RemoveDiacritics(name);
        }

        public static implicit operator Noun(String name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            return new Noun(name);
        }

        public override String ToString()
        {
            return Name;
        }
    }
}
