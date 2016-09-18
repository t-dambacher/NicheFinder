using System;

namespace NounsWiktionary
{
    /// <summary>
    /// Exception levée en cas d'erreur de parsing
    /// </summary>
    internal class ParseException : ApplicationException
    {
        /// <summary>
        /// Numéro de ligne
        /// </summary>
        public Int32 LineNumber { get; private set; }

        /// <summary>
        /// Contenu de la ligne
        /// </summary>
        public String Line { get; private set; }

        /// <summary>
        /// Construit une nouvelle instance de la classe ParseException
        /// </summary>
        public ParseException(Int32 lineNumber, String lineContent, Exception innerException)
            : base(String.Empty, innerException)
        {
            this.Line = lineContent;
            this.LineNumber = lineNumber;
        }

        /// <summary>
        /// Message d'erreur
        /// </summary>
        public override String Message
        {
            get
            {
                return $"Une erreur est survenue lors du parse de la ligne {LineNumber} ('{Line}') : {InnerException.Message} ";
            }
        }
    }
}
