using NicheFinder.DataBase.Entities;

namespace NicheFinder.DataBase.Configuration
{
    public class NAF5Configuration : EntityConfiguration<NAF5>
    { }

    public class NAF5DescriptionConfiguration : EntityConfiguration<NAF5Description>
    { }

    public class NounConfiguration : EntityConfiguration<Noun>
    { }

    public class NAF5Description_NounConfiguration : IDbEntityConfiguration<NAF5Description_Noun>
    {
        public NAF5Description_NounConfiguration()
        {
            this.HasKey(t => new { t.IDNAF5, t.IDNoun });
        }
    }

    public class SearchRequestConfiguration : EntityConfiguration<SearchRequest>
    { }

    public class SearchRequest_NounConfiguration : IDbEntityConfiguration<SearchRequest_Noun>
    {
        public SearchRequest_NounConfiguration()
        {
            this.HasKey(t => new { t.IDSearchRequest, t.IDNoun });
        }
    }
}
