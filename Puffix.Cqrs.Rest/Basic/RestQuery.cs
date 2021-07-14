using Puffix.Cqrs.Context;
using Puffix.Cqrs.Executors;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Puffix.Cqrs.Rest.Basic
{
    /// <summary>
    /// Implémentation de base pour la définition d'une requête REST.
    /// </summary>
    /// <typeparam name="ResultT">Type du résutlat de la reuqête REST.</typeparam>
    /// <typeparam name="RestQueryResultT">Type du résultat brute de la requête REST.</typeparam>
    public abstract class RestQuery<ResultT, RestQueryResultT> : IRestQuery<ResultT, RestQueryResultT>
    {
        #region Propriétés

        /// <summary>
        /// Resultat de la requête.
        /// </summary>
        private ResultT result;

        /// <summary>
        /// Resultat de la requête.
        /// </summary>
        public ResultT Result => result;

        /// <summary>
        /// Interprétation du résultat de la requête.
        /// </summary>
        protected abstract Func<string, RestQueryResultT> ParseResult { get; }

        #endregion Propriétés

        /// <summary>
        /// Contrôle du contexte d'exécution de la requête.
        /// </summary>
        /// <param name="applicationContext">Context de l'application.</param>
        /// <param name="contextChecker">Contrôleur de contexte.</param>
        /// <returns>Résultat du contrôle.</returns>
        public abstract void CheckContext(IApplicationContext applicationContext, IChecker contextChecker);

        /// <summary>
        /// Contrôle des paramètres.
        /// </summary>
        /// <param name="parametersChecker">Contrôleur de paramètres.</param>
        /// <returns>Résultat du contrôle.</returns>
        public abstract void CheckParameters(IChecker parametersChecker);

        /// <summary>
        /// Construction de l'URI d'appel du service REST.
        /// </summary>
        /// <param name="executionContext">Contexte d'exécution.</param>
        /// <param name="applicationContext">Context de l'application.</param>
        /// <returns>URI d'appel.</returns>
        public abstract Uri BuildUri(IExecutionContext executionContext, IApplicationContext applicationContext);

        /// <summary>
        /// Execution de la requête (exécution interne).
        /// </summary>
        /// <param name="executionContext">Contexte d'exécution.</param>
        /// <param name="applicationContext">Context de l'application.</param>
        public async Task ExecuteAsync(IExecutionContext executionContext, IApplicationContext applicationContext)
        {
            // Construction de la requête.
            Uri uri = BuildUri(executionContext, applicationContext);

            // Appel du client REST.
            RestQueryResultT restResult = await CallRestServiceAsync(uri);

            // Extraction du résultat.
            result = ExtractResult(restResult);
        }

        /// <summary>
        /// Extraction du résulat de la requête REST.
        /// </summary>
        /// <param name="queryResult">Résultat de la requête REST.</param>
        /// <returns>Résultat de la requête.</returns>
        public abstract ResultT ExtractResult(RestQueryResultT queryResult);

        /// <summary>
        /// Appel du service REST.
        /// </summary>
        /// <param name="serviceUri">Uri d'appel.</param>
        /// <returns>Contenu XML de la réponse.</returns>
        private async Task<RestQueryResultT> CallRestServiceAsync(Uri serviceUri)
        {
            // Envoi de la requête et traitement de la réponse.
            string result;
            using HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync(serviceUri);
            response.EnsureSuccessStatusCode();

            // Lecture du contenu de la réponse.
            result = await response.Content.ReadAsStringAsync();

            // Déclaration du résultat de la requête.
            RestQueryResultT queryResult = ParseResult(result);
            return queryResult;
        }
    }
}
