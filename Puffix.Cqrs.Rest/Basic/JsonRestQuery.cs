using System;
using System.Text.Json;

namespace Puffix.Cqrs.Rest.Basic
{
    /// <summary>
    /// Implémentation de base pour la définition d'une requête REST avec un résultat au format JSON.
    /// </summary>
    /// <typeparam name="ResultT">Type du résutlat de la reuqête REST.</typeparam>
    public abstract class JsonRestQuery<ResultT> : RestQuery<ResultT, string>
    {
        #region Propriétés

        /// <summary>
        /// Interprétation du résultat de la requête.
        /// </summary>
        protected override Func<string, string> ParseResult => restResult => restResult;

        #endregion Propriétés

        /// <summary>
        /// Extraction du résulat de la requête REST.
        /// </summary>
        /// <param name="queryResult">Résultat de la requête REST.</param>
        /// <returns>Résultat de la requête.</returns>
        public override ResultT ExtractResult(string queryResult) => JsonSerializer.Deserialize<ResultT>(queryResult);

        /// <summary>
        /// Extraction du résultat de la requête REST ().
        /// </summary>
        /// <param name="queryResult">Résultat de la requête REST.</param>
        /// <param name="conversionFunction">Fonction de transformation (prend en argument un document JSON).</param>
        /// <returns>Résultat de la requête.</returns>
        public virtual ResultT ExtractResult(string queryResult, Func<JsonDocument, ResultT> conversionFunction)
        {
            JsonDocumentOptions options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };

            using JsonDocument jsonDocument = JsonDocument.Parse(queryResult, options);
            return conversionFunction(jsonDocument);
        }
    }
}
